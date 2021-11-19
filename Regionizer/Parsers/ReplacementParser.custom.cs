

#region using statements

using DataJuggler.Core.UltimateHelper;
using DataJuggler.Regionizer.CodeModel.Objects;
using System;
using System.Collections.Generic;
using XmlMirror.Runtime.Objects;
using XmlMirror.Runtime.Util;

#endregion

namespace DataJuggler.Regionizer.Parsers
{

    #region class ReplacementParser : ParserBaseClass
    /// <summary>
    /// This class is used to parse 'Replacement' objects.
    /// </summary>
    public partial class ReplacementParser : ParserBaseClass
    {

        #region Events

            #region Parsing(XmlNode xmlNode)
            /// <summary>
            /// This event is fired BEFORE the collection is initialized.
            /// An example of this is the replacements (plural) node.
            /// </summary>
            /// <param name="xmlNode"></param>
            /// <returns>True if cancelled else false if not.</returns>
            public bool Parsing(XmlNode xmlNode)
            {
                // initial value
                bool cancel = false;

                try 
                {
                    // set the commentID
                    int commentID = NumericHelper.ParseInteger(xmlNode.ParentNode.ChildNodes[0].FormattedNodeValue, 0, -1);

                    // if this is the Comment being sought
                    if (commentID != this.CommentID)
                    {
                        // Set the value for the cancel to true
                        cancel = true;
                    }
                }
                catch(Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }

                // return value
                return cancel;
            }
            #endregion

            #region Parsing(XmlNode xmlNode, ref Replacement replacement)
            /// <summary>
            /// This event is fired when a single object is initialized.
            /// An example of this is the replacement (singlular) node.
            /// </summary>
            /// <param name="xmlNode"></param>
            /// <param name="replacement"></param>
            /// <returns>True if cancelled else false if not.</returns>
            public bool Parsing(XmlNode xmlNode, ref Replacement replacement)
            {
                // initial value
                bool cancel = false;

                // Add any pre processing code here. Set cancel to true to abort adding this object.

                // return value
                return cancel;
            }
            #endregion

            #region Parsed(XmlNode xmlNode, ref Replacement replacement)
            /// <summary>
            /// This event is fired AFTER the replacement is parsed.
            /// </summary>
            /// <param name="xmlNode"></param>
            /// <param name="replacement"></param>
            /// <returns>True if cancelled else false if not.</returns>
            public bool Parsed(XmlNode xmlNode, ref Replacement replacement)
            {
                // initial value
                bool cancel = false;

                // Add any post processing code here. Set cancel to true to abort adding this object.

                // return value
                return cancel;
            }
            #endregion

        #endregion

    }
    #endregion

}