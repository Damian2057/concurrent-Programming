using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Exceptions
{
    [Serializable]
    public class IdMultiplyException : Exception
    {
        public IdMultiplyException() : base() { }
        public IdMultiplyException(string message) : base(message) { }
        public IdMultiplyException(string message, Exception inner) : base(message, inner) { }
    }
}
