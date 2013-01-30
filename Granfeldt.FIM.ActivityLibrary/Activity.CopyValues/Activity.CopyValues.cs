// January 10, 2013 | Soren Granfeldt
//  - initial version released for testing
// January 22, 2013 | Soren Granfeldt
//  - added function to convert different types/values
//    before comparison.
// January 22, 2013 | Soren Granfeldt
//  - rewrote update logic to allow updating more the one object
// January 29, 2013 | Kristian Birk Thim
//  - extended the AttributeValue class to include more logic specific to it.
//  - created ConditionalAttributeValue. A AttributeValue class which includes conditional properties (Inherits from AttributeValue)
//  - created AttributeValueCollection which includes some of the processing logic specific to a collection of AttributeValues.
//  - re-wrote parts of the main code to incorporate the new classes.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text.RegularExpressions;
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

    public partial class CopyValuesActivity : SequenceActivity
    {

        #region Properties

        public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(CopyValuesActivity));
        [Description("Title")]
        [Category("Title Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Title
        {
            get
            {
                return ((string)(base.GetValue(CopyValuesActivity.TitleProperty)));
            }
            set
            {
                base.SetValue(CopyValuesActivity.TitleProperty, value);
            }
        }

        public static DependencyProperty AttributePairsProperty = DependencyProperty.Register("AttributePairs", typeof(string[]), typeof(CopyValuesActivity));
        [Description("AttributePairs")]
        [Category("AttributePairs Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string[] AttributePairs
        {
            get
            {
                return ((string[])(base.GetValue(CopyValuesActivity.AttributePairsProperty)));
            }
            set
            {
                base.SetValue(CopyValuesActivity.AttributePairsProperty, value);
            }
        }

        public static DependencyProperty UpdateOnTrueProperty = DependencyProperty.Register("UpdateOnTrue", typeof(bool), typeof(CopyValuesActivity));
        [Description("UpdateOnTrue")]
        [Category("UpdateOnTrue Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool UpdateOnTrue
        {
            get
            {
                return ((bool)(base.GetValue(CopyValuesActivity.UpdateOnTrueProperty)));
            }
            set
            {
                base.SetValue(CopyValuesActivity.UpdateOnTrueProperty, value);
            }
        }

        public static DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType), typeof(CopyValuesActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Parameters")]
        public Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType Target
        {
            get
            {
                return ((Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType)(base.GetValue(CopyValuesActivity.TargetProperty)));
            }
            set
            {
                base.SetValue(CopyValuesActivity.TargetProperty, value);
            }
        }

        public static DependencyProperty ResolvedSourceExpressionProperty = DependencyProperty.Register("ResolvedSourceExpression", typeof(System.String), typeof(CopyValuesActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public String ResolvedSourceExpression
        {
            get
            {
                return ((string)(base.GetValue(CopyValuesActivity.ResolvedSourceExpressionProperty)));
            }
            set
            {
                base.SetValue(CopyValuesActivity.ResolvedSourceExpressionProperty, value);
            }
        }

        public static DependencyProperty AlternativeTargetObjectProperty = DependencyProperty.Register("AlternativeTargetObject", typeof(string), typeof(CopyValuesActivity));
        [Description("AlternativeTargetObject")]
        [Category("AlternativeTargetObject Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string AlternativeTargetObject
        {
            get
            {
                return ((string)(base.GetValue(CopyValuesActivity.AlternativeTargetObjectProperty)));
            }
            set
            {
                base.SetValue(CopyValuesActivity.AlternativeTargetObjectProperty, value);
            }
        }

        public static DependencyProperty ResolvedAlternativeTargetObjectProperty = DependencyProperty.Register("ResolvedAlternativeTargetObject", typeof(System.String), typeof(CopyValuesActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public String ResolvedAlternativeTargetObject
        {
            get
            {
                return ((string)(base.GetValue(CopyValuesActivity.ResolvedAlternativeTargetObjectProperty)));
            }
            set
            {
                base.SetValue(CopyValuesActivity.ResolvedAlternativeTargetObjectProperty, value);
            }
        }

        public static DependencyProperty ObjectIDToUpdateProperty = DependencyProperty.Register("ObjectIDToUpdate", typeof(Guid), typeof(CopyValuesActivity));
        [Description("ObjectIDToUpdate")]
        [Category("ObjectIDToUpdate Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Guid ObjectIDToUpdate
        {
            get
            {
                return ((Guid)(base.GetValue(CopyValuesActivity.ObjectIDToUpdateProperty)));
            }
            set
            {
                base.SetValue(CopyValuesActivity.ObjectIDToUpdateProperty, value);
            }
        }

        #endregion

        AttributeValue ResolvedAttributeValue;
        List<string> selectionAttributes = new List<string>();
        AttributeValueCollection attrValues = new AttributeValueCollection();

        public CopyValuesActivity()
        {
            InitializeComponent();
        }

        public List<Guid> ObjectsToUpdate = new List<Guid>();

        private void PrepareResolveValues_ExecuteCode(object sender, EventArgs e)
        {
            Debugging.Log("Activity initialized");
            // parse attribute pairs and build hashtable for easy processing
            foreach (string attribute in this.AttributePairs)
            {
                string[] s = attribute.Split(new string[] { "," }, StringSplitOptions.None);
                if (s.Length == 3)
                {
                    foreach (string se in s.ToList().Where(n => !(n.Contains("[//")) && !(string.IsNullOrEmpty(n))))
                    {
                        Debugging.Log("Add selection attribute", se);
                        selectionAttributes.Add(se);
                    }
                    if (s[1].Trim() == "")
                    {
                        //Non-conditional AttributeValue
                        attrValues.Add(new AttributeValue(s[0].Trim(), s[2].Trim()));
                    }
                    else
                    {
                        //Conditional AttributeValue
                        attrValues.Add(new ConditionalAttributeValue(s[0].Trim(), s[2].Trim(), s[1].Trim(), this.UpdateOnTrue));
                    }
                }
            }
        }

        private void MoreToResolve_Condition(object sender, ConditionalEventArgs e)
        {
            // return true if there are any more source values to resolve
            e.Result = attrValues.Where(av => av.ShouldResolve).Count() > 0;
            if (e.Result)
            {
                ResolvedAttributeValue = attrValues.Where(a => a.ShouldResolve).FirstOrDefault();
                ResolveSourceGrammar.GrammarExpression = ResolvedAttributeValue.SourceAttributeName;
                ResolveSourceGrammar.Closed += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(ResolveSourceGrammar_Closed);
                Debugging.Log("Resolving", ResolvedAttributeValue.SourceAttributeName);
            }
            else
            {
                Debugging.Log("No more source values to resolve");
            }
        }

        void ResolveSourceGrammar_Closed(object sender, ActivityExecutionStatusChangedEventArgs e)
        {
            Debugging.Log(string.Format("Activity {0} result", e.Activity.Name), e.ExecutionResult);
        }

        private void NonExistingGrammarException_ExecuteCode(object sender, EventArgs e)
        {
            // if we get here, the resolve has failed. If the value that we're
            // trying to resolve doesn't exist, we also get here. We
            // assume that there is no value and set source value to null
            // which effectively results in a 'Delete' operation on
            // the target attribute value (if present)
            Debugging.Log("Missing value or invalid XPath reference", this.ResolveSourceGrammar.GrammarExpression);
            ResolvedAttributeValue.SourceAttributeValue = null;
            //ResolvedAttributeValue.ShouldResolve = false;
        }

        private void FetchResolvedGrammar_ExecuteCode(object sender, EventArgs e)
        {
            Debugging.Log("Resolved", this.ResolvedSourceExpression);
            ResolvedAttributeValue.SourceAttributeValue = this.ResolvedSourceExpression;
            //ResolvedAttributeValue.ShouldResolve = false; //IsResolved is set automatically
        }

        private void ShouldDoLookup_Condition(object sender, ConditionalEventArgs e)
        {
            e.Result = false;
            if (string.IsNullOrEmpty(this.AlternativeTargetObject))
            {
                Debugging.Log("No lookup is done. Object to update is //Target");
            }
            else
            {
                Debugging.Log("Unresolved alternative object", this.AlternativeTargetObject);
                e.Result = true;
            }
        }

        private void ExtractTargetObjectID_ExecuteCode(object sender, EventArgs e)
        {
            SequentialWorkflow containingWorkflow = null;
            if (!SequentialWorkflow.TryGetContainingWorkflow(this, out containingWorkflow))
            {
                throw new InvalidOperationException("Could not get parent workflow!");
            }
            ObjectsToUpdate.Add(containingWorkflow.TargetId);
            Debugging.Log("Target ObjectID to update", containingWorkflow.TargetId);
        }

        private void PrepareFindTargetResource_ExecuteCode(object sender, EventArgs e)
        {
            Debugging.Log("Preparing lookup of", ResolveLookupGrammar.ResolvedExpression);
            FindTargetResource.ActorId = WellKnownGuids.FIMServiceAccount;
            FindTargetResource.PageSize = 100;
            FindTargetResource.Attributes = new string[] { "ObjectID" };
            FindTargetResource.XPathFilter = this.ResolvedAlternativeTargetObject;
            FindTargetResource.Closed += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(FindTargetResource_Closed);
        }

        void FindTargetResource_Closed(object sender, ActivityExecutionStatusChangedEventArgs e)
        {
            Debugging.Log(string.Format("Activity {0} result", e.Activity.Name), e.ExecutionResult);
            Debugging.Log("Objects found#", FindTargetResource.TotalResultsCount);
            if (FindTargetResource.TotalResultsCount > 0)
            {
                ObjectsToUpdate.AddRange(FindTargetResource.EnumeratedResourceIDs.ToList());
            }
        }

        void ReadTarget_Closed(object sender, ActivityExecutionStatusChangedEventArgs e)
        {
            Debugging.Log(string.Format("Activity {0} result", e.Activity.Name), e.ExecutionResult);
        }

        private void MoreTargetsToUpdate_Condition(object sender, ConditionalEventArgs e)
        {
            if (ObjectsToUpdate.Count > 0)
            {
                e.Result = true;
                this.ObjectIDToUpdate = ObjectsToUpdate[0];
                ObjectsToUpdate.RemoveAt(0);

                ReadTarget.ActorId = WellKnownGuids.FIMServiceAccount;
                ReadTarget.ResourceId = this.ObjectIDToUpdate;
                ReadTarget.SelectionAttributes = selectionAttributes.Distinct().ToArray();
                ReadTarget.Closed += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(ReadTarget_Closed);
            }
            else
            {
                e.Result = false;
                Debugging.Log("No more objects to update");
            }
        }

        private void ShouldUpdate_Condition(object sender, ConditionalEventArgs e)
        {
            try
            {
                List<UpdateRequestParameter> updateParameters = new List<UpdateRequestParameter>();

                //Gets all non matching AttributeValues in AttrValues.
                updateParameters = attrValues.Changes;

                // if we have added any update parameters this means that there are changes
                // so we prepare the update activtiy with relevant information and
                // set e.Result to true to ensure that the Update activity is called
                if (updateParameters.Count > 0)
                {

                    // Important: grap a hold of the proper UpdateResourceActivity instance
                    // we do this by "digging" down through the activties. I really, really
                    // need to find a more dynamic way of doing this.
                    var seqParent = (SequenceActivity)this.LoopUpdateAllTargets.DynamicActivity;
                    IfElseActivity ifelseact = (IfElseActivity)seqParent.Activities.OfType<IfElseActivity>().FirstOrDefault();
                    IfElseBranchActivity iebranch = (IfElseBranchActivity)ifelseact.Activities.OfType<IfElseBranchActivity>().FirstOrDefault();

                    UpdateResourceActivity currentUpdateActivityInstance = (UpdateResourceActivity)iebranch.Activities.OfType<UpdateResourceActivity>().FirstOrDefault();
                    if (currentUpdateActivityInstance == null)
                    {
                        throw new InvalidOperationException("Could not find dynamic UpdateResourceActivity");
                    }
                    else
                    {
                        e.Result = true;
                        currentUpdateActivityInstance.ActorId = WellKnownGuids.FIMServiceAccount;
                        currentUpdateActivityInstance.UpdateParameters = updateParameters.ToArray();
                        currentUpdateActivityInstance.ResourceId = this.ObjectIDToUpdate;
                        Debugging.Log("Updating", UpdateTarget.ResourceId);
                    }
                }
                else
                {
                    Debugging.Log("Nothing to update for", this.ObjectIDToUpdate);
                    e.Result = false;
                }
            }
            catch (Exception ex)
            {
                Debugging.Log(string.Format("Error: '{0}'", ex.Message));
            }
        }

        private void ExitGracefully_ExecuteCode(object sender, EventArgs e)
        {
            Debugging.Log("Activity exited");
        }

        private void FaultArgumentNullException_ExecuteCode(object sender, EventArgs e)
        {
            Debugging.Log("Error", UpdateTarget.ExecutionResult);
        }

        private void SetTargetResource_ExecuteCode(object sender, EventArgs e)
        {
            //Sets the TargetResource on the attValues collection
            attrValues.TargetResource = this.ReadTarget.Resource;
        }
    }
}
