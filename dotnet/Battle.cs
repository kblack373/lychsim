using Lychgate;
using System.ComponentModel.Design;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.Swift;
using System.Xml.Linq;
public class Battle
{
	private Army heroArmy;
	private Army mobArmy;
	public  CombatLogger Log { get; set; }

	//add local vars to track current round and turn
	private int CurrentRound;
	private int CurrentTurnOfRound;

	//constant for data output
	private const string DefaultFilePath = "default.csv";

	public Battle(Army inHeroArmy, Army inMobArmy)
	{
		this.heroArmy = inHeroArmy;
		this.mobArmy = inMobArmy;

		//todo: spin up logger
		Log = new CombatLogger();
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

		}
		WriteLogs();
		
	}

    private void WriteLogs()
    {
        string filePath = Environment.ExpandEnvironmentVariables(DefaultFilePath);
        this.Log.WriteCsvLog(filePath);
    }

    public int FightUntilVictor()
	{
		bool result = true;
		int curRound = 0;
        while (result)
        {
            printAction(">> Round " + (curRound));
            curRound++;
            int roundResult = FightOneRound(curRound);
			
            // if there were no turns taken last round and we're done with the battle. let's find out who won.
            ComUn topAliveHero = heroArmy.GetTopTargetOne();
            ComUn topAliveMob = mobArmy.GetTopTargetOne();
            if (topAliveHero.Idr == 0 & topAliveMob.Idr == 0)
            {
                //both sides dead
                printAction("All combatants have fallen... The battlefield is a desolate stalemate.");
                result = false;

            }
            else if (topAliveHero.Idr == 0)
            {
                // all heroes dead; mobs win.
                printAction("All the heroes have succumbed to their wounds. Heroes have been slaughtered and defeated...");
                result = false;

            }
            else if (topAliveMob.Idr == 0)
            {

                // all mobs dead; heores win.
                printAction("No enemies remain. The Heroes have won!");
                result = false;
            }

        }
        WriteLogs();
        return curRound;
    }

	private int UnitAttackTarget(ComUn unit, ComUn target)
	{
		//logic to handle different attack results
		int signalOut;
		//init Log with all the stuff we know
		CombatLog newLog;
		newLog = new CombatLog( this.CurrentRound, this.CurrentTurnOfRound, unit.Name, unit.Idr,  unit.Accuracy,  target.Name, target.Idr, target.Dodge,  target.Ac);

		//prep to get the swing result "swingrating" by reference
		double swingRating = 0.0;
        int result = unit.Attack(target, ref swingRating);
		//swingRating is now populated with the result of the "to hit" roll.
		newLog.HitRollResult = swingRating;

        unit.Exhaust();
        if (result == 0)
		{
			//attack hits but does not penetrate armor
			newLog.HitSuccess = true;
            newLog.Damage = 0;
            newLog.TargetRemainHp = target.Hp;
            signalOut = 0;
		}
		else if (result < 0)
		{
            // attack is a miss
            newLog.HitSuccess = false;
			newLog.Damage = 0;
            newLog.TargetRemainHp = target.Hp;
            signalOut = 0;


        } else
		{
            // then the result is applied as damage
            newLog.HitSuccess = true;
            target.SubHP(result);
			newLog.Damage = result;
			newLog.TargetRemainHp = target.Hp;
			//check target death
			if (target.CheckDeath())
			{
                string msg = target.Name + "takes mortal damage and falls to their wounds.";
				printAction(msg);
            }

            //exhaust attacker

            signalOut = result;
		}

		this.Log.InsertLog(newLog);
		return signalOut;
    }

	public int FightOneRound(int curRndCount)
	{
		// called to fight one round of combat, iterates through all eligible units to attack an eligible target
		// returns the number of turns of combat that took place (i.e. each attack)
		int turnCount = 0;
		String msgBus = "";

		msgBus = "Round " + curRndCount + " begins...";
		printAction(msgBus);
        CurrentRound = curRndCount;
        //init our top hero and enemy (mob)
        ComUn topHero = heroArmy.TopOne();
        ComUn topMob =  mobArmy.TopOne();
		ComUn targetNow; //empty for now, placeholder

		//init local vars
		int netDmg = 0;

		//keep fighting until we run out of valid heros or mobs
		while (topHero.Idr != 0 || topMob.Idr != 0)
		{
			CurrentTurnOfRound = turnCount;
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
					netDmg = UnitAttackTarget(topHero, targetNow);
					AttackResponse(netDmg, topHero, targetNow);

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
					netDmg = UnitAttackTarget(topMob, targetNow);
                    AttackResponse(netDmg, topMob, targetNow);
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

    private void AttackResponse(int netDmg, ComUn attacker, ComUn defender)
    {
		string message = "";
        if (netDmg > 0)
		{
			//report successful hit
			message = attacker.Name + " hit " + defender.Name + " for " + netDmg + " points of damage.";
			

		} else if ( netDmg == 0)
		{
            //report a block from armor
            message = attacker.Name + " hit " + defender.Name + " but could not pierce their target's armor!";
            
        } else if ( netDmg < 0)
		{
            //report as a miss
            //this is counted as a miss in ComUn.Attack()
            message = attacker.Name + " missed!";

        } else
		{
			message = "??action error.";
		}

        printAction(message);

    }

    public void ReadyArmies()
	{
		heroArmy.ReadyUpAll();
		mobArmy.ReadyUpAll();

	}

	private void InsertLog(ComUn atkUnit, ComUn defTarget, int dmg, double rollResult, bool hitSuccess)
	{

        //obsolete.
        //instead, logs are inserted by calling the limited constructer new ComLog() in the UnitAttackTarget() method
		//this is done exclusively at that method level because that method has the longest reach of visibility into hit detection and resulting damage

        //executed AFTER ComUn.Attack() processes
        //that way remaining HP = defTarget.Hp
        CombatLog newLog = new(CurrentRound,
								CurrentTurnOfRound,
								atkUnit.Name,
								atkUnit.Idr,
								atkUnit.Accuracy,
								hitSuccess,
								rollResult,
								defTarget.Name,
								defTarget.Idr,
								defTarget.Dodge,
								defTarget.Ac,
								dmg,
								defTarget.Hp);

	}

}
