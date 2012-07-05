// -----------------------------------------------------------------------
// <copyright file="TripViewModel.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// ViewModel for displaying a a single trip's import status.
    /// </summary>
    public class TripViewModel : INotifyPropertyChanged
    {
        // These properties aren't changed by the UI, so no events for them
        public virtual int Id { get; set; }
        public virtual string TripNumber { get; set; }
        public virtual DateTime? DepartureDate { get; set; }
        public virtual DateTime? ReturnDate { get; set; }
        public virtual string GearCode { get; set; }

        private bool _shouldCopy;
        public virtual bool ShouldCopy
        {
            get { return _shouldCopy; }
            set
            {
                _shouldCopy = value;
                NotifyPropertyChanged("ShouldCopy");
            }
        }

        private bool? _isSuccessful;
        public virtual bool? IsSuccessful 
        {
            get { return _isSuccessful; }
            set
            {
                _isSuccessful = value;
                NotifyPropertyChanged("IsSuccessful");
            }
        }

        private string _importMessage;
        public virtual string ImportMessage 
        {
            get { return _importMessage; }
            set
            {
                _importMessage = value;
                NotifyPropertyChanged("ImportMessage");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyChanged)
        {
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyChanged));
        }
    }
}
