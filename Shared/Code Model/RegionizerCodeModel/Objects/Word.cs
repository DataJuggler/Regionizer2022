using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataJuggler.Regionizer.CodeModel.Objects
{
    public class Word
    {
        private string text;

        public Word(string text)
        {
            // set the text
            this.Text = text;
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}
