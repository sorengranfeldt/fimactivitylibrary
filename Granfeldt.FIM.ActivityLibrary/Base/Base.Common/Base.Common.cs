﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Granfeldt.FIM.ActivityLibrary
{
    public class WellKnownGuids
    {
        public static Guid BuiltInSynchronizationAccount = new Guid("fb89aefa-5ea1-47f1-8890-abe7797d6497");
        public static Guid FIMServiceAccount = new Guid("e05d1f1b-3d5e-4014-baa6-94dee7d68c89");
        public static Guid Anonymous = new Guid("b0b36673-d43b-4cfa-a7a2-aff14fd90522");
    }

    public static class Debugging
    {
        private static string DebugLogFilename = null;
        private static Mutex loggingMutex = new Mutex();

        static Debugging()
        {
            try
            {
                DebugLogFilename = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Granfeldt\FIM\Workflows", false).GetValue("DebugLogFilename", null).ToString();
                DebugLogFilename = string.Format(DebugLogFilename, DateTime.Now);
            }
            catch
            {
                // we get here if key og hive doesn't exist
            }
        }

        public static void Log(string s)
        {
            string message = string.Format("{0}: {1}", DateTime.Now, string.IsNullOrEmpty(s) ? "(null)" : s);
            loggingMutex.WaitOne();
            if (!string.IsNullOrEmpty(DebugLogFilename))
            {
                using (StreamWriter f = new StreamWriter(DebugLogFilename, true))
                {
                    f.WriteLine(message);
                }
            }
            loggingMutex.ReleaseMutex();
            System.Diagnostics.Trace.WriteLine(string.Format("{0}: {1} - {2}", DateTime.Now, s, TraceRoutine("")));
        }

        public static void Log(string name, object value)
        {
            Log(string.Format("{0}: {1}", name, value != null ? value.ToString() : "(null)"));
        }

        public static void Log(Exception ex)
        {
            Log(string.Format("Error: {0}", ex.Message));
            throw ex;
        }

        public static string TraceRoutine(string action)
        {
            StackTrace CallStack = new StackTrace();
            int i = 0;
            string StrOut = null;
            for (i = CallStack.FrameCount - 1; i >= 1; i += -1)
            {
                if (StrOut != null)
                {
                    StrOut += "->";
                }
                StrOut += CallStack.GetFrame(i).GetMethod().Name;
            }
            CallStack = null;
            return string.Format("{0}: {1}", action, StrOut);
        }

    }

    public static class FIMAttributeUtilities
    {

        /// <summary>
        /// Returns a formatted value ready for committing to create/update resource built-in activity
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object FormatValue(object value)
        {
            if (value == null)
                return null;

            if (value.GetType() == typeof(bool))
                return value;

            // if the source value is a date, we need to format the date for the update to take the value
            DateTime outputDate;
            if (DateTime.TryParse(value.ToString(), out outputDate))
            {
                value = outputDate.ToString("yyyy-MM-ddTHH:mm:ss.000");
                return value;
            }

            // if it's a reference value, we need to remove leading urn:uuid text before later comparison
            value = Regex.Replace(value.ToString(), "^urn:uuid:", "", RegexOptions.IgnoreCase);
            return value;
        }

        /// <summary>
        /// Returns true if the two values are different (FIM evaluation)
        /// </summary>
        /// <param name="sourceValue"></param>
        /// <param name="targetValue"></param>
        /// <returns></returns>
        public static bool ValuesAreDifferent(object sourceValue, object targetValue)
        {
            if (targetValue == null || sourceValue == null)
            {
                if (targetValue != null || sourceValue != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (!targetValue.ToString().Equals(sourceValue.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    public class StringUtilities
    {
        /// <summary>
        /// Parses and splits the expression passed
        /// </summary>
        /// <param name="Expression">Expression to parse, i.e. [//Target/DisplayName] or [//WorkflowData/Password]</param>
        /// <param name="Destination">Will hold the destination object type after parsing</param>
        /// <param name="DestinationAttribute">Will hold the destination attribute after parsing</param>
        /// <returns></returns>
        public static void ExtractWorkflowExpression(string Expression, out string Destination, out string DestinationAttribute)
        {
            Regex regex = new Regex(@"^\[//(?<destination>\w+)/+?(?<destinationattribute>\w*[^]])", RegexOptions.IgnoreCase);
            Destination = regex.Match(Expression).Result("${destination}");
            DestinationAttribute = regex.Match(Expression).Result("${destinationattribute}");
        }

    }

}
