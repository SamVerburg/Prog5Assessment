using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LeagueOfNinjas.ViewModel
{
    public class AddNinjaVM : ViewModelBase
    {
        public NinjaVM Ninja { get; set; }

        public GenericCommand AddCommand { get; set; }

        private NinjaListVM _ninjas;

        public AddNinjaVM(NinjaListVM ninjas)
        {
            _ninjas = ninjas;
            Ninja = new NinjaVM();
            AddCommand = new GenericCommand(Add, CanAdd);
        }

        private bool CanAdd(object parameter)
        {
            //Checks whether user can add ninja
            if (!String.IsNullOrEmpty(Ninja.Name) && Ninja.Gold > 0)
            {
                return true;
            }
            return false;
        }

        private void Add(object parameter)
        {
            using (var context = new LeagueOfNinjasEntities())
            {
                context.Ninjas.Add(Ninja.ToModel());
                context.SaveChanges();
            }

            _ninjas.Ninjas.Add(Ninja);
        }
    }
}
