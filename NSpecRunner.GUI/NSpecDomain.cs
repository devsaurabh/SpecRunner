using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NSpec.Domain;
using NSpec;
using NSpec.Domain.Formatters;
using NSpecRunner.GUI;

namespace NSpecRunner
{
    [Serializable]
    public class NSpecRunnerDomain
    {
        #region Private Members

        private readonly string config;
        private AppDomain domain;
        private string dll;
        
        #endregion

        #region Ctor

        public NSpecRunnerDomain(string config)
        {
            this.config = config;
        }
        
        #endregion

        #region Public Methods

        public ContextWrapper Run(string tagOrClassName, RunnerInvocation invocation, Func<RunnerInvocation, ContextWrapper> action, string dll)
        {
            this.dll = dll;

            var setup = new AppDomainSetup();

            setup.ConfigurationFile = Path.GetFullPath(config);

            setup.ApplicationBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            domain = AppDomain.CreateDomain("NSpecRunnerDomain.Run", null, setup);

            var type = typeof(Wrapper);

            var assemblyName = type.Assembly.GetName().Name;

            var typeName = type.FullName;

            domain.AssemblyResolve += Resolve;

            var wrapper = (Wrapper)domain.CreateInstanceAndUnwrap(assemblyName, typeName);

            var results = wrapper.Execute(invocation, action);// RunContexts(tagOrClassName);

            AppDomain.Unload(domain);

            return results;
        }

        public ContextCollection Build(string dll)
        {
            this.dll = dll;

            var setup = new AppDomainSetup();

            setup.ConfigurationFile = Path.GetFullPath(config);

            setup.ApplicationBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            domain = AppDomain.CreateDomain("NSpecDomain.Run", null, setup);

            var type = typeof(Wrapper);

            var assemblyName = type.Assembly.GetName().Name;

            var typeName = type.FullName;

            domain.AssemblyResolve += Resolve;

            var contexts = BuildContexts();

            AppDomain.Unload(domain);

            return contexts;
        }
        
        #endregion

        #region Private Methods

        private ContextCollection BuildContexts()
        {
            var reflector = new Reflector(this.dll);

            var finder = new SpecFinder(reflector);

            var builder = new ContextBuilder(finder, new DefaultConventions());

            var contexts = builder.Contexts().Build();

            if (contexts.AnyTaggedWithFocus())
            {
                builder = new ContextBuilder(finder, new Tags().Parse(NSpec.Domain.Tags.Focus), new DefaultConventions());
            }

            return builder.Contexts().Build();
        }

        private ContextCollection RunContexts(string tags)
        {
            var reflector = new Reflector(this.dll);

            var finder = new SpecFinder(reflector);

            var builder = new ContextBuilder(finder, new Tags().Parse(tags), new DefaultConventions());

            var runner = new ContextRunner(builder, new ConsoleFormatter(), false);

            var contexts = builder.Contexts().Build();

            if (contexts.AnyTaggedWithFocus())
            {
                builder = new ContextBuilder(finder, new Tags().Parse(NSpec.Domain.Tags.Focus), new DefaultConventions());

                runner = new ContextRunner(builder, new ConsoleFormatter(), false);
            }

            return runner.Run(builder.Contexts().Build());
        }

        private Assembly Resolve(object sender, ResolveEventArgs args)
        {
            var name = args.Name;

            var argNameForResolve = args.Name.ToLower();

            if (argNameForResolve.Contains(","))
                name = argNameForResolve.Split(',').First() + ".dll";
            else if (!argNameForResolve.EndsWith(".dll") && !argNameForResolve.Contains(".resource"))
                name += ".dll";
            else if (argNameForResolve.Contains(".resource"))
                name = argNameForResolve.Substring(0, argNameForResolve.IndexOf(".resource")) + ".xml";

            var missing = Path.Combine(Path.GetDirectoryName(dll), name);

            if (File.Exists(missing)) return Assembly.LoadFrom(missing);

            return null;
        }
        
        #endregion
    }
}