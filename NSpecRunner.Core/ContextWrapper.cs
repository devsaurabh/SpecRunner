using NSpecRunner.Core.Domain;
using System;
using System.Collections.Generic;

namespace NSpecRunner.Core
{
    [Serializable]
    public class ContextWrapper
    {
        public List<SerializableContext> Collection { get; set; }
    }
}
