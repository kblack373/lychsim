# kb + nolan 2 13 2025
# lychgate logging for big data analysis
import csv

class CombatLogger:
    
    def __init__(self, logFileName):
        # initialize the logger
        self.logFileName = logFileName
        self.combatLogArr = []
        pass
        
        
    def insertLog(self, listFeats):
        print("logged: ", listFeats)
        self.combatLogArr.append(listFeats)
        return 1
        
        
    def writeFile(self):
        #print("called inner method")
        #local vars
        fname = self.logFileName 
        output = self.combatLogArr
        #print(output)
        
        with open(fname, 'w', newline='') as csvFile:
           print ("Writing to ", fname, "...")
           csvWrit = csv.writer(csvFile)
           csvWrit.writerows(output)
        print ("done.")
        return 1