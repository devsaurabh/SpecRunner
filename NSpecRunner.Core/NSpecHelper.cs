using NSpecRunner.Core.Domain;
using NSpecRunner.Core.Domain.Formatters;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

namespace NSpecRunner.Core
{
    public class NSpecHelper
    {
        #region Private Members

        private NSpecTestDomain _activeDomain;
        private Dictionary<string, NSpecResultModel> _loadedAssemblies;
        
        #endregion

        #region Ctor

        public NSpecHelper()
        {
            _loadedAssemblies = new Dictionary<string, NSpecResultModel>();
        }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Loads the assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public async Task<NSpecResultModel> LoadAssembly(string assembly)
        {
            var task = Task.Run(() =>
            {
                var runnerInvocation = new ContextRunnerInvocator(assembly, string.Empty);
                _activeDomain = new NSpecTestDomain(assembly);
                var result = _activeDomain.Run(runnerInvocation, t => t.BuildContexts()); ;
                _loadedAssemblies.Add(assembly, result);
                return result;
            });

            return await task;
        }

        /// <summary>
        /// Unloads the assembly.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns></returns>
        public bool UnloadAssembly(string assemblyName)
        {
            NSpecResultModel model = null;
            if (_loadedAssemblies.TryGetValue(assemblyName, out model))
            {
                _loadedAssemblies.Remove(assemblyName);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Runs the tests.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="uniqueSpecName">Name of the unique spec.</param>
        /// <param name="resultFormatter">The result formatter.</param>
        /// <returns></returns>
        public async Task<NSpecResultModel> RunTests(string assembly, string uniqueSpecName, IFormatterWithOutput resultFormatter)
        {
            var task = Task.Run(() =>
            {
                var tags = GetTags(assembly, uniqueSpecName);
                var runnerInvocation = new ContextRunnerInvocator(assembly, tags, resultFormatter, false);
                _activeDomain = new NSpecTestDomain(assembly);
                return _activeDomain.Run(runnerInvocation, t => t.Run());
            });

            return await task;
        }

        /// <summary>
        /// Runs all assembles.
        /// </summary>
        /// <param name="resultFormatter">The result formatter.</param>
        /// <returns></returns>
        public async Task<NSpecResultSummaryModel> RunAllAssembles(IFormatterWithOutput resultFormatter)
        {
            var task = Task.Run<NSpecResultSummaryModel>(() =>
            {
                StringBuilder builder = new StringBuilder();
                var assemblies = _loadedAssemblies.Keys;
                var summary = new NSpecResultSummaryModel();

                var stopWatch = new System.Diagnostics.Stopwatch();
                stopWatch.Start();

                foreach (var assembly in assemblies)
                {
                    var result = RunTests(assembly, string.Empty, resultFormatter).Result;
                    summary.TotalExecuted += result.ContextCollection.Examples().Count();
                    summary.TotalFailed += result.ContextCollection.Failures().Count();
                    summary.TotalErrors = summary.TotalFailed;
                    builder.AppendLine(result.Output);
                }
                stopWatch.Stop();
                summary.TimeTake = stopWatch.Elapsed.Seconds;
                summary.Output = builder.ToString();
                return summary;
            });
            return await task;
        }

        /// <summary>
        /// Gets the test count in assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="testName">Name of the test.</param>
        /// <returns></returns>
        public int GetTestCountInAssembly(string assembly, string testName = null)
        {
            NSpecResultModel model = null;
            if (_loadedAssemblies.TryGetValue(assembly, out model))
            {
                if (!string.IsNullOrWhiteSpace(testName))
                {
                    if (assembly == testName)
                    {
                        return model.ContextCollection.Examples().Count();
                    }
                    else
                    {
                        var context = model.ContextCollection.AllContexts().FirstOrDefault(t => t.UniqueName == testName);
                        if (context == null)
                            return 1;
                        return context.AllExamples().Count();
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Gets the test count in all the assemblies
        /// </summary>
        /// <returns></returns>
        public int GetTestCount()
        {
            int count = 0;
            foreach (var item in _loadedAssemblies.Keys)
            {
                count += _loadedAssemblies[item].ContextCollection.Examples().Count();
            }
            return count;
        }
        
        #endregion

        #region Private Methods

        private string GetTags(string assembly, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            NSpecResultModel model = null;
            if (_loadedAssemblies.TryGetValue(assembly, out model))
            {
                var context = model.ContextCollection.AllContexts().FirstOrDefault(t => t.UniqueName == name);
                if (context != null)
                    return string.Join(",", context.Tags);
                else
                {
                    var example = model.ContextCollection.Examples().FirstOrDefault(t => t.UniqueName == name);
                    if (example != null)
                        return string.Join(",", example.Tags);
                }
            }

            return string.Empty;
        }
        
        #endregion
    }
}
