using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using NSpecRunner.GUI.Framework;
using NSpecRunner.Core;
using NSpecRunner.Core.Domain;
using NSpecRunner.Core.Domain.Formatters;
using System.Diagnostics;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.IO;

namespace NSpecRunner.GUI
{
    public partial class SpecRunner : Form
    {
        #region Private Members

        private NSpecHelper _nspecHelper;
        private bool _isReady;
        private List<string> _assemblies;
        private List<TreeNode> _allCurrentSpecNames;
        public int _testCount, _failedCount;
        private Dictionary<string, string> _exceptions;
        private string _projectName;
        private bool _isFileSaved;

        #endregion

        #region Ctor

        public SpecRunner()
        {
            InitializeComponent();
            _nspecHelper = new NSpecHelper();
            _allCurrentSpecNames = new List<TreeNode>();
            _assemblies = new List<string>();
            _isReady = false;
            _exceptions = new Dictionary<string, string>();
        }
        
        #endregion

        #region Private Events

        /// <summary>
        /// Handles the Load event of the SpecRunner control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SpecRunner_Load(object sender, EventArgs e)
        {
            _isFileSaved = true;
            _isReady = true;
            _projectName = ApplicationConstants.DEFAULT_PROJECT_NAME;
            SetControls();
        }

        /// <summary>
        ///  Reload the current assemblies
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReloadMenuItem_Click(object sender, EventArgs e)
        {
            treeSpecs.Nodes[0].Nodes.Clear();
            treeSpecs.Nodes[0].SelectedImageKey = treeSpecs.Nodes[0].ImageKey = ImageTypes.PENDING;
            _nspecHelper = new NSpecHelper();
            _assemblies.ForEach(t => LoadAssembly(t));
            SetControls();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Spec Project|*.nsproj";

            DialogResult dialogResult = dialog.ShowDialog();
            if (dialogResult == DialogResult.OK || dialogResult == DialogResult.Yes)
            {
                var projFile = dialog.FileName;
                SaveFile(projFile);
                _isFileSaved = true;
            }
            else
            {
                _isFileSaved = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OpenProjectMenuItem_Click(object sender, EventArgs e)
        {
            if (_isFileSaved || ShowSaveFilePrompt())
            {
                var fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Spec Pojects|*.nsproj";
                DialogResult dialogResult = fileDialog.ShowDialog();
                if (dialogResult == System.Windows.Forms.DialogResult.OK || dialogResult == DialogResult.Yes)
                {
                    XElement element = XElement.Load(fileDialog.FileName);
                    if (element != null)
                    {
                        // unload the existing project
                        UnloadProject();

                        // set controls
                        SetControls();

                        // load the assemblies
                        _projectName = fileDialog.FileName;

                        foreach (var assembly in _assemblies)
                        {
                            bool isLoaded = await LoadAssembly(assembly);
                            _assemblies.AddRange(element.Elements().Select(t => t.Value).Where(t => File.Exists(t)));
                        }
                        
                        // enable controls
                        SetControls();
                    }
                }
            }
        }

        /// <summary>
        ///  Handles the click even from the Save Project Menu Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveProjectMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile(_projectName);
        }

        /// <summary>
        ///  Handles the click even from the Close Project Menu Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseProjectMenuItem_Click(object sender, EventArgs e)
        {
            if (_isFileSaved || ShowSaveFilePrompt())
            {
                UnloadProject();
                SetControls();
                _projectName = ApplicationConstants.DEFAULT_PROJECT_NAME;
                this.SetTitle();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstErrors_Click(object sender, EventArgs e)
        {
            var firstSelectedItem = lstErrors.SelectedItems[0];
            string error = string.Empty;
            if (_exceptions.TryGetValue(firstSelectedItem.Name, out error))
            {
                txtStackTrace.Text = error;
            }
           
        }

        /// <summary>
        ///  Handles the AfterSElect even of the Tree View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeSpecs_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (_isReady)
            {
                if (treeSpecs.SelectedNode != null && treeSpecs.SelectedNode.Level > 0)
                    CloseAssemblyMenuItem.Enabled = true;

                lblSelectedNode.Text = e.Node.Text;

                if (e.Node.Parent == null)
                    lblTestCount.Text = "Test Cases: " + _nspecHelper.GetTestCount();
                else
                {
                    string assembly = string.Empty;
                    GetTestAssembly(e.Node, ref assembly);
                    lblTestCount.Text = "Test Cases: " + _nspecHelper.GetTestCountInAssembly(assembly, e.Node.Name);
                }
            }
        }

        /// <summary>
        ///  Triggers when user clicks on the Tree view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeSpecs_MouseClick(object sender, MouseEventArgs e)
        {
            TreeNode selectedNode = treeSpecs.HitTest(e.Location).Node;
            if (selectedNode.ForeColor == Color.Gray)
                return;
                

            if (e.Button == MouseButtons.Right)
            {
                treeSpecs.SelectedNode = selectedNode;
                if (_isReady)
                {
                    if (selectedNode.Parent == null)
                        TreeSpecContextMenu.Items[0].Enabled = false;
                    else
                    {
                        TreeSpecContextMenu.Items[0].Enabled = true;
                        TreeSpecContextMenu.Items[1].Enabled = true;
                    }
                }
                else
                {
                    TreeSpecContextMenu.Items[0].Enabled = false;
                    TreeSpecContextMenu.Items[1].Enabled = false;
                }
            }
        }

        /// <summary>
        ///  Unloads the active assembly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAssemblyMenuItem_Click(object sender, EventArgs e)
        {
            if (treeSpecs.SelectedNode != null)
            {
                string assemblyName = string.Empty;
                GetTestAssembly(treeSpecs.SelectedNode, ref assemblyName);

                if (_nspecHelper.UnloadAssembly(assemblyName))
                {
                    _assemblies.Remove(assemblyName);
                    for (int i = 0; i < treeSpecs.Nodes[0].Nodes.Count; i++)
                    {
                        // remove the nodes from treeview
                        if (treeSpecs.Nodes[0].Nodes[i].Name == assemblyName)
                        {
                            treeSpecs.Nodes[0].Nodes[i].Remove();

                        }
                    }
                    var defaultStatus = ImageTypes.PENDING;
                    if (treeSpecs.Nodes[0].Nodes.Count > 0)
                    {
                        //var defaultStatus = ImageTypes.PENDING;
                        foreach (TreeNode assemblyNode in treeSpecs.Nodes[0].Nodes)
                        {
                            if (assemblyNode.ImageKey == ImageTypes.FAILED) { defaultStatus = ImageTypes.FAILED; break; }
                            else if (assemblyNode.ImageKey == ImageTypes.PASSED) defaultStatus = ImageTypes.PASSED;

                        }
                    }
                    else
                    {
                        UnloadProject();
                        SetControls();
                    }
                    treeSpecs.Nodes[0].SelectedImageKey = treeSpecs.Nodes[0].ImageKey = defaultStatus;
                }

                
            }
        }

        /// <summary>
        ///  Adds the assembly file in the current project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddAssemblyMenuItem_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Spec Files|*.dll|Executables|*.exe";
            DialogResult dialogResult = fileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!_assemblies.Contains(fileDialog.FileName))
                {
                    lblFileName.Text = fileDialog.FileName;
                    var isLoaded = await LoadAssembly(fileDialog.FileName);
                    if (isLoaded) _assemblies.Add(fileDialog.FileName);
                    _isFileSaved = !isLoaded;
                    SetControls();
                }
            }
        }

        /// <summary>
        ///  Exits the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            if (_isFileSaved || ShowSaveFilePrompt())
            {
                _isFileSaved = true;
                UnloadProject();
                Application.Exit();
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the SpecRunner control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void SpecRunner_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(_isFileSaved || ShowSaveFilePrompt())) e.Cancel = true;

        }

        /// <summary>
        /// Handles the Click event of the AboutMenuStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AboutMenuStrip_Click(object sender, EventArgs e)
        {
            var frmAbout = new About();
            frmAbout.ShowDialog();
        }

        /// <summary>
        ///  Handles the Click event of the btnRunSelected control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnRunSelected_Click(object sender, EventArgs e)
        {
            _exceptions = new Dictionary<string, string>();
            txtStackTrace.Text = string.Empty;
            _failedCount = _testCount = 0;
            txtResult.Text = string.Empty;
            lstErrors.Groups.Clear();
            lstErrors.Items.Clear();
            string selectedTag = string.Empty;
            string selectedAssembly = string.Empty;
            int totalExamples = 0;
            var formatter = new StringFormatter();
            formatter.ExampleProcessed += On_ExampleProcessed;
            formatter.TestClassProcessed += On_TestClassProcessed;

            this.pbTestProgress.Value = 0;
            this.lblTestDetail.Text = string.Empty;


            if (treeSpecs.SelectedNode != null)
            {
                lblFileName.Text = string.Format("Initializing - {0}",treeSpecs.SelectedNode.Text);

                // if we are at the root run all assembles
                if (treeSpecs.SelectedNode.Level == 0)
                {
                    totalExamples = _nspecHelper.GetTestCount();
                    this.pbTestProgress.Maximum = totalExamples;
                    var summary = await _nspecHelper.RunAllAssembles(formatter);
                    
                    this.InvokeAction(() => txtResult.AppendLine(0, summary.Output, Color.Black));
                    this.InvokeAction(() => lblTestDetail.Text = string.Format("Total Assemblies: {0}  Total Tests {1}  Passed: {2}  Failed: {3}  Time: {4}",
                        _assemblies.Count(),
                        summary.TotalExecuted,
                        summary.TotalExecuted - summary.TotalFailed,
                        summary.TotalFailed,
                        summary.TimeTake));
                }
                else
                {
                    if (treeSpecs.SelectedNode.Level == 1)
                    {
                        selectedAssembly = treeSpecs.SelectedNode.Name;
                        totalExamples = _nspecHelper.GetTestCountInAssembly(selectedAssembly,selectedAssembly);
                    }
                    else
                    {
                        selectedTag = treeSpecs.SelectedNode.Name;
                        GetTestAssembly(treeSpecs.SelectedNode, ref selectedAssembly);
                        totalExamples = _nspecHelper.GetTestCountInAssembly(selectedAssembly, treeSpecs.SelectedNode.Name);
                        List<TreeNode> nodes = new List<TreeNode>();
                        GetTreeLeafs(treeSpecs.SelectedNode, nodes);
                        formatter.ExamplesToProcess = nodes.Select(t => t.Name).ToList();
                    }

                    this.pbTestProgress.Maximum = totalExamples;
                    Stopwatch watch = new Stopwatch();

                    watch.Start();
                    var lastResultCollection = await _nspecHelper.RunTests(selectedAssembly, selectedTag , formatter);
                    watch.Stop();

                    this.InvokeAction(() => txtResult.AppendLine(0, lastResultCollection.Output, Color.Black));
                    this.InvokeAction(() => lblTestDetail.Text = string.Format("Total Test: {3}  Passed: {0}  Failed: {1}  Time: {2}",
                        _testCount - _failedCount,
                        _failedCount,
                        watch.Elapsed.TotalSeconds,
                        _testCount
                        ));
                }
                lblFileName.Text = string.Format("Completed");
                _allCurrentSpecNames = null;
            }
            else
            {
                MessageBox.Show("Please select a test node");
            }
        }

        /// <summary>
        /// Called when [test class processed].
        /// </summary>
        /// <param name="runningClass">The running class.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void On_TestClassProcessed(string runningClass, EventArgs e)
        {
            _allCurrentSpecNames = new List<TreeNode>();
            var node = treeSpecs.Nodes.Find(runningClass, true).FirstOrDefault();
            if (node != null)
                GetTreeLeafs(node, _allCurrentSpecNames);
        }

        /// <summary>
        /// Raises the <see cref="E:ExampleProcessed" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ExampleEventArgs"/> instance containing the event data.</param>
        void On_ExampleProcessed(ExampleEventArgs e)
        {
            this.InvokeAction(() =>
            {
                var selectedNode = treeSpecs.SelectedNode;
                if (pbTestProgress.Value < pbTestProgress.Maximum)
                    pbTestProgress.Value++;
                if (selectedNode != null && _allCurrentSpecNames != null)
                {
                    var matchingExample = _allCurrentSpecNames.FirstOrDefault(t => t.Name == e.Example.UniqueName);

                    if (matchingExample != null)
                    {
                        _testCount++;
                        string resultType = e.Example.Exception != null ? ImageTypes.FAILED : ImageTypes.PASSED;
                        matchingExample.ImageKey = matchingExample.SelectedImageKey = resultType;
                        TraverseTillRoot(matchingExample, resultType);

                        if (e.Example.Exception != null)
                        {
                            _failedCount++;
                            var errorList = GetErrorItems(e.Example);
                            errorList.ForEach(itm => lstErrors.Items.Add(itm));

                            var errorGroup = new ListViewGroup("Example: " + e.Example.Spec);
                            errorGroup.Name = e.Example.UniqueName;
                            lstErrors.Groups.Add(errorGroup);
                            errorList.ForEach(itm => itm.Group = errorGroup);

                            _exceptions.Add(e.Example.UniqueName, e.Example.Exception.ExampleException.StackTrace);
                        }

                        lblFileName.Text = string.Format("Running - {0}/{1}/{2}", selectedNode.Text, e.Example.Context.Name, e.Example.Spec);
                    }

                }
            });
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the assembly.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private async Task<bool> LoadAssembly(string fileName)
        {
            _isReady = false;
            var parentNode = new TreeNode(fileName);
            parentNode.Name = fileName;

            var specCollection = await _nspecHelper.LoadAssembly(fileName);

            foreach (SerializableContext classContext in specCollection.ContextCollection)
            {
                TreeNode classNode = new TreeNode();
                classNode.Text = classContext.Name;
                classNode.Name = classContext.UniqueName; 
                classNode.ImageKey = ImageTypes.PENDING;
                classNode.SelectedImageKey = ImageTypes.PENDING;
                classNode.Tag = classContext.Tags;
                var tags = classContext.Tags;

                foreach (SerializableContext methodContext in classContext.Contexts)
                {
                    TreeNode methodNode = new TreeNode(methodContext.Name);
                    methodNode.Name = methodContext.UniqueName;
                    methodNode.ImageKey = ImageTypes.PENDING;
                    methodNode.Tag = methodContext.Tags;
                    methodNode.SelectedImageKey = ImageTypes.PENDING;

                    foreach (var act in methodContext.Examples)
                    {
                        var actNode = new TreeNode(act.Spec);
                        actNode.Name = act.UniqueName;
                        actNode.ImageKey = ImageTypes.PENDING;
                        actNode.SelectedImageKey = ImageTypes.PENDING;
                        actNode.Tag = act.Tags;
                        methodNode.Nodes.Add(actNode);
                    }
                    
                    BuildChildNodes(methodContext, methodNode);
                    classNode.Nodes.Add(methodNode);
                }

                foreach (var act in classContext.Examples)
                {
                    var actNode = new TreeNode(act.Spec);
                    actNode.Name = act.UniqueName;
                    actNode.ImageKey = ImageTypes.PENDING;
                    actNode.SelectedImageKey = ImageTypes.PENDING;
                    actNode.Tag = act.Tags;
                    classNode.Nodes.Add(actNode);
                }

                parentNode.Nodes.Add(classNode);
            }

            _isReady = true;

            if (parentNode.Nodes.Count > 0)
            {
                parentNode.Expand();
                treeSpecs.Nodes[0].Nodes.Add(parentNode);
                if (_projectName == ApplicationConstants.DEFAULT_PROJECT_NAME) this.SetTitle(ApplicationConstants.NEW_PROJECT_NAME);
                else this.SetTitle(_projectName);
                SetControls();
                return true;
            }
            else
            {
                //_projectName = ApplicationConstants.DEFAULT_PROJECT_NAME;
                SetControls();
                return false;
            }
        }

        /// <summary>
        /// Builds the child nodes.
        /// </summary>
        /// <param name="methodContext">The method context.</param>
        /// <param name="methodNode">The method node.</param>
        private void BuildChildNodes(SerializableContext methodContext, TreeNode methodNode)
        {
            foreach (var context in methodContext.Contexts)
            {
                var node = new TreeNode(context.Name);
                node.Name = context.UniqueName;
                node.ImageKey = ImageTypes.PENDING;
                node.SelectedImageKey = ImageTypes.PENDING;
                node.Tag = context.Tags;
                methodNode.Nodes.Add(node);

                foreach (var act in context.Examples)
                {
                    var actNode = new TreeNode(act.Spec);
                    actNode.Name = act.UniqueName;
                    actNode.ImageKey = ImageTypes.PENDING;
                    actNode.SelectedImageKey = ImageTypes.PENDING;
                    actNode.Tag = act.Tags;

                    node.Nodes.Add(actNode);
                }

                if (context.Contexts.Count > 0)
                {
                    BuildChildNodes(context, node);
                }
            }
        }

        /// <summary>
        /// Gets the test assembly from the current tree node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private void GetTestAssembly(TreeNode node, ref string parent)
        {
            if (node.Parent != null)
            {
                if (node.Parent.Parent != null)
                {
                    GetTestAssembly(node.Parent, ref parent);
                }
                else
                {
                    parent = node.Name;
                    return;
                }
            }
        }

        /// <summary>
        ///  Sets the status of various controls
        /// </summary>
        private void SetControls()
        {
            if (_isReady)
            {
                // new button enable

                // open project button
                OpenProjectMenuItem.Enabled = true;

                // close project button
                if (_projectName == ApplicationConstants.DEFAULT_PROJECT_NAME) CloseProjectMenuItem.Enabled = false;
                else CloseProjectMenuItem.Enabled = true;

                // add assembly button
                AddAssemblyMenuItem.Enabled = true;

                // close assembly button
                if (_assemblies.Count > 0 && treeSpecs.SelectedNode != null && treeSpecs.SelectedNode.Level > 1) CloseAssemblyMenuItem.Enabled = true;
                else CloseAssemblyMenuItem.Enabled = false;

                // save project button
                if (!_isFileSaved && _projectName != ApplicationConstants.DEFAULT_PROJECT_NAME) SaveProjectMenuItem.Enabled = true;
                else SaveProjectMenuItem.Enabled = false;

                // save as project button
                if (_assemblies.Count > 0) SaveAsMenuItem.Enabled = true;
                else SaveAsMenuItem.Enabled = false;

                // reload button
                if (_assemblies.Count > 0) ReloadMenuItem.Enabled = true;
                else ReloadMenuItem.Enabled = false;
            }
            else
            {
                OpenProjectMenuItem.Enabled = false;
                CloseProjectMenuItem.Enabled = false;
                AddAssemblyMenuItem.Enabled = false;
                CloseAssemblyMenuItem.Enabled = false;
                SaveProjectMenuItem.Enabled = false;
                SaveAsMenuItem.Enabled = false;
                ReloadMenuItem.Enabled = false;
            }

            txtResult.Text = string.Empty;
            lstErrors.Items.Clear();
            lstErrors.Groups.Clear();
            txtStackTrace.Text = string.Empty;
            lblTestDetail.Text = string.Empty;
            pbTestProgress.Value = 0;
            _exceptions.Clear();
            _failedCount = _testCount = 0;
        }

        /// <summary>
        /// Traverses the tree view to find the root most element from the selected node
        /// Used to mark the node status as failed or pass once the test is executed
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="resultType">Type of the result.</param>
        private void TraverseTillRoot(TreeNode node, string resultType)
        {
            if (node.Parent != null)
            {
                if (node.Parent.ImageKey != ImageTypes.FAILED)
                {
                    node.Parent.ImageKey = node.Parent.SelectedImageKey = resultType;
                    TraverseTillRoot(node.Parent,resultType);
                }
            }
        }

        /// <summary>
        /// Gets the tree leafs.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="leafCollection">The leaf collection.</param>
        void GetTreeLeafs(TreeNode node, List<TreeNode> leafCollection)
        {
            if (node.Nodes != null && node.Nodes.Count > 0)
            {
                foreach (TreeNode childNode in node.Nodes)
                {   
                    GetTreeLeafs(childNode, leafCollection);
                }
            }
            else
            {
                leafCollection.Add(node);
            }
        }

        /// <summary>
        /// Gets the error items.
        /// </summary>
        /// <param name="example">The example.</param>
        /// <returns></returns>
        public List<ListViewItem> GetErrorItems(SerializableExampleBase example)
        {
            var collection = new List<ListViewItem>(); 
            var exception = example.Exception.ExampleException;
            
            var listViewItem = new ListViewItem();
            listViewItem.Name = example.UniqueName;
            listViewItem.Text = "Name";
            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, example.Tags.First()));
            collection.Add(listViewItem);

            listViewItem = new ListViewItem();
            listViewItem.Name = example.UniqueName;
            listViewItem.Text = "Error Message";
            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, exception.Message));
            collection.Add(listViewItem);

            listViewItem = new ListViewItem();
            listViewItem.Name = example.UniqueName;
            listViewItem.Text = "Stack Trace";
            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, exception.StackTrace));
            collection.Add(listViewItem);
            return collection;
        }

        /// <summary>
        ///  Save the project
        /// </summary>
        /// <param name="projFile"></param>
        private void SaveFile(string projFile)
        {
            XElement projectFiles = new XElement("Files");
            _assemblies.ForEach(t =>
            {
                projectFiles.Add(new XElement("File", t));
            });

            projectFiles.Save(projFile);
            SaveProjectMenuItem.Enabled = true;
            _projectName = projFile;
            this.SetTitle(_projectName);
        }

        /// <summary>
        ///  Unload the current loaded project
        /// </summary>
        private void UnloadProject()
        {
            _assemblies.ForEach(t => _nspecHelper.UnloadAssembly(t));
            treeSpecs.Nodes[0].Nodes.Clear();
            _assemblies = new List<string>();
            SetControls();
        }


        private void NewProject()
        {
            // show the warning if the project is unsaved


            // show the save as dialog if the file is not been saved previous
        }

        /// <summary>
        ///  Shows the save alert if file is not saved
        /// </summary>
        private bool ShowSaveFilePrompt()
        {
            var dialogResult = MessageBox.Show("There are unsaved changes. Do you want to save?","Save Changes",MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes || dialogResult == DialogResult.OK)
            {
                if (_projectName == ApplicationConstants.DEFAULT_PROJECT_NAME)
                {
                    SaveAsMenuItem_Click(this, null);
                    return _isFileSaved;
                }
                else
                {
                    SaveFile(_projectName);
                    _isFileSaved = true;
                    return true;
                }
            }
            return true;
        }

        #endregion
    }
}
