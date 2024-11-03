Regionizer 2022 is a Visual Studio package that serves as a Document Formatter, Code Generation tool and 
Auto Commenting System.

# Update 5.19.2024 - New Video

New Video:

First Ever Opensource Saturday - Sunday Edition
https://youtu.be/uxa1xR6xpzk


# Installation Instructions

To install Regionizer, browse for the Install folder in this project, and double the file Regionizer2022.vsix.

After install, in Visual Studio select the Tools menu item, and you should see Regionizer.

This will popup the Regionizer Tool Window. Dock the tool window in the same location as Solution Explorer, 
Properties, etc.

Auto Commenting - To setup the Auto Commenting System, click the button link in Regionizer 2022 for
Setup Comment Dictionary. Browse for the folder you downloaded or cloned Regionizer 2022 into, and browse
for the Dictionary folder.

To apply an auto comment, place your cursor over a line of code, and hit control + shift.
If a match is found for this text, the comment will get written on the line you had your cursor at.

Examples:

    // example
	bool visible = false;

    (hit control shift here)
    if (visible)
	{
		
	}

Comment will be

	// if the value for visible is true
	if (visible)
	{
		
	}

	// get the files using FileHelper (part of DataJuggler.UltimateHelper)
	List<string> files = FileHelper.GetFiles(path, "", false);

	(hit control shift here)
	if (ListHelper.HasOneOrMoreItems(files))
	{

	}

Comment will be

	// If the files collection exists and has one or more items
	if (ListHelper.HasOneOrMoreItems(files))
	{

	}

You can also create a custom dictionary. The dictionary uses regular expressions.

# News

New video released that uses Regionizer

Create a Stock Predictor With C# and ML.NET Part I
https://youtu.be/hF8LkvwOXQY




	