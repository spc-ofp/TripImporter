using System;
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
            btnPause.IsEnabled = true;
            btnStop.IsEnabled = true;
            CopySelectedTrips();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            btnPause.IsEnabled = false;
            btnContinue.IsEnabled = true;
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            btnPause.IsEnabled = true;
            btnContinue.IsEnabled = false;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = true;
            btnPause.IsEnabled = false;
            btnStop.IsEnabled = false;
            btnContinue.IsEnabled = false;
            var worker = ((Button)sender).Tag as BackgroundWorker;
            if (null != worker)
                worker.CancelAsync();
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
            worker.WorkerSupportsCancellation = true;

            // How do I wire up events for btnPause etc?
            btnStop.Tag = worker;
            btnStop.Click += new RoutedEventHandler(btnStop_Click);
           

            worker.DoWork += delegate(object s, DoWorkEventArgs args)
            {
                int itemsCompleted = 0;
                foreach (var item in items)
                {
                    var results = this._tripService.CopyTrip(item.Id);
                    if (results.Item1)
                    {
                        item.ShouldCopy = false;
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
                    importProgress.Value = 0;
                }
                
                // TODO Re-enable btnStart when complete or cancelled (Still need to check that this is correct)
                btnStart.IsEnabled = true;
                
            };

            worker.RunWorkerAsync();

        }
    }
}
