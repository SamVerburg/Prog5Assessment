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
            using (var context = new LeagueOfNinjasEntities())
            {
                var ninjas = context.Ninja.ToList();
                Ninjas = new ObservableCollection<NinjaVM>(ninjas.Select(n => new NinjaVM(n)));
            }
        }
    }
}
