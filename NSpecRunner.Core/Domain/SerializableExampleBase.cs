using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NSpecRunner.Core.Domain
{
    [Serializable]
    public class SerializableExampleBase
    {
        public string Spec;
        public List<string> Tags;
        public SerializableExampleFailureException Exception;
        public SerializableContext Context;
        public string UniqueName;

        public bool Failed()
        {
            return Exception != null;
        }
    }

    [Serializable]
    public class SerializableExampleFailureException
    {
        public Exception ExampleException { get; set; }
        //public SerializableExampleFailureException(string message, Exception innerException)
        //    : base(message, innerException) { }

        //protected SerializableExampleFailureException(SerializationInfo info, StreamingContext context) 
        //    : base(info,context) { }
    }
}
