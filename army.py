# kb 2-7-2025
# simulator to experiment with Lychgate RPG combat scenarios
# combat unit Army container class

import comUn

class Army:
    def __init__(self, lsUnits):
        self.units = lsUnits
        self.sortSelf()
    
    def unitIndexer(self, n):
        #uses initiative (ini) as index
        return n.ini
    
    def sortSelf(self):
        units = self.units
        units.sort(key = self.unitIndexer)
        units.reverse()
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
            

    def top(self,i):
       ## print("method called.", i)
        #access and remove first element
        if (len(self.units)>=i+1):
            ##print("length correct.")
            top = self.units[i]
            if (top.ready):
                ##print("returning top of pack:", top.name)
                return top
            else:
                ##print("going deeper...")
                #otherwise recursive call to find the next in line who is ready
                # ready = no action this round
                return self.top(i+1)
        else: 
            #if there's noone left, return a blank unit with idr=0
            #this terminates the round if both armies have idr=0 as top
            return comUn.comUn('null',0,0,0,0,0,0,0)
    
    def topOne(self):
        return self.top(0)    
    def getCount(self):
        return len(self.units)
        
    def getTopTarget(self):
        return self.units[0]