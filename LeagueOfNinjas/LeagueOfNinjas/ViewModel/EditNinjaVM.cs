using GalaSoft.MvvmLight;
using System;
using System.Data.Entity;

namespace LeagueOfNinjas.ViewModel
{
    public class EditNinjaVM : ViewModelBase
    {
        public NinjaVM SelectedNinja { get; set; }

        public GenericCommand UpdateCommand { get; set; }

        public string NewName { get; set; }

        public string NewGold { get; set; }

        public EditNinjaVM(NinjaVM selectedNinja)
        {
            NewName = selectedNinja.Name;
            NewGold = selectedNinja.Gold.ToString();
            SelectedNinja = selectedNinja;
            UpdateCommand = new GenericCommand(Edit, CanEdit);
        }

        private bool isInteger(string value)
        {
            int temp;
            if (int.TryParse(value, out temp))
            {
                return true;
            }
            return false;
        }

        private bool CanEdit(object parameter)
        {
            return SelectedNinja != null
                && !String.IsNullOrEmpty(NewName)
                && isInteger(NewGold);
        }

        private void Edit(object parameter)
        {
            SelectedNinja.Name = NewName;
            SelectedNinja.Gold = Convert.ToInt32(NewGold);
            using (var context = new LeagueOfNinjasEntities())
            {
                context.Entry(SelectedNinja.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}

