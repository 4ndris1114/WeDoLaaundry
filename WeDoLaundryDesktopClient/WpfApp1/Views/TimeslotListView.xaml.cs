﻿using System;
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
using WpfApp1.Services;
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

        private async void update_btn_Click(object sender, RoutedEventArgs e)
        {
            bool mode = new();
            int value = 0;

            if (SelectedTimeslot != null)
            {
                //Check if availability changed:
                if (SelectedTimeslot.Availability < Convert.ToInt32(availability_txt.Text))
                {
                    mode = true;
                    value = Convert.ToInt32(availability_txt.Text) - SelectedTimeslot.Availability;
                }
                else if (SelectedTimeslot.Availability > Convert.ToInt32(availability_txt.Text))
                {
                    mode = false;
                    value = SelectedTimeslot.Availability - Convert.ToInt32(availability_txt.Text);
                }
                else
                {
                    MessageBox.Show("Availability must change in order to update!");
                }

                if (SelectedTimeslot != null && value != 0)
                {
                    bool wasModifiedInDb = await _controller.Modify(SelectedTimeslot.Id, mode, value);
                    if (wasModifiedInDb)
                    {
                        SelectedTimeslot.Availability = Convert.ToInt32(availability_txt.Text); //Modify viewmodel => raise PropertyChanged event
                        CleanUpSelection();
                        CollectionViewSource.GetDefaultView(this.timeslotDataGrid.ItemsSource).Refresh();
                    } else
                    {
                        MessageBox.Show("Modification was not successful");
                    }
                }
            }
        }

        private void CleanUpSelection()
        {
            id_txt.Text = "";
            date_txt.Text = "";
            timeSlot_txt.Text = "";
            availability_txt.Text = "";
            addresses_txt.Text = "";
            SelectedTimeslot = null;
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            // missing a logic for delete

        }

        private async void timeslotDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CleanUpSelection();
            TimeslotViewModel t = timeslotDataGrid.SelectedItem as TimeslotViewModel;
            if (t != null)
            {
                SelectedTimeslot = t;
            }

            if (SelectedTimeslot != null)
            {
                //Update panel
                id_txt.Text = SelectedTimeslot.Id.ToString();
                date_txt.Text = SelectedTimeslot.Date;
                timeSlot_txt.Text = SelectedTimeslot.Slot;
                availability_txt.Text = SelectedTimeslot.Availability.ToString();

                update_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;

                //Addresses panel
                List<string> addressList = await _controller.GetTimeslotAddressesAsync(SelectedTimeslot.Id);
                if (addressList != null)
                {
                    foreach (var item in addressList)
                    {
                        addresses_txt.Text += item + "\n";
                    }
                } else
                {
                    addresses_txt.Text = "No addresses";
                }
            }
        }
    }
}
