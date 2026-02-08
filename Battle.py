# kb 2-7-2025
# simulator to experiment with Lychgate RPG combat scenarios
# battle class; instance of one fight

import ComUn
import Army

class Battle:
    def __init__(self, heroArmy, enemyArmy):
        self.heroArmy = heroArmy
        self.enemyArmy = enemyArmy
        pass
        
    def fightOneRound(self):
        #todo: carry out one round of combat
        
        
        
    def fightRounds(self, rdCount):
        #iterate over fight logic
        for i in range(rdCount):
            print ("Round",i+1)
            self.fightOneRound()
            