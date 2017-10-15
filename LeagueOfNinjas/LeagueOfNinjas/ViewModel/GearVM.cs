using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using LeagueOfNinjas.Model;

namespace LeagueOfNinjas.ViewModel
{
    public class GearVM : ViewModelBase
    {
        public ObservableCollection<Item> Items { get; set; }

        public GearVM()
        {
            Items = new ObservableCollection<Item>();
            Items.Add(new Item() {Category = Category.Belt, GoldCost = 200, Name = "Leather Belt"});
        }
    }
}
