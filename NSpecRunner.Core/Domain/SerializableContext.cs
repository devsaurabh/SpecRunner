using System;
using System.Linq;
using System.Collections.Generic;

namespace NSpecRunner.Core.Domain
{
    [Serializable]
    public class SerializableContext
    {
        public string Name;
        public int Level;
        public List<string> Tags;
        public List<SerializableExampleBase> Examples;
        public SerializableContextCollection Contexts;
        public SerializableContext Parent;
        public SerializableExampleFailureException Exception;
        public string UniqueName;

        public IEnumerable<SerializableExampleBase> AllExamples()
        {
            return Contexts.Examples().Union(Examples);
        }

        public IEnumerable<SerializableContext> AllContexts()
        {
            return new[] { this }.Union(ChildContexts());
        }

        public IEnumerable<SerializableContext> ChildContexts()
        {
            return Contexts.SelectMany(c => new[] { c }.Union(c.ChildContexts()));
        }

        public IEnumerable<SerializableExampleBase> Failures()
        {
            return AllExamples().Where(e => e.Exception != null);
        }

        public SerializableContext()
        {
            Contexts = new SerializableContextCollection();
            Examples = new List<SerializableExampleBase>();
            
        }
    }
}
