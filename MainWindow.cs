using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Gtk;

//GTK window
public partial class MainWindow: Gtk.Window
{	
	//this list will hold all the fonts
	private List<Font> AllFonts = new List<Font>();

	//create the window
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		Title = "FontApp";

		//personal fonts
		GetFonts(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Library/Fonts");

		//System Fonts
//		GetFonts(@"/Library/Fonts");

		//Sort Fonts by Name
		AllFonts.Sort();

		//Add the Fonts to the Window
		AddFontsToWindow();

		//Show everything
		ShowAll();
	}

	//Get the fonts stored in a path/Directory
	private void GetFonts(string Path)
	{
		foreach(string fileName in Directory.GetFiles(Path))
		{
			//filter some system files out
			if (!fileName.Contains(".DS_Store") 
			     && !fileName.Contains("fonts.dir")
			    && !fileName.Contains("fonts.list")
			    && !fileName.Contains("fonts.scale")
			    )
			{
				//add font to list
				AllFonts.Add (new Font(Path,fileName));

				//print out the font name in the console
//				Console.WriteLine(fileName);

			}
		}
	}

	//Add the fonts to the window
	private void AddFontsToWindow()
	{
		//get font count
		FontTable.NRows = (uint) AllFonts.Count ;

		//make count number
		uint FontNum;
		FontNum = 1;
		foreach (Font F in AllFonts)
		{
			//make font label and add it to the window
			Label FL = new Label(F.FontName);
			FL.Xalign = 0.1f;
			FontTable.Attach(FL, 0,1,FontNum -1, FontNum);

			FL.ModifyFont(Pango.FontDescription.FromString(F.FontName + " 16"));
			FL.Justify = Justification.Left;

			//Add open in finder button to window
			FontTable.Attach(F.OpenInFinder(), 1,2,FontNum -1, FontNum);

			FontNum = FontNum +1;
		}
	}
	
	//close the window
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
