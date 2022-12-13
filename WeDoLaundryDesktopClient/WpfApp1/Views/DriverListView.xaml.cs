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
using WpfApp1.Controller_layer;
using WpfApp1.Model;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for DriverView.xaml
    /// </summary>
    public partial class DriverListView : UserControl
    {
        public DriverViewModel SelectedDriver { get; set; }
        public DriversController driversController;

        public DriverListView()
        {
            SelectedDriver = null;
            driversController = new DriversController();
            InitializeComponent();
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            var driverId = SelectedDriver.Id;

        }

        private async void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            var driverId = SelectedDriver.Id;

            await driversController.DeleteDriverAsync(driverId);
        }

        private void driversDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DriverViewModel d = driversDataGrid.SelectedItem as DriverViewModel;
            if (d != null)
            {
                SelectedDriver = d;
            }

            if (SelectedDriver != null)
            {
                first_name_txt.Text = SelectedDriver.FirstName;
                last_name_txt.Text = SelectedDriver.LastName;
                phone_number_txt.Text = SelectedDriver.Phone;
                email_txt.Text = SelectedDriver.Email;
                postal_code_txt.Text = SelectedDriver.PostalCode.ToString();
                city_txt.Text = SelectedDriver.City;
                address_txt.Text = SelectedDriver.Address;
                salary_txt.Text = SelectedDriver.Salary.ToString();

                update_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;
            }
        }
    }
}
