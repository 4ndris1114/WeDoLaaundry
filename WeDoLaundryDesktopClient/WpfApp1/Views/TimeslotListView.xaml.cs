using System;
using System.Collections.Generic;
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
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for TimeslotListView.xaml
    /// </summary>
    public partial class TimeslotListView : UserControl
    {
        private readonly TimeslotsController _controller;

        public TimeslotViewModel SelectedTimeslot { get; set; }

        public TimeslotListView()
        {
            _controller = new();
            SelectedTimeslot = null;
            InitializeComponent();
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            bool mode = new();
            int value = 0;

            if (SelectedTimeslot.Availability < Convert.ToInt32(availability_txt.Text))
            {
                mode = true;
                value = Convert.ToInt32(availability_txt.Text) - SelectedTimeslot.Availability;
            } else if (SelectedTimeslot.Availability > Convert.ToInt32(availability_txt.Text)) {
                mode = false;
                value = SelectedTimeslot.Availability - Convert.ToInt32(availability_txt.Text);
            } else {
                MessageBox.Show("Availability must change to update!");
            }

            if (SelectedTimeslot != null && value != 0)
            {
                _controller.Modify(SelectedTimeslot.Id, mode, value);
            }
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            // missing a logic for delete

        }

        private void timeslotDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            TimeslotViewModel t = timeslotDataGrid.SelectedItem as TimeslotViewModel;
            if (t != null)
            {
                SelectedTimeslot = t;
            }

            if (SelectedTimeslot != null)
            {
                id_txt.Text = SelectedTimeslot.Id.ToString();
                date_txt.Text = SelectedTimeslot.Date;
                timeSlot_txt.Text = SelectedTimeslot.Slot;
                availability_txt.Text = SelectedTimeslot.Availability.ToString();

                update_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;
            }
        }
    }
}
