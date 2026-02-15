# LychSim

A simulation program intended to experiment with RPG Party configurations. Runs multiple battles. 



# Quickstart guide.

This package requires Python 3. Go install Python3 here.

To configure a party, edit the `enc.csv` file. The data headers can be found in the `unit.txt` file and at the end of this file.

To execute an instance of the simulation, run this command in the application's root directory:

```
python3 .\engine.py
```

The application will then simulate 50 rounds of combat. 


# Data Headers

The combat units in the simulation are objects with the following positional parameters:

| Parameter      | Description |  Datatype
| ----------- | ----------- |  ----------- |
| class      | Whether the unit is hero or enemy       | bit |
| name   | display name of the unit        | string | 
| idr   | unique id for the unit        | int | 
| hp   | starting health points       | int | 
| hitChance   | chance to hit a target, accuracy      | float | 
| dmg   | damage dealt      | int | 
| ac   | armor class, static damage reduction     | int | 
| dodge   | chance for a target to dodge, depending on the roll      | float |
| ini   | initiative, speed. higher values will allow a unit to take their turn earlier units with lower initative     | int | 