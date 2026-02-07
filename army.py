# kb 2-7-2025
# simulator to experiment with Lychgate RPG combat scenarios
# combat unit Army container class

import ComUn

class Army:
    def __init__(self, lsUnits):
        self.units = lsUnits
        self.sortSelf()
    
    def unitIndexer(self, n):
        return n.ini
    
    def sortSelf(self):
        units = self.units
        units.sort(key = self.unitIndexer)
        self.units = units
        return units 
        
    
    def add(self, inUnit):
        units = self.units
        units.append(inUnit)
        self.units = units
        self.sortSelf()
        pl = len(units)
        return pl
        
    def report(self):
        units = self.units
        for u in units:
            print("------------")
            print(u.name)
            print("------------")
            print("Hit Points: ", u.hp)
            if (u.alive):
                print("Status: Alive")
            else:
                print("Status: Deceased")
            print("Net Accuracy: ", u.hitChance)
            print("Damage: ", u.dmg)
            print("Armor: ", u.ac)
            print("Net Evasion: ", u.dodge)
            print("Initiative: ", u.ini)
            print("")
            