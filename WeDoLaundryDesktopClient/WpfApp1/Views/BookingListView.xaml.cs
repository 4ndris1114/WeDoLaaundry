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

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for BookingListView.xaml
    /// </summary>
    public partial class BookingListView : UserControl
    {
        public BookingViewModel SelectedBooking { get; set; }
        public BookingListView()
        {
            SelectedBooking = null;
            InitializeComponent();
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            // missing a logic for update
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            // missing a logic for delete

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