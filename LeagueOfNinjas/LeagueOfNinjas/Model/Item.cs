using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace LeagueOfNinjas.Model
{
    public class Item
    {
        public string Name { get; set; }

        public int GoldCost { get; set; }

        public Category Category { get; set; }
    }
}
