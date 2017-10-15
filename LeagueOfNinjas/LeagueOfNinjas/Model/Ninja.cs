using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueOfNinjas.Model
{
    class Ninja
    {
        public string Name { get; set; }

        public int GoldAmount { get; set; }

        public Gear Gear { get; set; }


        public void ClearGear()
        {
            // Removes all the items from the list
        }


    }
}
