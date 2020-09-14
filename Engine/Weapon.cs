using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Engine
{
    public class Weapon : Item
    {                                    
        public int MaximumDamage { get; set; }
        public int MinimumDamage { get; set; }

        public Weapon(int id, string name, string nameplural , int minimumdamage, int maximumdamage) : base(id, name, nameplural)
        {
            MaximumDamage = maximumdamage;
            MinimumDamage = minimumdamage;
        }

    }
}
