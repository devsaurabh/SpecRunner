using NSpec.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSpecRunner.GUI
{
    [Serializable]
    public class ContextWrapper
    {
        public ContextCollection Collection { get; set; }
    }
}
