using Lychgate;
using System.Xml;
using Newtonsoft.Json;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
//todo: import xml reader, army class, comUn class, battle class
public class LychEngine
{
	private string FilePath;
	public LychEngine()
    { }


    public Army[] ParseConfig(string inStrFilePath)
    {
        this.FilePath = inStrFilePath;
        Army[] armies = [];
        List<ComUn> listHeroes = new();
        List<ComUn> listMobs = new();

        List<ComUn> configUnits = ParseJsonConfig();

        //now sort units to get armies
        foreach (ComUn unit in configUnits)
        {
            if (unit.Alignment == Faction.Heroes)
            {
                listHeroes.Add(unit);

            } else if (unit.Alignment == Faction.Enemies)
            {
                listMobs.Add(unit);
            }
        }

        Army heroArmy = new Army(listHeroes);
        Army mobArmy = new Army(listMobs);

        armies = [heroArmy, mobArmy];

        return armies;
    }

    public List<ComUn> ParseJsonConfig()
    {
        // fuck this Markup Language bullshit, we doin javascript babyyyy
        // json serialization: https://www.newtonsoft.com/json/help/html/SerializingCollections.htm


        if (File.Exists(FilePath))
        {
            //file is good to go
            string json = "";
            
            //placeholder list to receive data
            List<ComUn> unitsFlat = new List<ComUn>();

            json = File.ReadAllText(FilePath);
            unitsFlat = JsonConvert.DeserializeObject<List<ComUn>>(json);

            if (unitsFlat.Count>0)
            {
                return unitsFlat;

            }
            else
            {
                throw new Exception("Config file returned blank units. This means the file is empty or corrupt or some other horrible 3rd thing. Check the file: " + FilePath + "\n >> This is what I read, OK? \n " + json);
                }
           } 


       else
        {
            throw new Exception("Battle config file not found at path: " + FilePath);
        }

    }

	public void OpenXml()
	{

        //obsolete, preserved in case I want to copy these local declarations 

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
		xmlDoc.LoadXml(FilePath);

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
