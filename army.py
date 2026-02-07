# kb 2-7-2025
# simulator to experiment with Lychgate RPG combat scenarios
# combat unit Army container class

import ComUn

class Army:
    def __init__(self, lsUnits):
        self.units = lsUnits
    
    def unitIndexer(n):
        return n.ini
    
    def sort(self):
        self.units.sort(key = unitIndexer)