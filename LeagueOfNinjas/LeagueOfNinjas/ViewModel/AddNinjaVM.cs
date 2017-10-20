using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LeagueOfNinjas.ViewModel
{
   public class AddNinjaVM

    {
        public NinjaVM Ninja { get; set; }

        public ICommand AddCommand { get; set; }

        private NinjaListVM _ninjas;

        public AddNinjaVM(NinjaListVM ninjas)
        {
            _ninjas = ninjas;
            Ninja = new NinjaVM();
            AddCommand = new RelayCommand<AddNinjaWindow>(Save);
        }

        private void Save(AddNinjaWindow obj)
        {
            using (var context = new LeagueOfNinjasEntities())
            {
                context.SaveChanges();
            }

            _ninjas.Ninjas.Add(Ninja);
            obj.Hide();
        }
    }
}
