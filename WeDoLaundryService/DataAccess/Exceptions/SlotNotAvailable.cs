using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Exceptions
{
    public class SlotNotAvailable : Exception
    {
        public SlotNotAvailable()
        {
        }

        public SlotNotAvailable(string message)
            : base(message)
        {
        }

        public SlotNotAvailable(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
