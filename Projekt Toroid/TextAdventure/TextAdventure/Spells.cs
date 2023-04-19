using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    internal class Spells
    {
        private string SpellName;
        private string ShortName;
        private int EfectID;
        private int PowerMultiplayer;
        private int ManaCost;
        private int ReqLvl;
        private int ReqEQ;
        private string Description;

        public Spells(string spellName,string shortname, int efectID, int powerMultiplayer, int manaCost, int reqLvl, int reqEQ, string description)
        {
            this.SpellName = spellName;
            this.ShortName = shortname;
            this.EfectID = efectID;
            this.PowerMultiplayer = powerMultiplayer;
            this.ManaCost = manaCost;
            this.ReqLvl = reqLvl;
            this.ReqEQ = reqEQ;
            this.Description = description;
        }
        public string spellName
        {
            get { return SpellName; }
        }
        public string shortName
        {
            get { return ShortName; }
        }
        public int efectID
        {
            get { return EfectID; }
        }
        public int powerMultiplayer
        {
            get { return PowerMultiplayer; }
        }
        public int manaCost
        {
            get { return ManaCost; }
        }       
        public int reqLvl
        {
            get { return ReqLvl; }
        }
        public int reqEQ
        {
            get { return ReqEQ; }
        }
        public string description
        {
            get { return Description; }
        }
    }
}
