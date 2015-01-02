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
		GetFonts(@"/Library/Fonts");

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
			//Add Font Label to window
			FontTable.Attach(F.FontLabel(), 0,1,FontNum -1, FontNum);

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

	protected void OnSearchChanged (object sender, EventArgs e)
	{
//		Console.WriteLine(Search.Text);

		//Remove all the old stuff
		foreach (Widget W in FontTable.Children)
		{
//			Console.WriteLine(W.Name);
			FontTable.Remove(W);
			W.Destroy();
		}

		//set deault rows and columns
		FontTable.NRows = (uint) 3;
		FontTable.NColumns = (uint) 2;

		//make a SearchResults list
		List<Font> SearchResults = new List<Font>();

		//add matching fonts to the SearchResults list
		uint SearchCount;
		SearchCount = 0;
		foreach (Font F in AllFonts)
		{
//			Console.WriteLine(F.FontName.Contains(Search.Text).ToString());

			if (F.FontName.ToUpper().Contains(Search.Text.ToUpper()))
			{
				SearchResults.Add (F);
				SearchCount = SearchCount + 1;
			}
		}

		//add results to window
		try
		{
			FontTable.NRows = (uint) 3;
			FontTable.NColumns = (uint) 2;
			FontTable.NRows = SearchCount;

			uint SA;
			SA = 1;
			foreach (Font SR in SearchResults)
			{
				//Add Font Label to window
				FontTable.Attach(SR.FontLabel(), 0,1,SA -1, SA);
				
				//Add open in finder button to window
				FontTable.Attach(SR.OpenInFinder(), 1,2,SA -1, SA);

				SA = SA + 1;
			}

			//show everything
			ShowAll();

		}
		catch(Exception)
		{}

	}


}
