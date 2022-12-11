using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    public class BookingViewModel
    {
        public int Id { get; }

        public BookingViewModel(int id)
        {
            Id = id;
        }
    }
}
