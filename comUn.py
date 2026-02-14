# kb 2-7-2025
# simulator to experiment with Lychgate RPG combat scenarios
# combat unit class
import random

class comUn:
    def __init__(self, name, idr, hp, hitChance, dmg, ac, dodge, ini):
        self.name = name
        self.idr = int(idr)
        self.hp = int(hp)
        self.maxHp = self.hp
        self.hitChance = float(hitChance)
        self.dmg = int(dmg)
        self.ac = int(ac)
        self.dodge = float(dodge)
        self.ini = int(ini)
        self.alive=1
        self.ready=1


#hp functions
    def checkDeath(self):
        if(self.hp<=0):
            print(self.name, "has fallen to their wounds.")
            self.alive=0
            self.ready=0
            return 1
        else:
            return 0
    def setHp(self, inHp):
        self.hp = inHp
        return self.alive
        
    def subHp(self, inHp):
        self.hp -= inHp
        reportHp = self.hp
        self.checkDeath()
        return reportHp
        
    def addHp(self, inHp):
        self.hp += inHp
        reportHp = self.hp
        return reportHp
    
    # combat logic
    def tryDodge(self, inShot):
        #react formula: super basic
        react=self.dodge
        
        if (inShot>react):
            # the dodge fails
            return 0
        else:
            # the dodge succeeds
            return 1
        
        
    def attack(self, inTarget):
        #first we need to hit
        #grab our accuracy and damage
        acc = self.hitChance
        dmg = self.dmg
        
        # TODO : imeplement new hit/dodge algorithm
        
        
        #roll a D100 to swing at the target
        roll = random.randrange(1,100)
        # add the roll value to our base accuracy
        netAcc = acc + roll
        thresh = inTarget.dodge
        #
        if (netAcc > thresh):
            netDmg = dmg - inTarget.ac
            inTarget.subHp(netDmg)
            print(self.name, "deals", netDmg, "to", inTarget.name)
            
            #return 1 true for successful hit
            return netDmg
       
        else:
            #report a miss
            print(self.name, "swings but", inTarget.name, " is too quick!")
            #return 0 false for miss
            return 0
    
    
    def exhaust(self):
        self.ready=0
        return 1
        
class Hero(comUn):
    def __init__(self, name, idr, hp, hitChance, dmg, ac, dodge, ini): 
        super().__init__(name, idr, hp, hitChance, dmg, ac, dodge, ini)
        #distinguish player
        self.side="player"
        
        
class Enemy(comUn):
    def __init__(self, name, idr, hp, hitChance, dmg, ac, dodge, ini): 
        super().__init__(name, idr, hp, hitChance, dmg, ac, dodge, ini)
        #distinguish enemy
        self.side="enemy"