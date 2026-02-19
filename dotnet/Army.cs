using Lychgate;

//todo: import comUn class

public class Army
{
	//define instance variables
	//list of units; List predefined to hold type ComUn (combatUnit)
	private List<ComUn> unitList = new List<ComUn>();
	public Army(List<ComUn> inListUnits)
	{
		//take in list of units
		this.unitList = inListUnits;
		this.SortSelf();
		return;
	}

	public bool SortSelf()
	{

		//make local version
		List<ComUn> units = this.unitList;
        //sort using initative of ComUn as key
        //comparison delegate see: https://youtu.be/uRaO8HLoHAs?t=205
        units.Sort((unit1, unit2) => unit1.Ini.CompareTo(unit2.Ini));

		//reverse list since we want to sort descending instead of asc
		units.Reverse();
        //swap instance list with sorted list
        this.unitList = units;
		return true;
	}

	public int GetCount()
	{
		int len = this.unitList.Count;
		return len;

	}

	public int Add(ComUn inUnit)
	{
		//adds a unit, sorts itself, then returns the new length
		this.unitList.Add(inUnit);
		this.SortSelf();
		return this.GetCount();
	}

	public List<String> Report()
	{
		//define a bus of strings; a list of strings that comprise a full report message.
		List<String> rptStrBus = new List<String>();

		//header
		const String margin = "------------";
		String line = "";
		//iterate through unit list
		List<ComUn> units = this.unitList;
		foreach (ComUn u in units)
		{
			
			rptStrBus.Append(margin);
			line = u.Name + " id: #" + u.Idr;
			rptStrBus.Append(line);
            rptStrBus.Append(margin);
			line = "Hit Points: " + u.Hp;
            rptStrBus.Append(line);
            if (u.Alive)
			{
                line = "Status: Alive";

            } else
			{
				line = "Status: Dead";
			}
			rptStrBus.Append(line);
			line = "Accuracy: " + u.Accuracy;
            rptStrBus.Append(line);
			line = "Damage: " + u.Dmg;
			rptStrBus.Append(line);
			line = "Evasion Chance: " + u.Dodge;
			rptStrBus.Append(line);
			line = "Initiative: " + u.Ini;
			rptStrBus.Append(line);
			line = "";
            rptStrBus.Append(line);

        }

        return rptStrBus;
	}

	private ComUn Top(int index)
	{
		//recursive function to find the top ready unit
		ComUn rtnUnit;
		//check if the index is out of range
		if (this.unitList.Count >= index + 1)
		{
			//inspect the unit of the current index
			rtnUnit = this.unitList[index];
			//check if that unit is ready
			if (rtnUnit.Ready)
			{
				//if the unit is ready, then they are the top ready unit.
				return rtnUnit;

			} 
			else
			{
				//otherwise we need to go to the next lowest initiative.
				//because the Army class is self-sorting, we know the next index will be the next highest initative.
				//so we increment our current index then call this method on itseld again.
				return this.Top(index + 1);
			}
		}
		else
		{
			//if we reach this section, it means there's noone left in the army.
			//to signify this, we return a unit with a NULL name and all 0s
			return new ComUn("null", 0, 0, 0, 0, 0, 0, 0);
			//this terminates the round sequence in the Battle class.
		}

	}
    public ComUn TopOne()
    {
        ComUn payload = this.Top(0);
		return payload;
	}
	public ComUn GetTopTarget(int index)
	{
        // this is the SAME LOGIC as this.Top() with the difference being that this
        // only returns the top ALIVE unit instead of top ready unit

		ComUn rtnUnit;
        //check if the index is out of range
        if (this.unitList.Count >= index + 1)
        {
            //inspect the unit of the current index
            rtnUnit = this.unitList[index];
			//check if that unit is ready
			if (rtnUnit.Alive)
            {
                //if the unit is ready, then they are the top ready unit.
                return rtnUnit;

            }
            else
            {
                //otherwise we need to go to the next lowest initiative.
                //because the Army class is self-sorting, we know the next index will be the next highest initative.
                //so we increment our current index then call this method on itseld again.
                return this.Top(index + 1);
            }
        }
        else
        {
            //if we reach this section, it means there's noone left in the army.
            //to signify this, we return a unit with a NULL name and all 0s
            return new ComUn("null", 0, 0, 0, 0, 0, 0, 0);
            //this terminates the round sequence in the Battle class.
        }


    }

	public ComUn GetTopTargetOne()
	{

		return GetTopTarget(0);
	}

	public void ReadyUpAll()
	{
		// in python we do this by passsing by reference and using local references
		// C# is pass-by-value so I implemented ComUn.ReadyUp() in that class
		foreach (ComUn u in this.unitList)
		{
			u.ReadyUp();
		}
	}
}

