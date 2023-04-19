using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    internal class Enemy
    {
        private int Health;
        private int PhysicalAtack;
        private int MagicalAtack;
        private int PhysicalDefense;
        private int MagicalDefense;
        private string Name;
        private int Tier;

        public Enemy (string Name, int Health, int PhysicalAtack, int MagicalAtack, int PhysicalDefense, int MagicalDefense, int Tier)
        {
            this.Name = Name;
            this.Health = Health;
            this.PhysicalAtack = PhysicalAtack;
            this.MagicalAtack = MagicalAtack;
            this.PhysicalDefense = PhysicalDefense;
            this.MagicalDefense = MagicalDefense;
            this.Tier = Tier;
        }
        public string name
        {
            get { return Name; }
            set { Name = value; }
        }
        public int health
        {
            get { return Health; }
            set { Health = value; }
        }
        public int pAtack
        {
            get { return PhysicalAtack; }
            set { PhysicalAtack = value; }
        }
        public int mAtack
        { 
            get { return MagicalAtack; }
            set { MagicalAtack = value; }
        }
        public int pDefense
        {
            get { return PhysicalDefense; }
            set { PhysicalDefense = value; }
        }
        public int mDefense
        {
            get { return MagicalDefense; }
            set { MagicalDefense = value; }
        }
        public int tier
        {
            get { return Tier; }
            set { Tier = value; }
        }
    }
}
