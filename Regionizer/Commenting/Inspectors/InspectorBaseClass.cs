

#region using statements

using DataJuggler.Core.UltimateHelper;
using DataJuggler.Regionizer.CodeModel.Objects;
using DataJuggler.Regionizer.Commenting.Interfaces;
using System;
using System.Text.RegularExpressions;
using XmlMirror.Runtime.Util;
using CM = DataJuggler.Regionizer.CodeModel.Objects;
using System.Collections.Generic;
using DataJuggler.Regionizer.CodeModel.Enumerations;

#endregion

namespace DataJuggler.Regionizer.Commenting.Inspectors
{

    #region class InspectorBaseClass
    /// <summary>
    /// This class is a base class that all Inspectors can inherit from (it is not required)
    /// </summary>
    public class InspectorBaseClass : IInspector
    {
        
        #region Private Variables
         private const string GuidID = "33659693-fbac-4ecf-9994-9386bd916f88";
         #endregion

        #region Methods

            #region ApplyReplacements(string source, List<Replacement> replacements, ReplacementTargetEnum target)
            /// <summary>
            /// This method returns the Replacements
            /// </summary>
            private string ApplyReplacements(string source, List<Replacement> replacements, ReplacementTargetEnum target)
            {
                // If the replacements collection exists and has one or more items
                if ((ListHelper.HasOneOrMoreItems(replacements)) && (TextHelper.Exists(source)))
                {
                    // iterate the Replacements
                    foreach (Replacement replacement in replacements)
                    {
                        // if this should be applied to the source word
                        if (replacement.Target == target)
                        {
                            // apply this to the source
                            source = source.Replace(replacement.Find, replacement.Replace);
                        }
                    }
                }

                // return value
                return source;
            }
            #endregion
            
            #region DoesTargetStartWithAVowel(string target)
            /// <summary>
            /// This method returns the Target Start With A Vowel
            /// </summary>
            private bool DoesTargetStartWithAVowel(string target)
            {
                // initial value
                bool startsWithVowel = false;

                // if the target exists
                if (TextHelper.Exists(target))
                {
                    // vowel pattern
                    string pattern = "[aeiouAEIOU]";

                    // if the first char is a vowel
                    if (Regex.IsMatch(target[0].ToString(), pattern))
                    {
                        // set to true
                        startsWithVowel = true;
                    }
                }

                // return value
                return startsWithVowel;
            }
            #endregion
            
            #region EvaluateMatch(Match match)
            /// <summary>
            /// This method evaluates a match 
            /// </summary>
            /// <param name="match"></param>
            /// <returns></returns>
            public string EvaluateMatch(Match match)
            {
                // initial value
                string replacementText = "";

                // if the text exists
                if (match != null)
                {
                    // get the replacementText
                    replacementText = match.Captures.ToString();
                }

                // return value
                return replacementText;
            }
            #endregion

            #region GetAutoCommentText(CM.CommentDictionary commentDictionary, string sourceCode, DictionaryInfo dictionaryInfo)
            /// <summary>
            /// This method returns the Auto Comment Text for the SourceCode given.
            /// </summary>
            public string GetAutoCommentText(CM.CommentDictionary commentDictionary, string sourceCode, DictionaryInfo dictionaryInfo)
            {
                // initial value
                string commentText = "";

                // the * is replaced with TargetPattern
                string target = "";

                // the % is replaced with TargetPattern2
                string target2 = "";

                // locals
                List<CodeComment> comments = null;
                List<CodeComment> comments2 = null;
                bool matchFound = false;                
                string sourceCodeCopy = "";

                // if the dictionaryInfo object exists
                if (dictionaryInfo != null)
                {
                    // if the sourceCode exists
                    if (TextHelper.Exists(sourceCode))
                    {
                        // remove the semicolon
                        sourceCode = sourceCode.Replace(";", "");

                        // trim the sourceCode
                        sourceCode = sourceCode.Trim();

                        // if the commentDictionary exists
                        if (commentDictionary != null)
                        {
                            // if UseCustomDictionary
                            if (dictionaryInfo.UseCustomDictionary)
                            {
                                // if we need to try custom dictionary first
                                if (dictionaryInfo.TryCustomDictionaryFirst)
                                {
                                    // set the comment collection to try first
                                    comments = commentDictionary.CustomComments;

                                    // set the comment collection to try next
                                    comments2 = commentDictionary.Comments;
                                }
                                else
                                {
                                    // set the comment collection to try first
                                    comments = commentDictionary.Comments;

                                    // set the comment collection to try next
                                    comments2 = commentDictionary.CustomComments;
                                }
                            }
                            else
                            {
                                // set the comments
                                comments = commentDictionary.Comments;

                                // comments2 is not used
                            }

                            // if there are one or more comments in the comments collection
                            if (ListHelper.HasOneOrMoreItems(comments))
                            {
                                // iterate the comments
                                foreach (CM.CodeComment comment in comments)
                                {
                                    // local
                                    sourceCodeCopy = sourceCode;

                                    // reply any replacements to the source
                                    sourceCodeCopy = ApplyReplacements(sourceCodeCopy, comment.Replacements, ReplacementTargetEnum.ApplyToSource);
                                    
                                    // if this is a match
                                    if (IsMatch(comment, sourceCodeCopy, ref target, ref target2))
                                    {
                                        // a match was found
                                        matchFound = true;

                                        // replace target and target2 if any replacements exist
                                        target = ApplyReplacements(target, comment.Replacements, ReplacementTargetEnum.ApplyToTarget1);

                                        // apply any replacements to target2
                                        target2 = ApplyReplacements(target2, comment.Replacements, ReplacementTargetEnum.ApplyToTarget2);

                                        // replace out the * with the target
                                        commentText = comment.Comment.Replace("*", target);

                                        // replace out the % with the target2
                                        commentText = commentText.Replace("%", target2);

                                        // break out of the loop
                                        break;
                                    }
                                }
                            }

                            // if the commentText has not been set and comments2 collection has one or more items
                            if ((!matchFound) && (ListHelper.HasOneOrMoreItems(comments2)))
                            {
                                // iterate the comments
                                foreach (CM.CodeComment comment in comments2)
                                {
                                    // local
                                    sourceCodeCopy = sourceCode;

                                    // for debugging only
                                    string name = comment.Name;

                                    // set the CommentID
                                    int id = comment.ID;

                                    // if this is a match
                                    if (IsMatch(comment, sourceCode, ref target, ref target2))
                                    {
                                        // replace target and target2 if any replacements exist
                                        target = ApplyReplacements(target, comment.Replacements, ReplacementTargetEnum.ApplyToTarget1);

                                        // apply any replacements to target2
                                        target2 = ApplyReplacements(target2, comment.Replacements, ReplacementTargetEnum.ApplyToTarget2);

                                        // replace out the * with the target
                                        commentText = comment.Comment.Replace("*", target);

                                        // replace out the % with the target2
                                        commentText = commentText.Replace("%", target2);
                                        
                                        // break out of the loop
                                        break;
                                    }
                                }
                            }
                        }

                        // if the commentText exists
                        if (TextHelper.Exists(commentText))
                        {
                            // if a(n) is present, we need to determine if the target starts with a vowel or not
                            if (commentText.Contains("a(n)"))
                            {
                                // does the target start with a vowel?
                                bool startsWithVowel = DoesTargetStartWithAVowel(target);

                                // if the target starts with a vowel
                                if (startsWithVowel)
                                {
                                    // replace out a(n) with an
                                    commentText = commentText.Replace("a(n)", "an");
                                }
                                else
                                {
                                    // replace out a(n) with a
                                    commentText = commentText.Replace("a(n)", "a");
                                }
                            }
                        }
                    }
                }

                // return value
                return commentText;
            }
            #endregion

            #region IsMatch(CodeComment comment, string sourceCode, ref string target, ref string target2);
            /// <summary>
            /// This method is used to compare a regular expression with a line of source code.
            /// </summary>
            /// <param name="comment"></param>
            /// <param name="sourceCode"></param>
            /// <param name="target"></param>
            /// <returns></returns>
            public bool IsMatch(CodeComment comment, string sourceCode, ref string target, ref string target2)
            {
                // initial value
                bool isMatch = false;

                // if the pattern matches
                if (Regex.IsMatch(sourceCode, comment.Pattern, RegexOptions.IgnoreCase))
                {
                    // set to true
                    isMatch = true;

                    // if the comment has a TargetPattern
                    if (comment.HasTargetPattern)
                    {
                        // Set the target
                        target = SetTarget(comment.TargetPattern, sourceCode);
                    }

                    // if the comment has a TargetPattern
                    if (comment.HasTargetPattern2)
                    {
                        // set target2
                        target2 = SetTarget(comment.TargetPattern2, sourceCode);
                    }
                }

                // return value
                return isMatch;
            }
            #endregion

            #region SetTarget(string targetPattern, string sourceCode)
            /// <summary>
            /// This method returns the Target
            /// </summary>
            private string SetTarget(string targetPattern, string sourceCode)
            {
                // initial value
                string target = "";

                // if the TargetPattern exists and the sourceCode exists
                if (TextHelper.Exists(targetPattern, sourceCode))
                {
                    // get the matches
                    MatchCollection matches = Regex.Matches(sourceCode, targetPattern);

                    // if there is one or matches, we take the first match
                    if (matches.Count >= 1)
                    {
                        // iterate the collection of matches
                        foreach (Match match in matches)
                        {
                            // get the value of the match
                            target = match.Value;

                            // break out of the loop
                            break;
                        }
                    }

                    // if the target string is set
                    if (TextHelper.Exists(target))
                    {
                        // replace out the opening or closing parens
                        target = target.Replace("(", "").Replace(")", "");

                        // replace out the opening or closing < >
                        target = target.Replace("<", "").Replace(">", "");

                        // trim the target
                        target = target.Trim();
                    }
                }

                // return value
                return target;
            }
            #endregion
            
        #endregion

        #region Properties
        
            #region ID
            /// <summary>
            /// This read only property returns the value for 'ID'.
            /// </summary>
            public Guid ID
            {
                get
                {
                    // initial value
                    Guid id = new Guid(GuidID);
                    
                    // return value  
                    return id;
                }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
