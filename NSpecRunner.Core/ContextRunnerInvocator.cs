using NSpec;
using NSpec.Domain;
using NSpec.Domain.Formatters;
using NSpecRunner.Core.Domain;
using NSpecRunner.Core.Domain.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NSpec.Domain.Extensions;

namespace NSpecRunner.Core
{
    [Serializable]
    public class ContextRunnerInvocator
    {
        #region Private Members

        private readonly string _tags;
        private readonly IFormatterWithOutput _formatter;
        private readonly string _dllFile;
        private readonly bool _failFast;
        private readonly List<string> _tagsCollection;

        #endregion

        #region Ctor

        public ContextRunnerInvocator(string dll, string tags)
            : this(dll, tags, false) { }

        public ContextRunnerInvocator(string dll, string tags, bool failFast)
            : this(dll, tags, new StringFormatter(), failFast) { }

        public ContextRunnerInvocator(string dll, string tags, IFormatterWithOutput formatter, bool failFast)
        {
            this._dllFile = dll;
            this._failFast = failFast;
            _tags = tags.Trim().Replace(" ","_");
            _formatter = formatter;
            _tagsCollection = new List<string>();
        }

        #endregion

        #region Public Methods

        public NSpecResultModel Run()
        {
            var reflector = new Reflector(this._dllFile);

            var finder = new SpecFinder(reflector);

            var builder = new ContextBuilder(finder, new Tags().Parse(_tags), new DefaultConventions());

            var runner = new ContextRunner(builder, _formatter, false);

            var contexts = builder.Contexts().Build();

            if (contexts.AnyTaggedWithFocus())
            {
                builder = new ContextBuilder(finder, new Tags().Parse(NSpec.Domain.Tags.Focus), new DefaultConventions());

                runner = new ContextRunner(builder, _formatter, true);
            }

            var contextCollection = runner.Run(contexts);
            
            var serializableContextCollection = new SerializableContextCollection();
            
            BuildResponse(serializableContextCollection, contextCollection);
            
            return new NSpecResultModel { ContextCollection = serializableContextCollection, Output = _formatter.GetFormattedString };
        }

        public NSpecResultModel Run(string specName)
        {
            var reflector = new Reflector(this._dllFile);
            var nspecInstance = new nspec();
            var conventions = new DefaultConventions();
            var finder = new SpecFinder(reflector);

            var builder = new ContextBuilder(finder,new Tags().Parse(_tags) , new DefaultConventions());
            var contexts = builder.Contexts().Build();

            var context = contexts.AllContexts().FirstOrDefault(t => t.Name == specName);
            var parentTypeInstance = contexts.AllContexts().FirstOrDefault(t => t is ClassContext);
            
            if (context != null && !context.HasAnyExecutedExample() && parentTypeInstance != null)
            {
                ILiveFormatter liveFormatter = new SilentLiveFormatter();

                if (_formatter is ILiveFormatter) liveFormatter = _formatter as ILiveFormatter;

                var instance = (parentTypeInstance as ClassContext).type.Instance<nspec>();
                context.Contexts.Where(t => t is MethodContext).Do(t => (t as MethodContext).Build(instance));
             
                context.Run(_formatter as ILiveFormatter, false, instance);
                context.AssignExceptions();

                if (builder.tagsFilter.HasTagFilters())
                {
                    context.TrimSkippedDescendants();
                }
            }

            var contextCollection = new ContextCollection { context };
            _formatter.Write(contextCollection);
            var serializableContextCollection = new SerializableContextCollection();
            BuildResponse(serializableContextCollection, contextCollection);
            return new NSpecResultModel { ContextCollection = serializableContextCollection, Output = _formatter.GetFormattedString };
        }

        public NSpecResultModel BuildContexts()
        {
            var reflector = new Reflector(this._dllFile);

            var finder = new SpecFinder(reflector);

            var builder = new ContextBuilder(finder, new Tags().Parse(_tags), new DefaultConventions());

            var contexts = builder.Contexts().Build();

            if (contexts.AnyTaggedWithFocus())
            {
                builder = new ContextBuilder(finder, new Tags().Parse(NSpec.Domain.Tags.Focus), new DefaultConventions());
            }

            var contextCollection = builder.Contexts().Build();
            var serializableContextCollection = new SerializableContextCollection();
            BuildResponse(serializableContextCollection, contextCollection);
            var tags = _tagsCollection.Distinct().Select(t => new TagPair { Tag = t, Assembly = _dllFile }).ToList();
            return new NSpecResultModel { ContextCollection = serializableContextCollection, Output = string.Empty, Tags = tags };
        }
        
        #endregion

        #region Private Methods

        private void BuildResponse(SerializableContextCollection contextCollection, ContextCollection collection)
        {
            foreach (var rootContext in collection)
            {
                // create a context
                var context = (rootContext as ClassContext) != null ? new SerializableClassContext() : new SerializableContext();
                context.Name = rootContext.Name;
                context.Parent = new SerializableContext { Name = rootContext.Parent.Name };
                context.Exception = rootContext.Failures().Count() > 0 ? new SerializableExampleFailureException { ExampleException = rootContext.Failures().Select(t => t.Exception as Exception).FirstOrDefault().GetBaseException() } : null;
                context.Tags = rootContext.Tags;
                context.Level = rootContext.Level;
                context.UniqueName = GetExampleName(rootContext, string.Empty);
                _tagsCollection.AddRange(rootContext.Tags);

                context.Examples = rootContext.Examples != null ? rootContext.Examples.Select(t => new SerializableExampleBase
                {
                    Spec = t.Spec,
                    Exception = t.Exception != null ? new SerializableExampleFailureException { ExampleException = t.Exception.GetBaseException() } : null,
                    Tags = t.Tags,
                    UniqueName = GetExampleName(t.Context, t.Spec),
                    Context = new SerializableContext { Name = rootContext.Parent.Name }
                }).ToList() : null;

                _tagsCollection.AddRange(rootContext.AllExamples().SelectMany(t => t.Tags));
                if (rootContext.Contexts.Count > 0)
                {
                    context.Contexts = new SerializableContextCollection();
                    BuildResponse(context.Contexts, rootContext.Contexts);
                }
                contextCollection.Add(context);
            }
        }

        private void GenerateUniqueExampleName(Context context, ref string output, Example ex)
        {
            output += "-" + context.Name;
            if (context.Parent != null)
                GenerateUniqueExampleName(context.Parent, ref output, ex);
            else
                output = ex.Spec + output;
        }


        private string GetExampleName(Context context,string specName)
        {
            string name = string.Empty;
            Utilities.GenerateUniqueExampleName(context, ref name, specName);
            return name;
        }

        #endregion
    }
}
