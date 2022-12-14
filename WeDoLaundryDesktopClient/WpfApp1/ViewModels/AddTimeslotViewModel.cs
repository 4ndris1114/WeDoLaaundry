using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Services;
using WpfApp1.Stores;

namespace WpfApp1.ViewModels
{
    public class AddTimeslotViewModel : ViewModelBase
    {
        private DateTime _date;
        private string _slot;
        private int _availability;

        public DateTime Date 
        { 
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
        public string Slot
        {
            get
            {
                return _slot;
            }
            set
            {
                _slot = value;
                OnPropertyChanged(nameof(Slot));
            }
        }
        public int Availability
        {
            get
            {
                return _availability;
            }
            set
            {
                _availability = value;
                OnPropertyChanged(nameof(Availability));
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public AddTimeslotViewModel(TimeslotStore timeslotStore, INavigationService cancelNavigationService)
        {
            CancelCommand = new NavigateCommand(cancelNavigationService);
            SubmitCommand = new AddTimeslotCommand(this, timeslotStore, cancelNavigationService);
        }
    }
}
