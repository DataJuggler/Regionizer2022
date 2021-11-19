

#region using statements

using DataJuggler.Core.UltimateHelper;
using DataJuggler.Regionizer.CodeModel.Objects;
using System;
using System.Collections.Generic;
using XmlMirror.Runtime.Objects;
using XmlMirror.Runtime.Util;
using DataJuggler.Regionizer.CodeModel.Enumerations;

#endregion

namespace DataJuggler.Regionizer.Parsers
{

    #region class CommentDictionaryParser : ParserBaseClass
    /// <summary>
    /// This class is used to parse 'CodeComment' objects.
    /// </summary>
    public partial class CommentDictionaryParser : ParserBaseClass
    {

        #region Private Variables
        // Source Values
        private const string GreaterThanSymbol = ">";
        private const string LessThanSymbol = "<";
        private const string AmpersandSymbol = "&";
        private const string PercentSymbol = "%";

        // EncodedValues
        private const string GreaterThanCode = "&gt;";
        private const string LessThanCode = "&lt;";
        private const string AmpersandCode = "&amp;";
        private const string PercentCode = "&#37;";
        #endregion
        
        #region Events

            #region Parsing(XmlNode xmlNode)
            /// <summary>
            /// This event is fired BEFORE the collection is initialized.
            /// An example of this is the codeComments (plural) node.
            /// </summary>
            /// <param name="xmlNode"></param>
            /// <returns>True if cancelled else false if not.</returns>
            public bool Parsing(XmlNode xmlNode)
            {
                // initial value
                bool cancel = false;

                // Add any pre processing code here. Set cancel to true to abort parsing this collection.

                // return value
                return cancel;
            }
            #endregion

            #region Parsing(XmlNode xmlNode, ref CodeComment codeComment)
            /// <summary>
            /// This event is fired when a single object is initialized.
            /// An example of this is the codeComment (singlular) node.
            /// </summary>
            /// <param name="xmlNode"></param>
            /// <param name="codeComment"></param>
            /// <returns>True if cancelled else false if not.</returns>
            public bool Parsing(XmlNode xmlNode, ref CodeComment codeComment)
            {
                // initial value
                bool cancel = false;

                // Add any pre processing code here. Set cancel to true to abort adding this object.

                // return value
                return cancel;
            }
            #endregion

            #region Parsed(XmlNode xmlNode, ref CodeComment codeComment)
            /// <summary>
            /// This event is fired AFTER the codeComment is parsed.
            /// </summary>
            /// <param name="xmlNode"></param>
            /// <param name="codeComment"></param>
            /// <returns>True if cancelled else false if not.</returns>
            public bool Parsed(XmlNode xmlNode, ref CodeComment codeComment)
            {
                // initial value
                bool cancel = false;

                // if the comment is not valid
                if (!codeComment.IsValid)
                {
                    // since the comment is not valid, cancel the adding of this comment
                    cancel = true;
                }
                else
                {
                    // for debugging only
                    string name = codeComment.Name;

                    // if the Pattern exists
                    if (codeComment.HasPattern)
                    {
                        // we need to replace out any reserve word characters for the following items (<,>,&,%)
                        codeComment.Pattern = ReplaceReserveCharacters(codeComment.Pattern);
                    }

                    // if the TargetPattern exists
                    if (codeComment.HasTargetPattern)
                    {
                        // we need to replace out any reserve word characters for the following items (<,>,&,%)
                        codeComment.TargetPattern = ReplaceReserveCharacters(codeComment.TargetPattern);
                    }

                    // if the TargetPattern2 exists
                    if (codeComment.HasTargetPattern2)
                    {
                        // we need to replace out any reserve word characters for the following items (<,>,&,%)
                        codeComment.TargetPattern2 = ReplaceReserveCharacters(codeComment.TargetPattern2);
                    }

                    // if the codeComment has a Replacements collection, we must load it.
                    if (codeComment.HasReplacements)
                    {
                        // Create a new instance of a 'ReplacementParser' object.
                        ReplacementParser replacementParser = new ReplacementParser();

                        // If the replacementParser object exists
                        if (replacementParser != null)
                        {
                            // set the CommentID so we know which set of Replacements to load
                            replacementParser.CommentID = codeComment.ID;

                            // parse the replacements
                            codeComment.Replacements = replacementParser.ParseReplacements(xmlNode.RootNode);
                        }
                    }
                }

                // return value
                return cancel;
            }
            #endregion

        #endregion

        #region Methods

            #region ReplaceReserveCharacters(string pattern)
            /// <summary>
            /// This method returns the Reserve Characters
            /// </summary>
            private string ReplaceReserveCharacters(string pattern)
            {
                // initial value
                string encodedPattern = "";

                // if the pattern exists
                if (TextHelper.Exists(pattern))
                {
                    // replace out each of the values that need to be replaced
                    // had to be done in the reverse order it was put in
                    pattern = pattern.Replace(PercentCode, PercentSymbol);
                    pattern = pattern.Replace(AmpersandCode, AmpersandSymbol);
                    pattern = pattern.Replace(LessThanCode, LessThanSymbol);
                    encodedPattern = pattern.Replace(GreaterThanCode, GreaterThanSymbol);
                }

                // return value
                return encodedPattern;
            }
            #endregion
            
        #endregion

    }
    #endregion

}
