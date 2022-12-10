using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter) => true;

        public abstract void Execute(object paramter);

        protected void OnExecuteChanged(object? parameter)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
