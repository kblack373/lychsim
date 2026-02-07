# kb 2-7-2025
# simulator to experiment with Lychgate RPG combat scenarios
# combat unit class

class ComUn:
    def __init__(self, name, idr, hp, hitChance, dmg, ac, dodge, ini):
        self.name = name
        self.idr = idr
        self.hp = hp 
        self.hitChance = hitChance
        self.dmg = dmg
        self.ac = ac
        self.dodge = dodge
        self.ini = ini
        self.alive=1


#c1 = ComUn("herp", 1, 25, 50.00, 7, 6, 1)
#print(c1.name)


#hp functions
    def setHp(self, inHp):
        self.hp = inHp
        return self.alive
        
    def subHp(self, inHp):
        self.hp -= inHp
        reportHp = self.hp
        return report
        
    def addHp(self, inHp):
        self.hp += inHp
        reportHp = self.hp
        return reportHp
    
    
class Hero(ComUn):
    def __init__(self, name, idr, hp, hitChance, dmg, ac, dodge, ini): 
        super().__init__(name, idr, hp, hitChance, dmg, ac, dodge, ini)
        #distinguish player
        self.side="player"
        
        
class Enemy(ComUn):
    def __init__(self, name, idr, hp, hitChance, dmg, ac, dodge, ini): 
        super().__init__(name, idr, hp, hitChance, dmg, ac, dodge, ini)
        #distinguish enemy
        self.side="enemy"