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

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for AddTimeslotView.xaml
    /// </summary>
    public partial class AddTimeslotView : UserControl
    {
        public AddTimeslotView()
        {
            InitializeComponent();
            dateCalendar.BlackoutDates.Add(new CalendarDateRange(new DateTime(1900, 1, 1), DateTime.Now.AddDays(-1)));
        }
    }
}
