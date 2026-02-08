# kb 2-7-2025
# simulator to experiment with Lychgate RPG combat scenarios
# battle class; instance of one fight

import comUn
import army
import time

class Battle:
    def __init__(self, heroArmy, enemyArmy):
        self.heroArmy = heroArmy
        self.enemyArmy = enemyArmy
        pass
        
        
    
    def fightOneRound(self):
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
        while (topHero.idr != 0 or topEnemy.idr != 0):
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
                    topHero.attack(targetNow)
                    topHero.exhaust()
            else:
                print(topEnemy.name)
                #enemy strikes first
                topEnemy.attack(heroes.getTopTarget(0))
                topEnemy.exhaust()
            
            #get top of the init for each army
            topHero = heroes.topOne()
            topEnemy = enemies.topOne()
            time.sleep(1)
        
        #wrap the armies back up
        self.heroArmy = heroes
        self.enemyArmy = enemies
        print("The fight concludes.")
        return (self.heroArmy, self.enemyArmy)
        
    def fightRounds(self, rdCount):
        #iterate over fight logic
        for i in range(rdCount):
            print (">>> Round",i+1)
            self.fightOneRound()
            self.enemyArmy.report()
            self.heroArmy.report()
            if (self.enemyArmy.getTopTarget(0).name is 'null'):
                print("All enemies successfully defeated. Heroes win!")
                return 0
            elif (self.heroArmy.getTopTarget(0).name is 'null'):
                print("All heroes have been slaughtered in combat. Defeat...")
                return 0
            self.enemyArmy.readyUpAll()
            self.heroArmy.readyUpAll()