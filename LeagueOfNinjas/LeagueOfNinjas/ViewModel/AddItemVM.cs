using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueOfNinjas.ViewModel
{
    public class AddItemVM
    {
        public GenericCommand AddCommand { get; set; }

        public ItemVM Item { get; set; } =  new ItemVM();

        public ObservableCollection<string> Categories { get; set; } 
            = new ObservableCollection<string>() {"Chest", "Shoulders", "Belt", "Head", "Boots", "Legs", };

        public string SelectedCategory { get; set; }

        private ShopVM _shopVM;

        public AddItemVM(ShopVM shopvm)
        {
            AddCommand = new GenericCommand(ExecuteMethod, CanExecuteMethod);
            _shopVM = shopvm;
        }

        private bool CanExecuteMethod(object parameter)
        {
            //Checks whether user can add item
            if (!String.IsNullOrEmpty(Item.Name) && !String.IsNullOrEmpty(SelectedCategory))
            {
                return true;
            }
            return false;
        }

        private void ExecuteMethod(object parameter)
        {
            Item.Category = SelectedCategory;
            using (var context = new LeagueOfNinjasEntities())
            {
                context.Gears.Add(Item.ToModel());
                context.SaveChanges();
            }

            _shopVM.ShopItems.Add(Item);
            _shopVM.TempShopItems.Add(Item);
            _shopVM.RetrieveCategoryItems(SelectedCategory);
        }
    }
}