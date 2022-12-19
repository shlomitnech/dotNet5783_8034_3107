using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class Exceptions : Exception
{
    public Exceptions() : base() { }
    public Exceptions(string message) : base(message) { }
    public Exceptions(string message, Exception innerException) : base(message, innerException) { }
    protected Exceptions(SerializationInfo info, StreamingContext context) : base(info, context) { }
    public override string ToString() => base.ToString();

}

[Serializable]
public class IdExistException : Exception
{
    public IdExistException() : base() { }
    public IdExistException(string message) : base(message) { }
    public IdExistException(string message, Exception inner) : base(message, inner) { }
    protected IdExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

[Serializable]
public class IdNotExistException : Exception
{
    public IdNotExistException() : base() { }
    public IdNotExistException(string message) : base(message) { }
    public IdNotExistException(string message, Exception inner) : base(message, inner) { }
    protected IdNotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

[Serializable]
public class WrongInput : Exception
{
    public WrongInput() : base() { }
    public WrongInput(string message) : base(message) { }
    public WrongInput(string message, Exception inner) : base(message, inner) { }
    protected WrongInput(SerializationInfo info, StreamingContext context) : base(info, context) { }
}


[Serializable]
public class UnfoundException : Exception
{
    public UnfoundException() : base() { }
    public UnfoundException(string message) : base(message) { }
    public UnfoundException(string message, Exception inner) : base(message, inner) { }
    protected UnfoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}



[Serializable]
public class IncorrectInput : Exception
{
    public IncorrectInput() : base() { }
    public IncorrectInput(string message) : base(message) { }
    public IncorrectInput(string message, Exception inner) : base(message, inner) { }
    protected IncorrectInput(SerializationInfo info, StreamingContext context) : base(info, context) { }
}


