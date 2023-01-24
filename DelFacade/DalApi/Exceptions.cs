using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public class Exceptions : Exception
    {
        public Exceptions() : base() { }
        public Exceptions(string message) : base(message) { }

    }
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
        public Duplicates(string v)
        { }

    }
    [Serializable]
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }

}
