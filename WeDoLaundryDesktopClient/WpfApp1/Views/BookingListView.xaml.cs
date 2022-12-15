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
            if (SelectedBooking != null && pick_up_address_txt.Text != "" && return_address_txt.Text != ""
                && status_txt.Text != "" && amount_of_bags_txt.Text != "") {
                if ((status_txt.Text == "BOOKED" || status_txt.Text == "IN_PROGRESS" || status_txt.Text == "RETURNED")
                    && 0 < Int16.Parse(amount_of_bags_txt.Text)) // Check other stuff too
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

                    var wasModifiedInDb = await bController.UpdateBookingAsync(SelectedBooking.Id, newBooking);

                    if (wasModifiedInDb)
                    {
                        SelectedBooking.PickUpAddress = pick_up_address_txt.Text;
                        SelectedBooking.ReturnAddress = return_address_txt.Text;
                        SelectedBooking.Status = (BookingStatus)Enum.Parse(typeof(BookingStatus), status_txt.Text);
                        SelectedBooking.AmountOfBags = Convert.ToInt32(amount_of_bags_txt.Text);
                        CleanUpSelection();
                        CollectionViewSource.GetDefaultView(this.bookingsDataGrid.ItemsSource).Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Can not delete this time slot!");
                    }
                }
                else
                {
                    MessageBox.Show("Can not update with a incorrect input value");
                }
            }
            else
            {
                MessageBox.Show("Can not update without a value");
            }
        }

        private void CleanUpSelection()
        {
            pick_up_address_txt.Text = "";
            return_address_txt.Text = "";
            status_txt.Text = "";
            amount_of_bags_txt.Text = "";
            SelectedBooking = null;
        }

        private async void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBooking != null)
            {
                bool wasDeleted = false;
                if (MessageBox.Show("Are you sure you want to delete this booking?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    wasDeleted = await bController.DeleteBookingAsync(SelectedBooking.Id, SelectedBooking.PickUpTimeId, SelectedBooking.ReturnTimeId);
                }
                if (wasDeleted)
                {
                    CleanUpSelection();
                    CollectionViewSource.GetDefaultView(this.bookingsDataGrid.ItemsSource).Refresh();
                }
                else
                {
                    MessageBox.Show("Booking was not deleted!");
                }
            }
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