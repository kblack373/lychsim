using Lychgate;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
public class LychEngine
{
    #region instanceVars
    private string FilePath;
    private Battle Battle;
    #region schema
    private const string JsonSchema = @"{
  '$schema': 'http://json-schema.org/draft-07/schema#',
  'title': 'Generated schema for Root',
  'type': 'array',
  'items': {
    'type': 'object',
    'properties': {
      'Name': {
        'type': 'string'
      },
      'Idr': {
        'type': 'number'
      },
      'Hp': {
        'type': 'number'
      },
      'Accuracy': {
        'type': 'number'
      },
      'Dmg': {
        'type': 'number'
      },
      'Ac': {
        'type': 'number'
      },
      'Dodge': {
        'type': 'number'
      },
      'Ini': {
        'type': 'number'
      },
      'Align': {
        'type': 'string'
      }
    },
    'required': [
      'Name',
      'Idr',
      'Hp',
      'Accuracy',
      'Dmg',
      'Ac',
      'Dodge',
      'Ini',
      'Align'
    ]
  }
}";
    #endregion
    #endregion
    #region constructors
    public LychEngine(string inFileName)
    {
        FilePath = inFileName;
        InitializeSim();
    }

    private void InitializeSim()
    {
        Army[] armies = ParseConfig(FilePath);
        this.Battle = new Battle(armies[0], armies[1]);
    }
    #endregion
    #region classMethods
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
           
            //validate JSON here
            //https://www.newtonsoft.com/jsonschema/help/html/ValidatingJson.htm
            //first, read in the text as a basic JSON reader object to handle the text
            JsonTextReader basicReader = new JsonTextReader(new StringReader(json));

            //second, use that reader object to init a JSchemaValidatingReader object
            //this will do the actual validation logic
            JSchemaValidatingReader valReader = new JSchemaValidatingReader(basicReader);
            valReader.Schema = JSchema.Parse(JsonSchema);
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                //pass in validated reader
                unitsFlat = serializer.Deserialize<List<ComUn>>(valReader);

            }
            catch
            {
                throw new Exception("Error desrializing text file to JSON. File: " + FilePath + "\n" + json);
            }

            if (unitsFlat is not null && unitsFlat.Count>0)
            {
                return unitsFlat;

            }
            else
            {
                throw new Exception("Config file returned blank or null units. This means the file is empty or corrupt or some other horrible 3rd thing. Check the file: " + FilePath + "\n >> This is what I read, OK? \n " + json);
                }
           } 


       else
        {
            throw new Exception("Battle config file not found at path: " + FilePath);
        }

    }

    public void RunSim()
    {
        Battle.GetHeroArmy().Report();
        Battle.GetMobArmy().Report();

        Battle.FightRounds(50);
    }

    #endregion
}
