using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class EntityNotFound : Exception
    {
        public EntityNotFound() : base() { }
        public EntityNotFound(string message) : base(message) { }
  




    }
    public class IdentifierMissing : Exception
    {
        public IdentifierMissing() 
        { }



    }
    public class Duplicates : Exception
    {
        public Duplicates()
        { }



    }
}
