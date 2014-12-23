using System;
using System.Collections.Generic;
using Gtk;

//the class to create font objects
public class Font : IComparable<Font> //inherits from IComparable...for sorting reasons
{	
	//basic variables
	public string Path = "";
	public string FullName = "";
	public string FileName = "";
	public string FontName = "";

	//constructor
	public Font(string P, string N)
	{
		Path = P;
		FullName = N;
		FileName = FullName.Replace(Path,"").Replace("/","");
		FontName = FileName.Substring(0,FileName.Length-4);
	}

	//allows me to make a open in finder button for the font
	public Button OpenInFinder()
	{
		Button FB = new Button("Open in Finder");

		FB.Clicked += delegate(object sender, System.EventArgs e) 
		{
			string arguments = ("-R " + FullName.Replace(" ","' '"));
			System.Diagnostics.Process.Start("open", arguments);
		};

		return FB;
	}

	//allows me to sort the font list
	public int CompareTo(Font other)
	{
		return String.Compare(FontName,other.FontName);
	}
}
