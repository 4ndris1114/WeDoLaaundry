using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model_layer
{
    public class Driver
    {

        public enum DriverStatus
        {
            FREE = 0,
            BUSY = 1,
            WORKING = 2
        }

        public int Id { get; set; }
        public int OrderId { get; set; }

        public Driver() { }

        public Driver(int id,int orderId)
        {
            Id = id;
            OrderId = orderId;
        } 
    }
}
