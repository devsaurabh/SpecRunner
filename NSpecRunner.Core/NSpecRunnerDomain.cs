using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NSpecRunner.Core.Domain;

namespace NSpecRunner.Core
{
    [Serializable]
    public class NSpecTestDomain
    {
        #region Private Members

        private readonly string config;
        private readonly string dll;
        
        #endregion

        #region Public Members

        public AppDomain CurrentDomain;
        
        #endregion

        #region Ctor

        public NSpecTestDomain(string dll)
        {
            this.dll = dll;
            this.config = dll + ".config";

            var setup = new AppDomainSetup();

            setup.ConfigurationFile = Path.GetFullPath(config);

            setup.ApplicationBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            CurrentDomain = AppDomain.CreateDomain("NSpecDomain.Run", null, setup);

            var type = typeof(Wrapper);

            var assemblyName = type.Assembly.GetName().Name;

            var typeName = type.FullName;

            CurrentDomain.AssemblyResolve += Resolve;
        }
        
        #endregion

        #region Public Methods

        public NSpecResultModel Run(ContextRunnerInvocator invocation, Func<ContextRunnerInvocator, NSpecResultModel> action)
        {
            var type = typeof(Wrapper);

            var assemblyName = type.Assembly.GetName().Name;

            var typeName = type.FullName;

            CurrentDomain.AssemblyResolve += Resolve;

            var wrapper = (Wrapper)CurrentDomain.CreateInstanceAndUnwrap(assemblyName, typeName);

            NSpecResultModel failures = wrapper.Execute(invocation, action);

            AppDomain.Unload(CurrentDomain);

            CurrentDomain = null;

            return failures;
        }

        public void Stop()
        {
            if (CurrentDomain != null)
            {
                AppDomain.Unload(CurrentDomain);
                CurrentDomain = null;
            }
        }

        #endregion

        #region Private Methods

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