using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Controller_layer;
using WpfApp1.Model;
using WpfApp1.Services;
using WpfApp1.Stores;

namespace WpfApp1.ViewModels
{
    public class CustomerListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<CustomerViewModel> _customers;
        private readonly CustomersController _controller;

        public IEnumerable<CustomerViewModel> Customers => _customers;

        public CustomerListViewModel()
        {
            _controller = new CustomersController();
            _customers = new ObservableCollection<CustomerViewModel>();
            PopulateList();
        }

        private async void PopulateList()
        {
            List<Customer> customerList = await _controller.GetCustomersAsync();

            if (customerList != null)
            {
                foreach (var customer in customerList)
                {
                    _customers.Add(new CustomerViewModel(customer));
                }
            }
        }
    }
}
