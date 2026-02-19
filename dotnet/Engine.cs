using Lychgate;
using Newtonsoft.Json;

//todo: import xml reader, army class, comUn class, battle class
public class LychEngine
{
	private string FilePath;
    private Battle battle;

    public LychEngine(string inFileName)
    {
        FilePath = inFileName;
        InitializeSim();
    }

    private void InitializeSim()
    {
        Army[] armies = ParseConfig(FilePath);
        this.battle = new Battle(armies[0], armies[1]);
    }
    public Army[] ParseConfig(string inStrFilePath)
    {
        FilePath = inStrFilePath;
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

}
