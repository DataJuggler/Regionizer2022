# Regionizer2022
Regionizer is a C# document formatter, code generation tool and auto commenting system.

I have an old video my YouTube channel from 2014, but a new one is my list for 2022.
https://youtu.be/lB86fBS0fZg


This project is for Visual Studio 2022 only. For 2017 or 2019, please see:
https://github.com/DataJuggler/Regionizer

All new development will be on Visual Studio 2022 (most likely).

To install:
Navigate to the Install folder, double click Regionizer2022.vsix

To run the project, open Regionizer2022.sln, and start debugging (switch to debug mode will be required).

To setup the Auto Commenting System, click the Setup Comment Dictionary, and browse the comment dictionary located here:
Regionizer2022\Regionizer\Dictionary\CommentDictionary.xml

The autocommenting uses regular expresions.

Once you get the comment dictionary installed.

Here is an example from Windows Forms:


InitializeComponent();

Place your mouse over the InitializeComponent line below and hit Control + Shift:

// Create controls 
InitializeComponent();

The comment is typed automatically.

#Text Editing#

Format Document

Your document is formatted into regions.

Format Selection

The selected method, event or property is formatted. If the region for Methods, Events or Properties does not exist in your document, a message box is shown.

Create Properties
Select one or more private variables and click the Create Properties button.

Methods

Add Method or Add Event are the only two methods I have added so far.

It is on my list to update this to include args, but Visual Studio has some ways to do this.

I will update this more when I have time.



