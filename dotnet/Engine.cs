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
using System.Xml;

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
        string readName;
        int readIdr;
        int readHp;
        int readMaxHp;
        double readAccuracy;
        int readDmg;
        int readAc;
        double readDodge;
        int readIni;
        bool readAlive;
        bool readReady;
        Faction readAlignment;

        //kb 2 16 2026
        //thinking we throw this method in the fucking trash and start again.
        //way too complicated.
        //use this instead: https://stackoverflow.com/a/55840

        //init lists
        //armies are represented as basic lists during config parse then tranformed into Army object
        List<ComUn> buildHeroArmyList = new();
        List<ComUn> buildMobArmyList = new();

        XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(XmlFilePath);

		//get all elements 
		XmlNodeList armiesList = xmlDoc.GetElementsByTagName("unit");
        //armiesList is a 2D list

        //iterate through our armiesList
		foreach (XmlNode node in armiesList)
		{
			// node is the xml representation of the unit
			// ref unit for army insert
			ComUn insUnit;

            //parse name
       
            //todo: implement... something's missing. I think we need this format:
            // <key="name" value="Hero1" />
            //instead of this format:
            // <name>Hero1</name>
		}
    }
}
