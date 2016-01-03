using System;
using System.Collections.Generic;

namespace NSpecRunner.Core.Domain
{
    [Serializable]
    public class NSpecResultModel
    {
        public SerializableContextCollection ContextCollection { get; set; }

        public string Output { get; set; }

        public List<TagPair> Tags { get; set; }

        public NSpecResultModel()
        {
            Tags = new List<TagPair>();
        }
    }

    [Serializable]
    public class NSpecResultSummaryModel
    {
        public int TotalExecuted { get; set; }
        public int TotalFailed { get; set; }
        public int TotalErrors { get; set; }
        public decimal TimeTake { get; set; }
        public string Output { get; set; }
    }

    [Serializable]
    public class TagPair
    {
        public string Assembly { get; set; }
        public string Tag { get; set; }
    }
}
