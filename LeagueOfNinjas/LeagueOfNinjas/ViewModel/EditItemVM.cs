using LeagueOfNinjas.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueOfNinjas.ViewModel
{
    public class EditItemVM
    {
        public ItemVM SelectedItem { get; set; }

        public EditItemCommand EditItem { get; set; }

        public EditItemVM(ItemVM selectedItem)
        {
            this.SelectedItem = selectedItem;
            EditItem = new EditItemCommand(ExecuteMethod, CanExecuteMethod);
        }

        public bool CanExecuteMethod(object parameter)
        {
            if (SelectedItem != null)
            {
                return true;
            }
            return false;
        }

        public void ExecuteMethod(object parameter)
        {
            using (var context = new LeagueOfNinjasEntities())
            {
                context.Entry(SelectedItem.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
