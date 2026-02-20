using Lychgate;
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

	public Army GetHeroArmy()
	{
		return heroArmy;
	}

	public Army GetMobArmy()
	{
		return mobArmy;
	}

	private void printAction(String message)
	{
        Console.WriteLine(message);
    }

	private void EndBattle(Faction winner)
	{

	}

	public void FightRounds(int roundCount)
	{
		for (int curRound=0; curRound < roundCount; curRound++) {
			printAction(">> Round " + (curRound + 1));
			int roundResult = FightOneRound(curRound);
			if (roundResult == 0) {
                // if there were no turns taken last round and we're done with the battle. let's find out who won.
                ComUn topAliveHero = heroArmy.GetTopTargetOne();
				ComUn topAliveMob = mobArmy.GetTopTargetOne();
				if (topAliveHero.Idr == 0 & topAliveMob.Idr == 0)
				{
					//both sides dead
					printAction("All combatants have fallen... The battlefield is a desolate stalemate.");
					return;

				}
				else if (topAliveHero.Idr == 0)
				{
                    // all heroes dead; mobs win.
                    printAction("All the heroes have succumbed to their wounds. Heroes have been slaughtered and defeated...");
					return;
                }
				else if (topAliveMob.Idr == 0) {

                    // all mobs dead; heores win.
                    printAction("No enemies remain. The Heroes have won!");
                    return;
                }
                else
                {
					throw new Exception("Invalid Game State.");
                }


            }
		}
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

		//init local vars
		int netDmg = 0;

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
					//hero attacks
					netDmg = topHero.Attack(targetNow);
					targetNow.SubHP(netDmg);
					topHero.Exhaust();

                } else
				{
					// there are no eligible targets
					msgBus = "No targets that " + topHero.Name + " can hit.";
					printAction(msgBus);

					topHero.Exhaust();


                }
            } 
			else
            {
				// enemy mob take a turn

				msgBus = topMob.Name + " moves in for an attack...";
				printAction(msgBus);

                //get the top target from the hero army
                targetNow = heroArmy.GetTopTarget(0);
				if (targetNow != null && targetNow.Name != "null" && targetNow.Idr != 0)
				{
					//we have a valid target for sure
					msgBus = topMob.Name + " attacks " + targetNow.Name;
					printAction(msgBus);
					//mob attacks
					netDmg = topMob.Attack(targetNow);
					targetNow.SubHP(netDmg);
					topMob.Exhaust();

				}
				else
				{
					// there are no eligible targets
					msgBus = "No targets that " + topMob.Name + " can hit.";
					printAction(msgBus);

					topMob.Exhaust();

				}
            }
			//next turn
			topHero = heroArmy.TopOne();
			topMob = mobArmy.TopOne();

			msgBus = "Next Hero: " + topHero.Name + Environment.NewLine + "Next Enemy: " + topMob.Name;
            printAction(msgBus);
            turnCount++;
        }

		//ready up for next round
		ReadyArmies();

		return turnCount;
    }

	public void ReadyArmies()
	{
		heroArmy.ReadyUpAll();
		mobArmy.ReadyUpAll();

	}

}
