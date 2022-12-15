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
    /// Interaction logic for DriverListView.xaml
    /// </summary>
    public partial class DriverListView : UserControl
    {
        public DriverViewModel SelectedDriver { get; set; }
        private DriversController driversController;
        public DriverListView()
        {
            SelectedDriver = null;
            driversController = new DriversController();
            InitializeComponent();
        }

        private async void update_btn_Click(object sender, RoutedEventArgs e)
        {
            if (first_name_txt.Text != "" && last_name_txt.Text != "" && phone_number_txt.Text != "" && postal_code_txt.Text != "" && city_txt.Text != "" && address_txt.Text != "" && email_txt.Text != "" && salary_txt.Text != "")
            {
                Driver driver = new Driver();

                driver.Id = SelectedDriver.Id;
                driver.FirstName = SelectedDriver.FirstName;
                driver.LastName = SelectedDriver.LastName;
                driver.Phone = SelectedDriver.Phone;
                driver.PostalCode = SelectedDriver.PostalCode;
                driver.City = SelectedDriver.City;
                driver.Address = SelectedDriver.Address;
                driver.Email = SelectedDriver.Email;
                driver.Salary = SelectedDriver.Salary;

                var wasModifiedInDb = await driversController.UpdateDriverAsync(SelectedDriver.Id, driver);

                if (wasModifiedInDb)
                {
                    SelectedDriver.FirstName = first_name_txt.Text;
                    SelectedDriver.LastName = last_name_txt.Text;
                    SelectedDriver.Phone = phone_number_txt.Text;
                    SelectedDriver.PostalCode = Int32.Parse(postal_code_txt.Text);
                    SelectedDriver.City = city_txt.Text;
                    SelectedDriver.Address = address_txt.Text;
                    SelectedDriver.Email = email_txt.Text;
                    SelectedDriver.Salary = Int32.Parse(salary_txt.Text);

                    CleanUpSelection();
                    CollectionViewSource.GetDefaultView(this.driversDataGrid.ItemsSource).Refresh();
                }
                else
                {
                    MessageBox.Show("Can not delete this driver!");
                }
            }
            else
            {
                MessageBox.Show("Can not update with a incorrect input value");
            }
        }

        private async void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDriver != null)
            {
                bool wasDeleted = false;
                if (MessageBox.Show("Are you sure you want to delete this driver?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    wasDeleted = await driversController.DeleteDriverAsync(SelectedDriver.Id);
                }
                if (wasDeleted)
                {
                    CleanUpSelection();
                    CollectionViewSource.GetDefaultView(this.driversDataGrid.ItemsSource).Refresh();
                }
                else
                {
                    MessageBox.Show("Can not delete this driver!");
                }
            }
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
                postal_code_txt.Text = SelectedDriver.PostalCode.ToString();
                city_txt.Text = SelectedDriver.City;
                address_txt.Text = SelectedDriver.Address;
                email_txt.Text = SelectedDriver.Email;
                salary_txt.Text = SelectedDriver.Salary.ToString();

                update_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;
            }
        }

        private void CleanUpSelection()
        {
            first_name_txt.Text = "";
            last_name_txt.Text = "";
            phone_number_txt.Text = "";
            email_txt.Text = "";
            postal_code_txt.Text = "";
            city_txt.Text = "";
            address_txt.Text = "";
            salary_txt.Text = "";
            SelectedDriver = null;
        }
    }
}