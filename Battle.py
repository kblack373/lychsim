# kb 2-7-2025
# simulator to experiment with Lychgate RPG combat scenarios
# battle class; instance of one fight

import comUn
import army
import time
import comLog

class Battle:
    def __init__(self, heroArmy, enemyArmy):
        self.heroArmy = heroArmy
        self.enemyArmy = enemyArmy
        
        # spin up a logger
        self.combatLog = comLog.CombatLogger("defaultOutput.csv")
        pass
        
        
    def logOneRound(self, logListOneRound):
        
        self.combatLog.insertLog(logListOneRound)
        return 1
        
    def logSave(self):
        print("called local method")
        self.combatLog.writeFile()
        return 1
        
    def fightOneRound(self, logRdCount):
        #todo: carry out one round of combat
        print("The fight begins...")
        #step 1: determine order
        #the two armies are self-sorting, so the highest init will always be at the top
        #toss-up
        heroes = self.heroArmy
        enemies = self.enemyArmy
        turnCount = heroes.getCount() + enemies.getCount()
        topHero = heroes.topOne()
        topEnemy = enemies.topOne()
        #init an empty list to insert a log for the current round of cmbat
        curRdLog = []
        #insert round count (ie round number) into first slot
        curRdLog.append(logRdCount)
        #track turns
        logTurnCount = 0
        while (topHero.idr != 0 or topEnemy.idr != 0):
            # insert and increment turn count
            curRdLog.append(logTurnCount)
            logTurnCount += 1
            #print(topHero.name, "vs", topEnemy.name)
            #compare the two inits; favor heroes
            if (topHero.ini >= topEnemy.ini):
                print(topHero.name)
                #hero gets to strike
                #0 = top of the pile
                #todo: add attack decision logic
                targetNow = enemies.getTopTarget(0)
                if (targetNow is None or targetNow.name=='null'):
                    print("No targets that", topHero.name,"can hit.")
                    topHero.exhaust()
                else:
                    print(topHero.name,"attacks",targetNow.name)
                    result = topHero.attack(targetNow)
                    logCharId = topHero.idr
                    logCurHp    = topHero.hp
                    logMaxHp = topHero.maxHp
                    # now get if hit was successful
                    logDmg      = result
                    logTargetId   = targetNow.idr
                    # write results to log list
                    turnResultBus = [logCharId, logCurHp, logMaxHp, logDmg, logTargetId]
                    for t in turnResultBus:
                        curRdLog.append(t)
                    topHero.exhaust()
            else:
                print(topEnemy.name)
                #enemy strikes first
                targetNow = heroes.getTopTarget(0)
                result = topEnemy.attack(targetNow)
                logCharId = topEnemy.idr
                logCurHp    = topEnemy.hp
                logMaxHp = topEnemy.maxHp
                # now get if hit was successful
                logDmg      = result
                logTargetId   = targetNow.idr
                # write results to log list
                turnResultBus = [logCharId, logCurHp, logMaxHp, logDmg, logTargetId]
                for t in turnResultBus:
                    curRdLog.append(t)
                topEnemy.exhaust()
            
            #get top of the init for each army
            topHero = heroes.topOne()
            topEnemy = enemies.topOne()
            
            #time.sleep(1)
            #insert full log payload list into the combat log
            self.combatLog.insertLog(curRdLog)
        
        #wrap the armies back up
        self.heroArmy = heroes
        self.enemyArmy = enemies
        print("The fight concludes.")
        return (self.heroArmy, self.enemyArmy)
    
    def endBattle(self):
        # save log to csv
        print("Battle finished, logging...")
        self.logSave()
    
    def fightRounds(self, rdCount):
        #iterate over fight logic
        for i in range(rdCount):
            print (">>> Round",i+1)
            self.fightOneRound(i)
            self.enemyArmy.report()
            self.heroArmy.report()
            if (self.enemyArmy.getTopTarget(0).name == 'null'):
                print("All enemies successfully defeated. Heroes win!")
                self.endBattle()
                return 0
            elif (self.heroArmy.getTopTarget(0).name == 'null'):
                print("All heroes have been slaughtered in combat. Defeat...")
                self.endBattle()
                return 0
            self.enemyArmy.readyUpAll()
            self.heroArmy.readyUpAll()
        
        