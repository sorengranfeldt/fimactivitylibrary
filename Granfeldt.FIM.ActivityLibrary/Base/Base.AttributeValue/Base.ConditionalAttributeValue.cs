using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ResourceManagement.WebServices.WSResourceManagement;

namespace Granfeldt.FIM.ActivityLibrary
{
    public class ConditionalAttributeValue : AttributeValue
    {
        //Constructors
        public ConditionalAttributeValue(string conditionAttributeName)
        {
            this.ConditionAttributeName = conditionAttributeName;
        }
        public ConditionalAttributeValue(string sourceAttributeName, string targetAttributeName, string conditionAttributeName)
        {
            this.ConditionAttributeName = conditionAttributeName;
            this.SourceAttributeName = sourceAttributeName;
            this.TargetAttributeName = targetAttributeName;
        }
        public ConditionalAttributeValue(string sourceAttributeName, string targetAttributeName, string conditionAttributeName, bool updateOnConditionTrue)
        {
            this.ConditionAttributeName = conditionAttributeName;
            this.UpdateOnTrue = updateOnConditionTrue;
            this.SourceAttributeName = sourceAttributeName;
            this.TargetAttributeName = targetAttributeName;
            
        }
        
        //Properties
        public string ConditionAttributeName { get; set; }
        public bool ConditionAttributeValue { get; private set; }
        public bool UpdateOnTrue { get; set; }

        /// <summary>
        /// Compares the Target attribute value with the existing source attribute value. If the source attribute value is not set, 
        /// it will be compared on as a null value.
        /// </summary>
        /// <returns>Result of the comparison.</returns>
        public override bool ShouldUpdate()
        {
            if (conditionsSatisfied())
            {
                return base.ShouldUpdate();
            }
            else
            {
                Debugging.Log(String.Format("Conditions not met. Because {0} has the value: {1}.",this.ConditionAttributeName,this.ConditionAttributeValue));
                return false;
            }
        }
        /// <summary>
        /// Sets Source and Target values as well as conditional values.
        /// </summary>
        /// <param name="resource">resource object of the target to be updated.</param>
        internal override void SetValues(ResourceType resource)
        {
            base.SetValues(resource);
            this.SetConditionValue(resource);
        }
        /// <summary>
        /// Private methods that determines if the conditions are met to allow the target to be updated.
        /// </summary>
        /// <returns>If conditions are met, it will return true, otherwise false.</returns>
        private bool conditionsSatisfied()
        {
            if (UpdateOnTrue)
            {
                if (ConditionAttributeValue)
                {
                    //True and True is True
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (!ConditionAttributeValue)
                {
                    //False and False is true
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Sets the ConditionAttributeValue, based on ConditionAttributeName and <paramref name="resource"/>.
        /// </summary>
        /// <param name="resource"></param>
        private void SetConditionValue(ResourceType resource)
        {
            this.ConditionAttributeValue = resource[this.ConditionAttributeName] != null ? (bool)resource[this.ConditionAttributeName] : false;
            Debugging.Log(String.Format("Condition set to: {0}", this.ConditionAttributeValue));
        }
    }
}
