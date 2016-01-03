using System;
using System.Linq;
using NSpec.Domain.Extensions;
using NSpec;
using System.Collections.Generic;
using NSpec.Domain.Formatters;
using System.Text;
using NSpec.Domain;

namespace NSpecRunner.Core.Domain.Formatters
{
    [Serializable]
    public class StringFormatter : IFormatter, ILiveFormatter, IFormatterWithOutput
    {
        #region Private Members

        private StringBuilder _output;
        private string indent = "  ";
        private string[] internalNameSpaces =
            new[]
                {
                    "NSpec.Domain",
                    "NSpec.AssertionExtensions",
                    "NUnit.Framework",
                    "NSpec.Extensions"
                };
        
        #endregion

        #region Public Members

        public List<string> ExamplesToProcess { get; set; }
        public Func<string, StringBuilder> WriteLineDelegate { get; set; }
        public string GetFormattedString { get { return _output.ToString(); } }
        
        #endregion

        #region Events

        public event ExampleHandler ExampleProcessed;
        public event ClassContextHandler TestClassProcessed;
        public EventArgs eventArgs = null;
        public delegate void ExampleHandler(ExampleEventArgs e);
        public delegate void ClassContextHandler(string runningClass, EventArgs e);
        
        #endregion

        #region Ctor

        public StringFormatter()
        {
            _output = new StringBuilder();
            WriteLineDelegate = _output.AppendLine;
            ExamplesToProcess = new List<string>();
        }
        
        #endregion

        #region Public Methods

        public void Write(ContextCollection contexts)
        {
            WriteLineDelegate(FailureSummary(contexts));
            WriteLineDelegate(Summary(contexts));
        }

        public void Write(Context context)
        {
            if (ExamplesToProcess.Count == 0 || context.AllExamples().Any(t => ExamplesToProcess.Contains(GetSpecName(t))))
            {
                if (context.Level == 1) WriteLineDelegate("");


                WriteLineDelegate(indent.Times(context.Level - 1) + context.Name);

                if (context is ClassContext && TestClassProcessed != null)
                {
                    string name = string.Empty;
                    Utilities.GenerateUniqueExampleName(context, ref name);
                    TestClassProcessed(name, eventArgs);
                }
            }
        }

        public void Write(ExampleBase e, int level)
        {
            string name = string.Empty;
            Utilities.GenerateUniqueExampleName(e.Context, ref name, e.Spec);
            if (ExamplesToProcess.Count == 0 || ExamplesToProcess.Contains(GetSpecName(e)))
            {
                var noFailure = e.Exception == null;
                var failureMessage = noFailure ? "" : " - FAILED - {0}".With(e.Exception.CleanMessage());
                var whiteSpace = indent.Times(level);
                var result = e.Pending ? whiteSpace + e.Spec + " - PENDING" : whiteSpace + e.Spec + failureMessage;
                WriteLineDelegate(result);

                if (ExampleProcessed != null)
                    ExampleProcessed(new ExampleEventArgs(e));
            }
        }

        public string FailureSummary(ContextCollection contexts)
        {
            if (contexts.Failures().Count() == 0) return "";

            var summary = "\n" + "**** FAILURES ****" + "\n";
            contexts.Failures().Where(t => ExamplesToProcess.Count == 0 || ExamplesToProcess.Contains(GetSpecName(t))).Do(f => summary += WriteFailure(f));
            return summary;
        }

        public string WriteFailure(ExampleBase example)
        {
            var failure = "\n" + example.FullName().Replace("_", " ") + "\n";
            failure += example.Exception.CleanMessage() + "\n";
            var stackTrace = FailureLines(example.Exception);
            stackTrace.AddRange(FailureLines(example.Exception.InnerException));
            var flattenedStackTrace = stackTrace.Flatten("\n").TrimEnd() + "\n";
            failure += example.Context.GetInstance().StackTraceToPrint(flattenedStackTrace);
            return failure;
        }

        List<string> FailureLines(Exception exception)
        {
            if (exception == null) return new List<string>();

            return exception
                .GetOrFallback(e => e.StackTrace, "")
                .Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Where(l => !internalNameSpaces.Any(l.Contains)).ToList();
        }

        public string Summary(ContextCollection contexts)
        {
            var condition = new Func<ExampleBase, bool>(t => ExamplesToProcess.Count == 0 || ExamplesToProcess.Contains(GetSpecName(t)));

            var summary = "{0} Examples, {1} Failed, {2} Pending".With(
                contexts.Examples().Where(condition).Count(),
                contexts.Failures().Where(condition).Count(),
                contexts.Pendings().Where(condition).Count()
                );

            if (contexts.AnyTaggedWithFocus())
            {
                summary += "\n" + "\n" + @"NSpec found context/examples tagged with ""focus"" and only ran those.";
            }

            return summary;
        }

        public string FocusNotification(ContextCollection contexts)
        {
            return "";
        }
        
        #endregion

        #region Private Methods

        private string GetSpecName(ExampleBase e)
        {
            string name = string.Empty;
            Utilities.GenerateUniqueExampleName(e.Context, ref name, e.Spec);
            return name;
        }
        
        #endregion
        
    }

    [Serializable]
    public class ExampleEventArgs : EventArgs
    {
        public SerializableExampleBase Example;

        public ExampleEventArgs(ExampleBase ex)
        {
            string name = string.Empty;
            Utilities.GenerateUniqueExampleName(ex.Context,ref name,ex.Spec);
            Example = new SerializableExampleBase
            {
                Spec = ex.Spec,
                Exception = ex.Exception != null ? new SerializableExampleFailureException { ExampleException = ex.Exception.GetBaseException() } : null,
                Context = new SerializableContext { Name = ex.Context.Name, Tags = ex.Context.Tags },
                Tags = ex.Tags,
                UniqueName = name
            };
        }
    }
}

