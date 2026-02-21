
using MathNet.Numerics;
using MathNet.Numerics.Distributions;

namespace Lychgate
{
    //faction enumeration
    public enum Faction
    {

        Heroes,
        Enemies

    }


    //base combat unit class
    public class ComUn
    {
        // instance variables
        public string Name;
        public int Idr;
        public int Hp;
        public int MaxHp;
        public double Accuracy;
        public int Dmg;
        public int Ac;
        public double Dodge;
        public int Ini;
        public bool Alive;
        public bool Ready;
        public Faction Alignment;
        public string strAlignTag = ""; //faction represented as string. needed to make the JSON parsing simpler.

        public ComUn(string name, int idr, int hp, double hitChance, int dmg, int ac, double dodge, int ini)
        {
            Name = name;
            Idr = idr;
            Hp = hp;
            MaxHp = hp;
            Accuracy = hitChance;
            Dmg = dmg;
            Ac = ac;
            Dodge = dodge;
            Ini = ini;
            Alive = true;
            Ready = true;
        }

        [Newtonsoft.Json.JsonConstructor]
        public ComUn(string name, int idr, int hp, double hitChance, int dmg, int ac, double dodge, int ini, string align)
        {
            //todo set alignment
            switch (align) {
                case "Hero":
                    this.Alignment = Faction.Heroes;
                    break;
                case "Mob":
                    this.Alignment = Faction.Enemies;
                    break;
                default:
                    throw new Exception("Invalid alignment value: " + align);
            
                }
            Name = name;
            Idr = idr;
            Hp = hp;
            MaxHp = hp;
            Accuracy = hitChance;
            Dmg = dmg;
            Ac = ac;
            Dodge = dodge;
            Ini = ini;
            Alive = true;
            Ready = true;
        }

        public static void CreateUnit()
        {   //make sure this level is working.
            Console.WriteLine("CreateUnit is working.");
        }
        
        public bool CheckDeath()
        {   //is the ceeature alive? TODO: Does this need to pass the creature ID?
            if (Hp <= 0)
            {
                
                Alive = false;
                Ready = false;
                return true;
            }
            else
            {
                return false;
            }
            //Console.WriteLine("CheckDeath is working.");
        }
        public bool SetHP(int inHP)
        {
            Hp = inHP;
            return Alive;
        }
        public int AddHP(int addHp)
        {
            Hp = Hp + addHp;
            return Hp;
        }
        public int SubHP(int minusHp)
        {
            Hp = Hp - minusHp;
            return Hp;
        }

        public void ReadyUp()
        {
            if (Alive)
            {
                this.Ready = true;
            }
        }

        //todo: implement Attack() method

        private bool TryHit(double acc, double targetDodge)
        {
            Random d100 = new();
            double roll = d100.NextDouble();

            roll += acc;

            if (roll > targetDodge)
            {
                return true;

            }
            else
            {
                return false;

            }

        }

        public int Attack(ComUn inTarget)
        {
            //roll D100
            bool hit = TryHit(this.Accuracy, inTarget.Dodge);
            if (hit)
            {   
                // Roll Damage 

                int netDmg = RollDmg() - inTarget.Ac;
                if (netDmg < 0)
                {
                    // don't return less than 0 if it doesnt get thru armor
                    return 0;
                }
                else
                {
                    // successful hit, roll damage
                    return netDmg;
                }
            }
            else
            {
                //unique error code if a total miss; net dmg can be zero.
                return -1;
            }

        }
        private int RollDmg()
        {
            double rolled_dmg = (int) Normal.Sample(this.Dmg, 2.0);

            if (rolled_dmg <= 0)
            {
                rolled_dmg = 0;
            }

            Console.WriteLine($"Damage Roll: {rolled_dmg}");
            return (int) rolled_dmg;

        }

        public void Exhaust()
        {
            //unit has taken its turn for the round.
            this.Ready = false;
        }
    }


    //todo: extend class to Hero and Enemy subclasses

    public class Hero : ComUn
    {

        public Hero(string name, int idr, int hp, double hitChance, int dmg, int ac, double dodge, int ini) : base(name, idr, hp, hitChance, dmg, ac, dodge, ini)
        {
            Alignment = Faction.Heroes;
        }
    }
    public class Mob : ComUn
    {

        public Mob(string name, int idr, int hp, double hitChance, int dmg, int ac, double dodge, int ini) : base(name, idr, hp, hitChance, dmg, ac, dodge, ini)
        {
            Alignment = Faction.Enemies;
        }
    }

}