using Lychgate;
using System;
public class Battle
{
	private Army heroArmy;
	private Army mobArmy;
	public Battle(Army inHeroArmy, Army inMobArmy)
	{
		this.heroArmy = inHeroArmy;
		this.mobArmy = inMobArmy;

		//todo: spin up logger
	}

	private void printAction(String message)
	{
        Console.WriteLine(message);
    }

	public int FightOneRound(int curRndCount)
	{
		// called to fight one round of combat, iterates through all eligible units to attack an eligible target
		// returns the number of turns of combat that took place (i.e. each attack)
		int turnCount = 0;
		String msgBus = "";

		msgBus = "Round " + curRndCount + " begins...";
		printAction(msgBus);

		//init our top hero and enemy (mob)
		ComUn topHero = heroArmy.TopOne();
        ComUn topMob =  mobArmy.TopOne();
		ComUn targetNow; //empty for now, placeholder

		//keep fighting until we run out of valid heros or mobs
		while (topHero.Idr != 0 && topMob.Idr != 0)
		{
			//compare the top two initative (Ini) of each of the two armies
			if (topHero.Ini >= topMob.Ini)
			{
				// hero takes a turn
				msgBus = topHero.Name + " is up to strike.";
				printAction(msgBus);

				//get the top target from the mob army
				targetNow = mobArmy.GetTopTarget(0);
				//check if that target is valid
				if (targetNow != null && targetNow.Name != "null" && targetNow.Idr != 0) {
                    //we have a valid target for sure
                    msgBus = topHero.Name + " attacks " + targetNow.Name;
                    printAction(msgBus);

					//ComUn.Attack(targetNow);
					// kb: pausing here becuase ComUn.Attack() is not currently implemented.
                }
            } 
			else
            {
				// enemy mob take a turn
            }
			turnCount++;
        }

		return turnCount;
    }
}
