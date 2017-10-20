using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace LeagueOfNinjas.ViewModel
{
    public class NinjaListVM : ViewModelBase
    {
        public ObservableCollection<NinjaVM> Ninjas { get; set; }

        public NinjaListVM()
        {
            Ninjas = new ObservableCollection<NinjaVM>();
            Ninjas.Add(new NinjaVM() { Naam = "Nina"});
            Ninjas.Add(new NinjaVM() { Naam = "Sam" });
        }
    }
}
