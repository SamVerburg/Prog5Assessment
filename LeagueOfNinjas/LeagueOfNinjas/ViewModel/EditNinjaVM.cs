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
   public class EditNinjaVM : ViewModelBase
    {
        public ICommand EditCommand { get; set; }

        private NinjaListVM _ninjas;

        private NinjaVM _selectedNinja;



        public EditNinjaVM(NinjaVM selectedNinja)
        {
            _selectedNinja = selectedNinja;
            EditCommand = new RelayCommand<EditNinjaWindow>(Edit);
        }

        private void Edit(EditNinjaWindow obj)
        {
            using (var context = new LeagueOfNinjasEntities())
            {
                context.SaveChanges();
            }

            obj.Hide();
        }
    }
}

