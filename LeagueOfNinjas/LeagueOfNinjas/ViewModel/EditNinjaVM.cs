using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LeagueOfNinjas.ViewModel
{
   public class EditNinjaVM : ViewModelBase
    {

        public NinjaVM SelectedNinja { get; set; }

        public ICommand UpdateCommand { get; set; }

        private NinjaListVM _ninjaList;


        public EditNinjaVM(NinjaVM selectedNinja, NinjaListVM ninjalist)
        {
            _ninjaList = ninjalist;
            SelectedNinja = selectedNinja;
            UpdateCommand = new RelayCommand<EditNinjaWindow>(Update);
        }

        private void Update(EditNinjaWindow obj)
        {
            using (var context = new LeagueOfNinjasEntities())
            {
                context.Entry(SelectedNinja.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
            obj.Hide();
        }

       
    }
}

