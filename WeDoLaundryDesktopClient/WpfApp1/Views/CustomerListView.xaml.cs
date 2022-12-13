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

        public CustomerListView()
        {
            SelectedCustomer = null;
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
                subscription_txt.Text = SelectedCustomer.CustomerType.ToString();

                update_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;
            }
        }
    }
}
