using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Stores;

namespace WpfApp1.Services
{
    public class CloseModalNavigationService : INavigationService
    {
        private readonly ModalNavigationStore _navigationStore;

        public CloseModalNavigationService(ModalNavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public void Navigate()
        {
            _navigationStore.Close();
        }
    }
}
