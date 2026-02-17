using System;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Text;

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
		XDocument xdoc = new();
		//step 1: open file as text
		using (StreamReader reader = File.OpenText(XmlFilePath))
		{
            xdoc = XDocument.Load(reader);

        }
		


	}
}
