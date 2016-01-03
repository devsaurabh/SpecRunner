using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSpecRunner.Core.Domain
{
    [Serializable]
    public class SerializableContextCollection : List<SerializableContext>
    {

        public IEnumerable<SerializableExampleBase> Failures()
        {
            return Examples().Where(e => e.Exception != null);
        }

        public IEnumerable<SerializableExampleBase> Examples()
        {
            return this.SelectMany(c => c.AllExamples());
        }

        public IEnumerable<SerializableContext> AllContexts()
        {
            return this.SelectMany(c => c.AllContexts());
        }
    }
}
