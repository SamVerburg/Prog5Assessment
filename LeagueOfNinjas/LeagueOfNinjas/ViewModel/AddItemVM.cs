using LeagueOfNinjas.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueOfNinjas.ViewModel
{
    public class AddItemVM
    {
        public AddItemCommand AddCommand { get; set; }

        public ItemVM Item { get; set; }

        private ShopVM _shopVM;

        public AddItemVM(ShopVM shopvm)
        {
            AddCommand = new AddItemCommand(ExecuteMethod, CanExecuteMethod);
            _shopVM = shopvm;
            Item = new ItemVM();
        }

        private bool CanExecuteMethod(object parameter)
        {
            //Checks whether user can add item
            if (!String.IsNullOrEmpty(Item.Name) && !String.IsNullOrEmpty(Item.Name))
            {
                return true;
            }
            return false;
        }

        private void ExecuteMethod(object parameter)
        {
            using (var context = new LeagueOfNinjasEntities())
            {
                context.Gear.Add(Item.ToModel());
                context.SaveChanges();
            }

            _shopVM.ShopItems.Add(Item);
            _shopVM.TempShopItems.Add(Item);
        }
    }
}