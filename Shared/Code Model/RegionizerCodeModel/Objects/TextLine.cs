

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataJuggler.Regionizer.CodeModel.Util;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Objects
{

    #region class TextLine
    /// <summary>
    /// This class is represents one line of text.
    /// </summary>
    public class TextLine
    {
        
        #region Private Variables
        private string text;
        private List<Word> words;
        private int lineNumber;
        #endregion

        #region Constructors

            public TextLine(string text)
            {
                // Set the text
                this.Text = text;

                // Parse the words
                this.Words = CSharpCodeParser.ParseWords(text);
            }

            public TextLine(string text, int lineNumber)
            {
                // Set the text
                this.Text = text;

                // Set the LineNumber
                this.LineNumber = lineNumber;

                // Parse the words
                this.Words = CSharpCodeParser.ParseWords(text);
            }

        #endregion

        #region Properties

            public int LineNumber
            {
                get { return lineNumber; }
                set { lineNumber = value; }
            }

            public string Text
            {
                get { return text; }
                set { text = value; }
            }

            public List<Word> Words
            {
                get { return words; }
                set { words = value; }
            }

        #endregion

    } 
    #endregion

}
