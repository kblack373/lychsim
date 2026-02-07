# kb 2-7-2025
# simulator to experiment with Lychgate RPG combat scenarios
# engine class; contains game logic
import csv
import ComUn

class Engine:
    def __init__(self):
        #empty constructor... for now
        
        #init list for party of heroes
        self.party = []
        
        #init list for enemy horde
        self.horde = []
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
                    u = ComUn.Hero(row[1],row[2],row[3],row[4],row[5],row[6],row[7],row[8])
                    plParty.append(u)
                elif(team==0):
                    #unit is enemy
                    u = ComUn.Enemy(row[1],row[2],row[3],row[4],row[5],row[6],row[7],row[8])
                    plHorde.append(u)
                else:
                    #bad file logic
                    print(row)
                    raise TypeError("Something is wrong with this row.")
                # zero out placeholder object for next loop iteration
                u = None
        return (plParty,plHorde)
e = Engine()
result = e.parseConfig('enc.csv')
e.party = result[0]
e.horde = result[1]