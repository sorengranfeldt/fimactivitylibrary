using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using Microsoft.ResourceManagement.WebServices.WSResourceManagement;
using Microsoft.ResourceManagement.Workflow.Activities;

namespace Granfeldt.FIM.ActivityLibrary
{
    public partial class CodeActivity : SequenceActivity
    {

        #region Properties

        public static DependencyProperty TargetResourceProperty = DependencyProperty.Register("TargetResource", typeof(Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType), typeof(Granfeldt.FIM.ActivityLibrary.CodeActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Parameters")]
        public Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType TargetResource
        {
            get
            {
                return ((Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType)(base.GetValue(Granfeldt.FIM.ActivityLibrary.CodeActivity.TargetResourceProperty)));
            }
            set
            {
                base.SetValue(Granfeldt.FIM.ActivityLibrary.CodeActivity.TargetResourceProperty, value);
            }
        }


        /// <summary>
        /// The title of the current instance of the workflow
        /// </summary>
        public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(CodeActivity));
        [Description("Title")]
        [Category("Title Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Title
        {
            get
            {
                return ((string)(base.GetValue(CodeActivity.TitleProperty)));
            }
            set
            {
                base.SetValue(CodeActivity.TitleProperty, value);
            }
        }

        public static DependencyProperty ReferencesProperty = DependencyProperty.Register("References", typeof(string[]), typeof(CodeActivity));
        [Description("References")]
        [Category("References Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string[] References
        {
            get
            {
                return ((string[])(base.GetValue(CodeActivity.ReferencesProperty)));
            }
            set
            {
                base.SetValue(CodeActivity.ReferencesProperty, value);
            }
        }

        public static DependencyProperty ParametersProperty = DependencyProperty.Register("Parameters", typeof(string[]), typeof(CodeActivity));
        [Description("Parameters")]
        [Category("Parameters Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string[] Parameters
        {
            get
            {
                return ((string[])(base.GetValue(CodeActivity.ParametersProperty)));
            }
            set
            {
                base.SetValue(CodeActivity.ParametersProperty, value);
            }
        }

        public static DependencyProperty ResolvedParameterExpressionProperty = DependencyProperty.Register("ResolvedParameterExpression", typeof(System.String), typeof(Granfeldt.FIM.ActivityLibrary.CodeActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public String ResolvedParameterExpression
        {
            get
            {
                return ((string)(base.GetValue(Granfeldt.FIM.ActivityLibrary.CodeActivity.ResolvedParameterExpressionProperty)));
            }
            set
            {
                base.SetValue(Granfeldt.FIM.ActivityLibrary.CodeActivity.ResolvedParameterExpressionProperty, value);
            }
        }

        public static DependencyProperty CodeProperty = DependencyProperty.Register("Code", typeof(string), typeof(CodeActivity));
        [Description("Code")]
        [Category("Code Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Code
        {
            get
            {
                return ((string)(base.GetValue(CodeActivity.CodeProperty)));
            }
            set
            {
                base.SetValue(CodeActivity.CodeProperty, value);
            }
        }

        public static DependencyProperty DestinationProperty = DependencyProperty.Register("Destination", typeof(string), typeof(CodeActivity));
        [Description("Destination")]
        [Category("Destination Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Destination
        {
            get
            {
                return ((string)(base.GetValue(CodeActivity.DestinationProperty)));
            }
            set
            {
                base.SetValue(CodeActivity.DestinationProperty, value);
            }
        }

        #endregion

        /// <summary>
        /// Contains in-memory compiled code
        /// </summary>
        private static object compiled = null;
        /// <summary>
        /// List of unresolved parameters
        /// </summary>
        List<string> UnresolvedParameters = null;
        /// <summary>
        /// List of resolved parameters. These are passed to the default code method
        /// </summary>
        List<object> ResolvedParameters = new List<object>();
        /// <summary>
        /// Reference to parent workflow
        /// </summary>
        SequentialWorkflow containingWorkflow = null;
        string destinationObject = null;
        string destinationAttribute = null;

        public CodeActivity()
        {
            Debugging.Log("Enter :: Initialize");
            InitializeComponent();
            Debugging.Log("Exit :: Initialize");
        }

        /// <summary>
        /// Has returned value from code function
        /// </summary>
        object codeReturnValue = null;

        private void MoreParametersToResolve_Condition(object sender, ConditionalEventArgs e)
        {
            // we need to convert the string array to a list to
            // be able to remove values
            if (UnresolvedParameters == null)
            {
                UnresolvedParameters = this.Parameters.ToList();
                Debugging.Log("Resolving parameters");
            }
            e.Result = false;
            if (UnresolvedParameters.Count > 0)
            {
                this.ResolveParameterValue.GrammarExpression = UnresolvedParameters[0];
                Debugging.Log("Resolving", UnresolvedParameters[0]);
                UnresolvedParameters.RemoveAt(0);
                e.Result = true;
            }
        }

        private void SaveResolvedValue_ExecuteCode(object sender, EventArgs e)
        {
            ResolvedParameters.Add(HttpUtility.HtmlDecode(this.ResolveParameterValue.ResolvedExpression));
            Debugging.Log("Resolved to", HttpUtility.HtmlDecode(this.ResolveParameterValue.ResolvedExpression));
        }

        private void NonExistingGrammarException_ExecuteCode(object sender, EventArgs e)
        {
            // if we get here, the resolve has failed. If the value that we're
            // trying to resolve doesn't exist, we also get here. We
            // assume that there is no value and set value to null
            Debugging.Log("Missing value or invalid XPath reference (returning [null])", this.ResolveParameterValue.GrammarExpression);
            ResolvedParameters.Add(null);
        }

        private void CompileCode_ExecuteCode(object sender, EventArgs e)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters compilerParameters = new CompilerParameters();

            // DLL must exist in C:\Windows\Microsoft.NET\Framework64\v2.0.50727
            foreach (string dll in this.References)
            {
                compilerParameters.ReferencedAssemblies.Add(dll);
            }
            if (compilerParameters.ReferencedAssemblies.Contains("System.dll"))
            {
                compilerParameters.ReferencedAssemblies.Add("System.dll");
            }
            compilerParameters.GenerateExecutable = false;
            compilerParameters.GenerateInMemory = true;
            compilerParameters.IncludeDebugInformation = false;

            CompilerResults cr = provider.CompileAssemblyFromSource(compilerParameters, HttpUtility.HtmlDecode(this.Code));
            if (cr.Errors.HasErrors)
            {
                StringBuilder compileErrors = new StringBuilder();
                foreach (CompilerError ce in cr.Errors)
                {
                    compileErrors.AppendFormat("Compile Error: {0} in Ln {2} Col {3}-{1}\r\n", ce.ErrorNumber, ce.ErrorText, ce.Line, ce.Column);
                }
                Debugging.Log(new Exception("Couldn't compile\r\n" + compileErrors));
            }
            Assembly assembly = cr.CompiledAssembly;
            compiled = assembly.CreateInstance("FIMDynamicClass");
        }

        private void ExecuteCode_ExecuteCode(object sender, EventArgs e)
        {
            if (compiled == null)
            {
                Debugging.Log(new NullReferenceException("Code must contain a class named FIMDynamicClass and that class must contain a method called FIMDynamicFunction"));
            }
            Debugging.Log("Parameter count", ResolvedParameters.Count);
            foreach (object param in ResolvedParameters)
            {
                Debugging.Log("Adding parameter", param);
            }
            MethodInfo mi = compiled.GetType().GetMethod("FIMDynamicFunction");
            Debugging.Log("Executing code");
            codeReturnValue = mi.Invoke(compiled, ResolvedParameters.ToArray());
            Debugging.Log("Code executed");
            Debugging.Log("Code return value", codeReturnValue);
        }

        private void ShouldUpdateTarget_Condition(object sender, ConditionalEventArgs e)
        {
            // try to get parent workflow.
            if (!SequentialWorkflow.TryGetContainingWorkflow(this, out containingWorkflow))
            {
                throw new InvalidOperationException("Could not get parent workflow");
            }

            StringUtilities.ExtractWorkflowExpression(this.Destination, out destinationObject, out destinationAttribute);
            if (!string.IsNullOrEmpty(destinationAttribute))
            {
                if (destinationObject.Equals("WorkflowData", StringComparison.OrdinalIgnoreCase))
                {
                    e.Result = false;
                    if (containingWorkflow != null)
                    {
                        containingWorkflow.WorkflowDictionary.Add(destinationAttribute, codeReturnValue);
                    }
                }
                else if (destinationObject.Equals("Target", StringComparison.OrdinalIgnoreCase))
                {
                    e.Result = true;
                    ReadTarget.ActorId = WellKnownGuids.FIMServiceAccount;
                    ReadTarget.ResourceId = containingWorkflow.TargetId;
                    ReadTarget.SelectionAttributes = new string[] { destinationAttribute };
                }
            }
            else
            {
                Debugging.Log(new Exception("Could not resolved destination. Please specify as [//Target/Attribute] or [//WorkflowData/Parameter]"));
            }
        }

        private void isTargetUpdateNeeded_Condition(object sender, ConditionalEventArgs e)
        {
            List<UpdateRequestParameter> updateParameters = new List<UpdateRequestParameter>();

            e.Result = false;
            object currentValue = TargetResource[destinationAttribute];
            if (object.Equals(currentValue, codeReturnValue))
            {
                Debugging.Log(string.Format("No need to update {0}. Value is already '{1}'", destinationAttribute, codeReturnValue));
            }
            else
            {
                e.Result = true;

                // if code returns null then remove current value; otherwise
                // update to new value
                updateParameters.Add(new UpdateRequestParameter(destinationAttribute, codeReturnValue == null ? UpdateMode.Remove : UpdateMode.Modify, codeReturnValue == null ? currentValue : codeReturnValue));

                Debugging.Log("Updating", containingWorkflow.TargetId);
                UpdateTargetResource.ActorId = WellKnownGuids.FIMServiceAccount;
                UpdateTargetResource.ResourceId = containingWorkflow.TargetId;
                UpdateTargetResource.UpdateParameters = updateParameters.ToArray();
                if (codeReturnValue == null)
                {
                    Debugging.Log(string.Format("Removing existing value '{0}' from {1}", currentValue, destinationAttribute));
                }
                else
                {
                    Debugging.Log(string.Format("Updating {0} from '{1}' to '{2}'", destinationAttribute, currentValue == null ? "(null)" : currentValue, codeReturnValue == null ? "(null)" : codeReturnValue));
                }
            }
        }

        private void CatchArgumentException_ExecuteCode(object sender, EventArgs e)
        {
            // if we get here, the resolve has failed. If the value that we're
            // trying to resolve doesn't exist, we also get here. We
            // assume that there is no value and set source value to null
            // which effectively results in a 'Delete' operation on
            // the target attribute value (if present)
            Debugging.Log("Error: Argument Exception");
        }

        private void ExitGracefully_ExecuteCode(object sender, EventArgs e)
        {
            Debugging.Log("Activity exited");
        }

    }
}
