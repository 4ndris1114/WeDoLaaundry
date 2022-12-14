using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerListView : UserControl
    {
        public CustomerViewModel SelectedCustomer { get; set; }
        public CustomersController customersController;

        public CustomerListView()
        {
            SelectedCustomer = null;
            customersController = new CustomersController();
            InitializeComponent();
        }

        private bool validateFields(Customer toBeChecked)
        {
            bool firstNameValid = Regex.IsMatch(toBeChecked.FirstName, @"^[a-zA-Z]+$");
            bool lastNameValid = Regex.IsMatch(toBeChecked.LastName, @"^[a-zA-Z]+$");
            bool phoneValid = Regex.IsMatch(toBeChecked.Phone, @"^\+[1-9]{1}[0-9]{3,14}$");
            bool emailValid = Regex.IsMatch(toBeChecked.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            bool postalValid = toBeChecked.PostalCode.GetType() == typeof(int);
            bool cityValid = Regex.IsMatch(toBeChecked.City, @"^[a-zA-Z]+$");
            bool addressValid = Regex.IsMatch(toBeChecked.Address, @"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$");


            if (firstNameValid && lastNameValid && phoneValid && emailValid && postalValid && cityValid && addressValid)
            {
                MessageBox.Show("Updated successfully!");
                return true;
            } else
            {
                MessageBox.Show("Couldn't be updated!");
                return false;
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
            SelectedCustomer = null;
        }

        private async void update_btn_Click(object sender, RoutedEventArgs e)
        {
            var customerId = SelectedCustomer.Id;

            Customer updateCustomer = new Customer();

            updateCustomer.Id = customerId;
            updateCustomer.FirstName = first_name_txt.Text;
            updateCustomer.LastName = last_name_txt.Text;
            updateCustomer.Phone = phone_number_txt.Text;
            updateCustomer.Email = email_txt.Text;
            updateCustomer.PostalCode = Int32.Parse(postal_code_txt.Text);
            updateCustomer.City = city_txt.Text;
            updateCustomer.Address = address_txt.Text;
            updateCustomer.CustomerType = (Customer.Subscription)SelectedCustomer.CustomerType;
            updateCustomer.UserId = SelectedCustomer.UserId;
            if (first_name_txt.Text != "" && last_name_txt.Text != "" && phone_number_txt.Text != "" &&
                email_txt.Text != "" && postal_code_txt.Text != "" && city_txt.Text != "" && address_txt.Text != "")
            {
                if (validateFields(updateCustomer))
                {
                    bool wasModifiedInDb = await customersController.UpdateCustomerAsync(customerId, updateCustomer);
                    if (wasModifiedInDb)
                    {
                        SelectedCustomer.FirstName = first_name_txt.Text;
                        SelectedCustomer.LastName = last_name_txt.Text;
                        SelectedCustomer.Phone = phone_number_txt.Text;
                        SelectedCustomer.Email = email_txt.Text;
                        SelectedCustomer.PostalCode = Convert.ToInt32(postal_code_txt.Text);
                        SelectedCustomer.City = city_txt.Text;
                        SelectedCustomer.Address = address_txt.Text;
                        CleanUpSelection();
                        CollectionViewSource.GetDefaultView(this.customersDataGrid.ItemsSource).Refresh();
                    }
                }
            }
        }

        private async void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCustomer != null)
            {
                bool wasDeleted = await customersController.DeleteCustomerAsync(SelectedCustomer.Id);
                if (wasDeleted)
                {
                    CleanUpSelection();
                    CollectionViewSource.GetDefaultView(this.customersDataGrid.ItemsSource).Refresh();
                }
                else
                {
                    MessageBox.Show("Can not delete this time slot!");
                }
            }
        }

        private void customersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            CustomerViewModel c = customersDataGrid.SelectedItem as CustomerViewModel;
            if (c != null)
            {
                SelectedCustomer = c;
            }

            if (SelectedCustomer != null)
            {
                first_name_txt.Text = SelectedCustomer.FirstName;
                last_name_txt.Text = SelectedCustomer.LastName;
                phone_number_txt.Text = SelectedCustomer.Phone;
                email_txt.Text = SelectedCustomer.Email;
                postal_code_txt.Text = SelectedCustomer.PostalCode.ToString();
                city_txt.Text = SelectedCustomer.City;
                address_txt.Text = SelectedCustomer.Address;

                update_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;
            }
        }
    }
}
