using System;
using System.Runtime.InteropServices.Swift;
using System.Security.Principal;



namespace Lychgate
{
    //base combat unit class
    public class ComUn
    {
        public string Name;
        public int Idr;
        public int Hp;
        public int MaxHp;
        public double HitChance;
        public int Dmg;
        public int Ac;
        public double Dodge;
        public int Ini;
        public bool Alive;
        public bool Ready;
        public static void CreateUnit()
        {   //make sure this level is working.
            Console.WriteLine("CreateUnit is working.");
        }
        public ComUn(string name, int idr, int hp, double hitChance, int dmg, int ac, double dodge, int ini)
        {
            Name = name;
            Idr = idr;
            Hp = hp;
            MaxHp = hp;
            HitChance = hitChance;
            Dmg = dmg;
            Ac = ac;
            Dodge = dodge;
            Ini = ini;
            Alive = true;
            Ready = true;
        }
        public bool CheckDeath()
        {   //is the ceeature alive? TODO: Does this need to pass the creature ID?
            if (Hp <= 0)
            {
                Console.WriteLine(Name + " has fallen to their wounds.");
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

        private bool tryHit(double acc, double targetDodge)
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
            bool hit = tryHit(this.HitChance, inTarget.Dodge);
            if (hit)
            {
                int netDmg = this.Dmg = inTarget.Ac;
                return netDmg;
            } else
            {
                //unique error code if a total miss; net dmg can be zero.
                return -1;
            }

        }

        public void Exhaust()
        {
            //unit has taken its turn for the round.
            this.Ready = false;
        }
    }
}

//todo: extend class to Hero and Enemy subclasses


