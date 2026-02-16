using Lychgate;
using System;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Swift;
using System.Runtime.Intrinsics.Arm;
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
		
		//swap instance list with sorted list
		this.unitList = units;
		return true;
	}

	public int GetLength()
	{
		int len = this.unitList.Count;
		return len;

	}

	public int Add(ComUn inUnit)
	{
		//adds a unit, sorts itself, then returns the new length
		this.unitList.Add(inUnit);
		this.SortSelf();
		return this.GetLength();
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
			line = "Accuracy: " + u.HitChance;
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
}
