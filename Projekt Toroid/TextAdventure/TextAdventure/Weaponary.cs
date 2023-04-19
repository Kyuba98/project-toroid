using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    internal class Weaponary
    {
        private string EQName = "empty";
        private int BonusPA = 0;
        private int BonusMA = 0;
        private int BonusPD = 0;
        private int BonusMD = 0;
        private string Type;
        private int Price;
        private int ID;

        public Weaponary(string EQName, int BonusPA, int BonusMA, int BonusPD, int BonusMD, string Type, int Price, int ID)
        {
            this.EQName = EQName;
            this.BonusPA = BonusPA;
            this.BonusMA = BonusMA;
            this.BonusPD = BonusPD;
            this.BonusMD = BonusMD;
            this.Type = Type;
            this.Price = Price;
            this.ID = ID;
        } 
        public string eqname
        {
            get { return EQName; }
        }
        public int bonusPA
        {
            get { return BonusPA; }
        }
        public int bonusMA
        {
            get { return BonusMA; }
        }
        public int bonusPD
        {
            get { return BonusPD; }
        }
        public int bonusMD
        {
            get { return BonusMD; }
        }
        public string type
        {
            get { return Type; }
        }
        public int price
        {
            get { return Price; }
        }
        public int id
        {
            get { return ID; }
        }
    }
}
