using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using WpfApp1.Model;
using WpfApp1.ViewModels;
using WpfApp1.Controller_layer;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for BookingListView.xaml
    /// </summary>
    public partial class BookingListView : UserControl
    {
        public BookingViewModel SelectedBooking { get; set; }
        private BookingsController bController;
        public BookingListView()
        {
            SelectedBooking = null;
            bController = new BookingsController();
            InitializeComponent();
        }

        private async void update_btn_Click(object sender, RoutedEventArgs e)
        {
            if (status_txt.Text == "BOOKED" || status_txt.Text == "IN_PROGRESS" || status_txt.Text == "RETURNED") // Check other stuff too
            {
                Booking newBooking = new Booking();

                newBooking.Id = SelectedBooking.Id;
                newBooking.CustomerId = SelectedBooking.CustomerId;
                newBooking.DriverId = SelectedBooking.DriverId;
                newBooking.PickUpTimeId = SelectedBooking.PickUpTimeId;
                newBooking.ReturnTimeId = SelectedBooking.ReturnTimeId;
                newBooking.PickUpAddress = pick_up_address_txt.Text;
                newBooking.ReturnAddress = return_address_txt.Text;
                newBooking.Status = (Booking.BookingStatus)Enum.Parse(typeof(Booking.BookingStatus), status_txt.Text);
                newBooking.AmountOfBags = Int16.Parse(amount_of_bags_txt.Text);

                //await bController.UpdateBookingAsync(SelectedBooking.Id, newBooking);
            }
            else
            {
                //Display error message??
            }
        }

        private async void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            var bookingId = SelectedBooking.Id;
            //await bController.DeleteBookingAsync(SelectedBooking);

        }
        private void bookingsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BookingViewModel b = bookingsDataGrid.SelectedItem as BookingViewModel;
            if (b != null)
            {
                SelectedBooking = b;
            }

            if (SelectedBooking != null)
            {
                pick_up_address_txt.Text = SelectedBooking.PickUpAddress;
                return_address_txt.Text = SelectedBooking.ReturnAddress;
                status_txt.Text = SelectedBooking.Status.ToString();
                amount_of_bags_txt.Text = SelectedBooking.AmountOfBags.ToString();

                update_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;
            }
        }
    }
}