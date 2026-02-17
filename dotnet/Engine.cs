using System;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Java;
using System.Drawing;
using System.Security.Principal;
using Lychgate;
using System.ComponentModel.Design;
using System.Diagnostics.Tracing;

//todo: import xml reader, army class, comUn class, battle class
public class LychEngine
{
	private string XmlFilePath;
	public LychEngine(string inXmlFilePath)
	{
		XmlFilePath = inXmlFilePath;
		return;
	}

	public void OpenXml()
	{
        // reading in the XML using LINQ 
        // https://stackoverflow.com/a/670569
        XDocument xdoc = XDocument.Load(XmlFilePath);
		//step 1: open file as text

		var lv1s = from lv1 in xdoc.Descendants("army")
				   select new
				   {

					   align = lv1.Attribute("alignment").Value,
					   units = lv1.Descendants("unit")
						
				   };

		foreach (var lv1 in lv1s)
		{
			//iterate through
			string sideStr = lv1.align.ToString();
			Faction sideFac;
			if (sideStr == "heores")
			{
				sideFac = Faction.Heroes;
			} else if (sideStr == "enemies")
			{
				sideFac = Faction.Enemies;
			} else
			{
				throw new Exception("undefined army <alignment> element.");
				return;
			}
			foreach (var lv2 in lv1.units)
			{
				//to do: populate units

			}

		}
		


	}
}
