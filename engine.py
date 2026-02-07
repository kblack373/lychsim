# kb 2-7-2025
# simulator to experiment with Lychgate RPG combat scenarios
# engine class; contains game logic
import csv
import ComUn

class Engine:
    def __init__(self):
        #empty constructor... for now
        pass
    
    def parseConfig(self, inFileName):
        try:
            # attempt to open file
            with open(inFileName, newline='') as csvFile:
                rd = csv.reader(csvFile, delimiter=' ', quotechar='|')
                for row in rd:
                    #sample output
                    print(', '.join(row))
        except:
            print ("file error")

e = Engine()
e.parseConfig('enc.csv')