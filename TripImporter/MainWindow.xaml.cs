﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ImportLibrary;
using System.ComponentModel;

namespace TripImporter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PendingTripService _tripService = new PendingTripService();

        private Brush NormalBrush = new SolidColorBrush() { Color = new Color() { A = 255, R = 1, G = 211, B = 28 } };

        private EcObservableCollection<TripViewModel> GetObservableTrips(string programCode = null)
        {
            var trips = this._tripService.GetPendingTrips(programCode);
            var observableTrips = new EcObservableCollection<TripViewModel>();
            trips.ToList().ForEach(x => observableTrips.Add(x));
            return observableTrips;
        }
        
        public MainWindow()
        {
            InitializeComponent();
            // AutoMapper configuration -- only needs to happen once
            AutoMapperConfiguration.Configure();
            btnContinue.IsEnabled = false;
            btnPause.IsEnabled = false;
            btnStop.IsEnabled = false;
            pendingTrips.ItemsSource = GetObservableTrips();           
        }

        private void OnProgramChange(object sender, SelectionChangedEventArgs e)
        {
            string programCode = cmbProgramCode.SelectedValue as string;
            if (null != pendingTrips)
            {
                pendingTrips.ItemsSource = GetObservableTrips(programCode);
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            CopySelectedTrips();
        }

        private void CopySelectedTrips()
        {
            // Enable Win7 Taskbar integration
            // Still not quite working, but pick it up from here:
            // http://msdn.microsoft.com/en-us/library/ff969360.aspx
            TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Normal;
            
            // Only copy the checked items
            var items = pendingTrips.Items
                                    .Cast<TripViewModel>()
                                    .Where(i => i.ShouldCopy);
            int maxRecords = items.Count();

            // Use a background worker that reports progress
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;

            worker.DoWork += delegate(object s, DoWorkEventArgs args)
            {
                int itemsCompleted = 0;
                foreach (var item in items)
                {
                    var results = this._tripService.CopyTrip(item.Id);
                    if (results.Item1)
                    {
                        // TODO Update status icon

                    }
                    item.ImportMessage = results.Item2;
                    itemsCompleted++;
                    int percentComplete = Convert.ToInt32(((decimal)itemsCompleted / (decimal)maxRecords) * 100);
                    worker.ReportProgress(percentComplete, results.Item1);
                    
                }
                
            };

            worker.ProgressChanged += (s, e) =>
            {
                importProgress.Value = e.ProgressPercentage;
                importProgress.Foreground =
                    ((bool)e.UserState) ? importProgress.Foreground = NormalBrush : Brushes.Red;
                TaskbarItemInfo.ProgressState =
                    ((bool)e.UserState) ? 
                        System.Windows.Shell.TaskbarItemProgressState.Normal :
                        System.Windows.Shell.TaskbarItemProgressState.Error;
                TaskbarItemInfo.ProgressValue = ((double)e.ProgressPercentage) / 100;
            };

            worker.RunWorkerCompleted += (s, e) =>
            {
                if (e.Cancelled)
                {
                    TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Paused;
                }
                else if (null != e.Error)
                {
                    TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Error;
                }
                else
                {
                    TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.None;
                }
                
                // TODO Re-enable btnStart when complete or cancelled
                
            };

            worker.RunWorkerAsync();

        }
    }
}
