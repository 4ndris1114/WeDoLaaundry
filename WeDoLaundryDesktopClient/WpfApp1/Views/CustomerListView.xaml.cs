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

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerListView : UserControl
    {
        public CustomerListView()
        {
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
            DataGrid dg = (DataGrid)sender;
            DataRowView dr = (DataRowView)dg.SelectedItem; 

            if (dr != null)
            {
                first_name_txt.Text = dr["FirstName"].ToString();
                last_name_txt.Text = dr["LastName"].ToString();
                phone_number_txt.Text = dr["Phone"].ToString();
                email_txt.Text = dr["Email"].ToString();
                postal_code_txt.Text = dr["PostalCode"].ToString();
                city_txt.Text = dr["City"].ToString();
                address_txt.Text = dr["Address"].ToString();
                subscription_txt.Text = dr["CustomerType"].ToString();

                update_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;
            }
        }
    }
}
