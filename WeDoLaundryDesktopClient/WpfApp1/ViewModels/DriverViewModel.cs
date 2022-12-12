using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    public class DriverViewModel
    {
        public int Id { get; }

        public DriverViewModel(int id)
        {
            Id = id;
        }
    }
}
