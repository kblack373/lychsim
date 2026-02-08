# kb 2-7-2025
# simulator to experiment with Lychgate RPG combat scenarios
# combat unit class
import random

class comUn:
    def __init__(self, name, idr, hp, hitChance, dmg, ac, dodge, ini):
        self.name = name
        self.idr = int(idr)
        self.hp = int(hp)
        self.hitChance = float(hitChance)
        self.dmg = int(dmg)
        self.ac = int(ac)
        self.dodge = float(dodge)
        self.ini = int(ini)
        self.alive=1
        self.ready=1


#c1 = comUn("herp", 1, 25, 50.00, 7, 6, 1)
#print(c1.name)


#hp functions
    def setHp(self, inHp):
        self.hp = inHp
        return self.alive
        
    def subHp(self, inHp):
        self.hp -= inHp
        reportHp = self.hp
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
        
        #take a swing
        swing = random.randrange(1,100)
        
        #check if our swing is within our acc threshold
        if (swing <= acc):
            #that's a potential hit
            #now check against dodge
            if (not(inTarget.tryDodge(swing))):
                #that's a confirmed hit!
                #now apply damage
                netDmg = dmg - inTarget.ac
                inTarget.subHp(netDmg)
                print(self.name, "deals", netDmg, "to", inTarget.name)
            else:
                #report a miss
                print(self.name, "swings but", inTarget.name, " is too quick!")
        else:
            #report a miss
            print(self.name, "swings and misses.")
    
    def attackTarget(self, inTarget):
        if (inTarget==0):
            attack
    
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