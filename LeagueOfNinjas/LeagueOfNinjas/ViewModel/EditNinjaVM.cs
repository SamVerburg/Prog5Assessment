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

        public GenericCommand UpdateCommand { get; set; }

        public string NewName { get; set; }

        public int NewGold { get; set; }

        public EditNinjaVM(NinjaVM selectedNinja)
        {
            NewName = selectedNinja.Name;
            NewGold = selectedNinja.Gold;
            SelectedNinja = selectedNinja;
            UpdateCommand = new GenericCommand(Edit, CanEdit);
        }

        private bool CanEdit(object parameter)
        {
            //Checks whether user can edit ninja
            if (SelectedNinja != null)
            {
                if (!String.IsNullOrEmpty(NewName) && NewGold > 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void Edit(object parameter)
        {
            SelectedNinja.Name = NewName;
            SelectedNinja.Gold = NewGold;
            using (var context = new LeagueOfNinjasEntities())
            {
                context.Entry(SelectedNinja.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }

        }
    }
}

