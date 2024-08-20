using System;

namespace Otus.Teaching.Pcf.Administration.Core.Exceptions
{
    public class MessageNotRecievedException<T> : Exception
    {
        public MessageNotRecievedException() : base($"Message not recieved ({typeof(T).Name})") { }
    }
}
