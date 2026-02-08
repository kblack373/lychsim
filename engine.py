# kb 2-7-2025
# simulator to experiment with Lychgate RPG combat scenarios
# engine class; contains game logic
import csv
import army
import comUn
import battle

class Engine:
    def __init__(self):        
        #init empty
        self.party = None
        self.horde = None
        self.battle = None
        pass
    
    def parseConfig(self, inFileName):
        # define payloads
        plParty = []
        plHorde = []
        # attempt to open file
        with open(inFileName, newline='') as csvFile:
            rd = csv.reader(csvFile, delimiter=' ', quotechar='|')
            u = None
            for row in rd:
                #check and assign team
                team = int(row[0])
                if(team==1):
                    #unit is hero, make Hero object
                    u = comUn.Hero(row[1],row[2],row[3],row[4],row[5],row[6],row[7],row[8])
                    plParty.append(u)
                elif(team==0):
                    #unit is enemy
                    u = comUn.Enemy(row[1],row[2],row[3],row[4],row[5],row[6],row[7],row[8])
                    plHorde.append(u)
                else:
                    #bad file logic
                    print(row)
                    raise TypeError("Something is wrong with this row.")
                # zero out placeholder object for next loop iteration
                u = None
        return (plParty,plHorde)
e = Engine()
print("> Reading csv configuration.... ")
result = e.parseConfig('enc.csv')
print("> Complete! ")
print("> Initializing Armies.... ")
e.party = army.Army(result[0])
e.horde = army.Army(result[1])
print("> Complete! ")
print("Reporting Heroes...")
e.party.report()
print("Reporting Enemies...")
e.horde.report()

#e.party.units[0].attack(e.horde.units[0])
e.battle = battle.Battle(e.party,e.horde)
e.battle.fightOneRound()