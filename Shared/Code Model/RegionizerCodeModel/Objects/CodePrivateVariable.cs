

#region using statements

using DataJuggler.Core.UltimateHelper;
using DataJuggler.Core.UltimateHelper.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Objects
{

    #region class CodePrivateVariable : CodeLine
    /// <summary>
    /// This class represents a PrivateVariable in a Class File.
    /// </summary>
    public class CodePrivateVariable : CodeLine
    {

        #region Constructor
        /// <summary>
        /// Create a Private Variable object
        /// </summary>
        /// <param name="textLine"></param>
        public CodePrivateVariable(TextLine textLine) : base(textLine)
        {
            if (HasTextLine)
            {
                // Get the words
                TextLine.Words = WordParser.GetWords(TextLine.Text);
            }
        } 
        #endregion

        #region Properties

            #region PrivateVariableName
            /// <summary>
            /// This read only property returns the value of PrivateVariableName from the object TextLine.
            /// </summary>
            public string PrivateVariableName
            {
                
                get
                {
                    // initial value
                    string privateVariableName = "";
                    
                    // if TextLine exists
                    if ((HasTextLine) && (TextLine.HasWords))
                    {
                        // if there are one or more words
                        if (ListHelper.HasXOrMoreItems(TextLine.Words, 3))
                        {
                            // set the return value
                            privateVariableName = TextLine.Words[2].Text;
                        }
                    }
                    
                    // return value
                    return privateVariableName;
                }
            }
            #endregion
            
        #endregion

    } 
    #endregion

}
