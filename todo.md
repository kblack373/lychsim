# To-Do
-[x] Implement logging to run deep analysis
-[]  Enhance the combat system to reflect ideal Implementation


## Tasks

### Combat Flow
#### When any unit attacks:
1. roll d100
2. add *the unit's* **accuraccy property**
3. if this sum is greater than the *target's* **dodge property**: it is a HIT.
4. otherwise, it is a MISS.
5. if it is a HIT, apply the *unit's* DMG minus the *target's* AC.
