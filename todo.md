# To-Do
-[x] Implement logging to run deep analysis
-[X]  Enhance the combat system to reflect ideal Implementation


## Tasks

### C# Conversion 
- N Black is handling battle and ComUn classes
- K Black is handling Engine and Army classes
- **End Goal**: Re-implement the current stable python build in C# as a console application. 
- Will merge into `master` branch once complete



### Combat Flow
#### When any unit attacks:
1. roll d100
2. add *the unit's* **accuraccy property**
3. if this sum is greater than the *target's* **dodge property**: it is a HIT.
4. otherwise, it is a MISS.
5. if it is a HIT, apply the *unit's* DMG minus the *target's* AC.

