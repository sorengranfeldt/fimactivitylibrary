// January 15, 2013 | Soren Granfeldt
//  - initial version
// January 17, 2013 | Soren Granfeldt
//  - changed activity name from CodeActivity to CodeRunActivity
//    due to clashes with built-in naming convention
// January 18, 2013 | Soren Granfeldt
//  - changed update to use helper update activity
// December 16, 2015 | soren granfeldt
//	- changed logging to tuse tracer

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
    public partial class CodeRunActivity : SequenceActivity
    {

        #region Properties

        public static DependencyProperty TargetResourceProperty = DependencyProperty.Register("TargetResource", typeof(Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType), typeof(Granfeldt.FIM.ActivityLibrary.CodeRunActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Parameters")]
        public Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType TargetResource
        {
            get
            {
                return ((ResourceType)(base.GetValue(Granfeldt.FIM.ActivityLibrary.CodeRunActivity.TargetResourceProperty)));
            }
            set
            {
                base.SetValue(Granfeldt.FIM.ActivityLibrary.CodeRunActivity.TargetResourceProperty, value);
            }
        }


        /// <summary>
        /// The title of the current instance of the workflow
        /// </summary>
        public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(CodeRunActivity));
        [Description("Title")]
        [Category("Title Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Title
        {
            get
            {
                return ((string)(base.GetValue(CodeRunActivity.TitleProperty)));
            }
            set
            {
                base.SetValue(CodeRunActivity.TitleProperty, value);
            }
        }

        public static DependencyProperty ReferencesProperty = DependencyProperty.Register("References", typeof(string[]), typeof(CodeRunActivity));
        [Description("References")]
        [Category("References Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string[] References
        {
            get
            {
                return ((string[])(base.GetValue(CodeRunActivity.ReferencesProperty)));
            }
            set
            {
                base.SetValue(CodeRunActivity.ReferencesProperty, value);
            }
        }

        public static DependencyProperty ParametersProperty = DependencyProperty.Register("Parameters", typeof(string[]), typeof(CodeRunActivity));
        [Description("Parameters")]
        [Category("Parameters Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string[] Parameters
        {
            get
            {
                return ((string[])(base.GetValue(CodeRunActivity.ParametersProperty)));
            }
            set
            {
                base.SetValue(CodeRunActivity.ParametersProperty, value);
            }
        }

        public static DependencyProperty ResolvedParameterExpressionProperty = DependencyProperty.Register("ResolvedParameterExpression", typeof(System.String), typeof(Granfeldt.FIM.ActivityLibrary.CodeRunActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public String ResolvedParameterExpression
        {
            get
            {
                return ((string)(base.GetValue(Granfeldt.FIM.ActivityLibrary.CodeRunActivity.ResolvedParameterExpressionProperty)));
            }
            set
            {
                base.SetValue(Granfeldt.FIM.ActivityLibrary.CodeRunActivity.ResolvedParameterExpressionProperty, value);
            }
        }

        public static DependencyProperty CodeProperty = DependencyProperty.Register("Code", typeof(string), typeof(CodeRunActivity));
        [Description("Code")]
        [Category("Code Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Code
        {
            get
            {
                return ((string)(base.GetValue(CodeRunActivity.CodeProperty)));
            }
            set
            {
                base.SetValue(CodeRunActivity.CodeProperty, value);
            }
        }

        public static DependencyProperty DestinationProperty = DependencyProperty.Register("Destination", typeof(string), typeof(CodeRunActivity));
        [Description("Destination")]
        [Category("Destination Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Destination
        {
            get
            {
                return ((string)(base.GetValue(CodeRunActivity.DestinationProperty)));
            }
            set
            {
                base.SetValue(CodeRunActivity.DestinationProperty, value);
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

        public CodeRunActivity()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Has returned value from code function
        /// </summary>
        object codeReturnValue = null;

        private void MoreParametersToResolve_Condition(object sender, ConditionalEventArgs e)
        {
            Tracer.Enter("Activity initialized");
            // we need to convert the string array to a list to
            // be able to remove values
            if (UnresolvedParameters == null)
            {
                UnresolvedParameters = this.Parameters.ToList();
               Tracer.TraceInformation("Resolving parameters");
            }
            e.Result = false;
            if (UnresolvedParameters.Count > 0)
            {
                this.ResolveParameterValue.GrammarExpression = UnresolvedParameters[0];
				Tracer.TraceInformation("Resolving {0}", UnresolvedParameters[0]);
                UnresolvedParameters.RemoveAt(0);
                e.Result = true;
            }
        }

        private void SaveResolvedValue_ExecuteCode(object sender, EventArgs e)
        {
            ResolvedParameters.Add(HttpUtility.HtmlDecode(this.ResolveParameterValue.ResolvedExpression));
			Tracer.TraceInformation("Resolved to {0}", HttpUtility.HtmlDecode(this.ResolveParameterValue.ResolvedExpression));
        }

        private void NonExistingGrammarException_ExecuteCode(object sender, EventArgs e)
        {
			// if we get here, the resolve has failed. If the value that we're
			// trying to resolve doesn't exist, we also get here. We
			// assume that there is no value and set value to null
			Tracer.TraceError("Missing value or invalid XPath reference (returning [null]) {0}", this.ResolveParameterValue.GrammarExpression);
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
				Tracer.TraceError("Couldn't compile {0}" + compileErrors);
            }
            Assembly assembly = cr.CompiledAssembly;
            compiled = assembly.CreateInstance("FIMDynamicClass");
        }

        private void ExecuteCode_ExecuteCode(object sender, EventArgs e)
        {
            if (compiled == null)
            {
				Tracer.TraceError("Code must contain a class named FIMDynamicClass and that class must contain a method called FIMDynamicFunction");
            }
			Tracer.TraceInformation("Parameter count {0}", ResolvedParameters.Count);
            foreach (object param in ResolvedParameters)
            {
				Tracer.TraceInformation("Adding parameter {0}", param);
            }
            MethodInfo mi = compiled.GetType().GetMethod("FIMDynamicFunction");
			Tracer.TraceInformation("Executing code");
            codeReturnValue = mi.Invoke(compiled, ResolvedParameters.ToArray());
			Tracer.TraceInformation("Code executed");
			Tracer.TraceInformation("Code return value {0}", codeReturnValue);
        }

        private void ShouldUpdateTarget_Condition(object sender, ConditionalEventArgs e)
        {
            // try to get parent workflow.
            if (!SequentialWorkflow.TryGetContainingWorkflow(this, out containingWorkflow))
            {
				string errorMessage = "Could not get parent workflow";
				Tracer.TraceError(errorMessage);
                throw new InvalidOperationException(errorMessage);
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
                    UpdateTargetIfNeeded.ActorId = WellKnownGuids.FIMServiceAccount;
                    UpdateTargetIfNeeded.AttributeName = destinationAttribute;
                    UpdateTargetIfNeeded.NewValue = codeReturnValue;
                    UpdateTargetIfNeeded.TargetId = containingWorkflow.TargetId;
                }
            }
            else
            {
                Tracer.TraceError("Could not resolved destination. Please specify as [//Target/Attribute] or [//WorkflowData/Parameter]");
            }
        }

        private void CatchArgumentException_ExecuteCode(object sender, EventArgs e)
        {
            // if we get here, the resolve has failed. If the value that we're
            // trying to resolve doesn't exist, we also get here. We
            // assume that there is no value and set source value to null
            // which effectively results in a 'Delete' operation on
            // the target attribute value (if present)
            Tracer.TraceError("Error: Argument Exception");
        }

        private void ExitGracefully_ExecuteCode(object sender, EventArgs e)
        {
            Tracer.TraceInformation("Activity exited");
        }
    }
}
