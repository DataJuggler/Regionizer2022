

#region using statements

using DataJuggler.Core.UltimateHelper.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataJuggler.Regionizer.CodeModel.Util;
using DataJuggler.Regionizer.CodeModel.Objects;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Util
{

    #region class DescriptionHelper
    /// <summary>
    /// This class is used to build descriptions for methods or events based upon the 
    /// Event Name of Method Name.
    /// </summary>
    public class DescriptionHelper
    {

        #region Methods

            #region CreateConstructorDescription(string objectType)
            /// <summary>
            /// This method returns the Constructor Description
            /// </summary>
            public string CreateConstructorDescription(string objectType)
            {
                // initial value
                string createConstructorDescription = "Create a new instance of a '" + objectType + "' object.";
                
                // return value
                return createConstructorDescription;
            }
            #endregion
            
            #region GetButtonClickDescription(string eventName)
            /// <summary>
            /// This method gets the Button Click description.
            /// </summary>
            /// <param name="eventName"></param>
            /// <returns></returns>
            private static string GetButtonClickDescription(string eventName)
            {
                // initial value
                string description = "";

                // verify the string exists
                if (!String.IsNullOrEmpty(eventName))
                {
                    // set the buttonName
                    string buttonName = eventName.Replace("_Click", "");

                    // Set the description
                    description = "the '" + buttonName + "' is clicked.";
                }

                // return value
                return description;
            }
            #endregion

            #region GetEventDescription(string eventName)
            /// <summary>
            /// This method returns the description for an event.
            /// </summary>
            /// <param name="eventName"></param>
            /// <param name="returnType"></param>
            /// <returns></returns>
            public static string GetEventDescription(string eventName)
            {
                // initial value
                string description = "";

                // first we have to test if this methodName is handled 
                description = GetPreProcessedDescription(eventName);

                // if the string still does not exist
                if (String.IsNullOrEmpty(description))
                {
                    // if _Click is true
                    if (eventName.Contains("_Click"))
                    {
                        // Get the description for a button or object click.
                        description = GetButtonClickDescription(eventName);
                    }
                    else if (eventName.Contains("SelectedIndexChanged"))
                    {
                        // We are going with the assumpttion this is a button or object got clicked.
                        description = GetSelectedIndexChangedDescription(eventName);
                    }
                    else
                    {
                        // Get the description fora  Generic Event (unknown type of event)
                        description = GetGenericEventDescritpion(eventName);
                    }
                }

                // replace out any double spaces with a  single space
                description = description.Replace("  ", " ");

                // return value
                return description;
            }
            #endregion

            #region GetGenericEventDescritpion(string eventName)
            /// <summary>
            /// This method gets the Generic Event Description.
            /// </summary>
            /// <param name="eventName"></param>
            /// <returns></returns>
            private static string GetGenericEventDescritpion(string eventName)
            {
                // initial value
                string description = "";

                // Create  string builder
                StringBuilder sb = new StringBuilder("");

                // locals
                bool isVerb = false;
                string firstWord = "";
                string lastWord = "";

                // Get the words
                List<Word> words = CSharpCodeParser.ParseWordsByCapitalLetters(eventName);

                // if there are one or more words
                if ((words != null) && (words.Count > 0))
                {
                    // we have to test if the first word is a verb
                    isVerb = false;
                    firstWord = words[0].Text;
                    lastWord = words[words.Count - 1].Text;

                    // we have to determine if the first word is a verb
                    isVerb = IsVerb(firstWord);

                    // if the first word is a verb
                    if (isVerb)
                    {
                        // remove the first word
                        words.RemoveAt(0);
                    }

                    // now iterate the words
                    foreach (Word word in words)
                    {
                        // append the text of this word
                        sb.Append(" " + word.Text);
                    }

                    // Trim 
                    description = sb.ToString().Trim();
                }

                // return value
                return description;
            }
            #endregion

            #region GetMethodDescription(string methodName, string returnType)
            /// <summary>
            /// This method returns the method description based upon the method name.
            /// </summary>
            /// <param name="methodName"></param>
            /// <returns></returns>
            public static string GetMethodDescription(string methodName, string returnType)
            {
                // initail value
                string description = "";

                // first we have to test if this methodName is handled 
                description = GetPreProcessedDescription(methodName);

                // if the string still does not exist
                if (String.IsNullOrEmpty(description))
                {
                    // Create  string builder
                    StringBuilder sb = new StringBuilder("");

                    // Get the words
                    List<Word> words = CSharpCodeParser.ParseWordsByCapitalLetters(methodName);

                    // if the return Type exists
                    bool hasReturnType = (!String.IsNullOrEmpty(returnType));

                    // if there is a return type and it is not void
                    if ((hasReturnType) && (returnType != "void"))
                    {
                        // if this is a list or a collection
                        if ((returnType.Contains("List")) || (returnType.Contains("Collection")))
                        {
                            // add the word returns
                            sb.Append(" returns a list of");
                        }
                        else
                        {
                            // add the word returns
                            sb.Append(" returns the ");
                        }

                        // if there are one or more words
                        if ((words != null) && (words.Count > 0))
                        {
                            // we have to test if the first word is a verb
                            bool isVerb = false;
                            string firstWord = words[0].Text;

                            // we have to determine if the first word is a verb
                            isVerb = IsVerb(firstWord);

                            // remove the first word
                            words.RemoveAt(0);
                        }
                    }
                    
                    // if there are one or more words
                    if ((words != null) && (words.Count > 0))
                    {
                        // now iterate the words
                        foreach (Word word in words)
                        {
                            sb.Append(" " + word.Text);
                        }
                    }

                    // Trim 
                    description = sb.ToString().Trim();
                }

                // replace out any double spaces with a  single space
                description = description.Replace("  ", " ");

                // return value
                return description;
            }
            #endregion

            #region GetPreProcessedDescription(string methodName)
            /// <summary>
            /// This method returns a description for common method names.
            /// </summary>
            public static string GetPreProcessedDescription(string methodName)
            {
                // initial value
                string description = "";

                // if the string exists
                if (!String.IsNullOrEmpty(methodName))
                {
                    // test for known method names
                    switch(methodName.ToLower())
                    {
                        case "init":

                            // Set the list
                            description = " This method performs initializations for this object.";

                            // required
                            break;

                        case "page_load":

                            // Set the list
                            description = " when this Page is loaded.";

                            // required
                            break;
                    }
                }
                
                // return value
                return description;
            }
            #endregion

            #region GetSelectedIndexChangedDescription(string eventName)
            /// <summary>
            /// This method returns the description for the SelectedIndexChanged.
            /// </summary>
            /// <param name="eventName"></param>
            /// <returns></returns>
            private static string GetSelectedIndexChangedDescription(string eventName)
            {
                // initial value
                string description = "";

                // verify the string exists
                if (!String.IsNullOrEmpty(eventName))
                {
                    // set the buttonName
                    string controlName = eventName.Replace("SelectedIndexChanged", "");

                    // Set the description
                    description = "a selection is made in the '" + controlName + "'.";
                }

                // return value
                return description;
            }
            #endregion
          
            #region IsVerb(string firstWord)
            /// <summary>
            /// This method returns true if this is a word like Get, Find, Load, Calculate, etc.
            /// </summary>
            public static bool IsVerb(string firstWord)
            {
                // initial value
                bool isVerb = false;
                
                // if the word exists
                if (!String.IsNullOrEmpty(firstWord))
                {
                    // get the first world
                    string temp = firstWord.ToLower();

                    switch(temp)
                    {
                        case "get":
                        case "find":
                        case "load":
                        case "calculate":
                        case "set":
                        case "process":
                        case "add":

                        // is verb is true
                        isVerb = true;

                        // required
                        break;
                    }
                }

                // reurn value
                return isVerb;
            }
            #endregion
            
        #endregion

    }
    #endregion

}
