using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace LeagueOfNinjas.ViewModel
{
    public class AddItemVM
    {
        public GenericCommand AddCommand { get; set; }

        public ItemVM Item { get; set; } = new ItemVM();
        public string Price { get; set; }
        public string Intelligence { get; set; }
        public string Agility { get; set; }
        public string Strength { get; set; }

        public ObservableCollection<string> Categories { get; set; }
            = new ObservableCollection<string>() { "Chest", "Shoulders", "Belt", "Head", "Boots", "Legs", };

        public string SelectedCategory { get; set; }

        private ShopVM _shopVM;

        public AddItemVM(ShopVM shopvm)
        {
            AddCommand = new GenericCommand(Add, CanAdd);
            _shopVM = shopvm;
        }

        private bool CanAdd(object parameter)
        {
            return !string.IsNullOrEmpty(Item.Name)
                && !string.IsNullOrEmpty(SelectedCategory)
                && isInteger(Intelligence)
                && isInteger(Agility)
                && isInteger(Strength)
                && isInteger(Price);
        }

        private bool isInteger(String value)
        {
            int temp = 0;
            if (int.TryParse(value, out temp))
            {
                return true;
            }
            return false;
        }

        private void Add(object parameter)
        {
            Item.Price = Convert.ToInt32(Price);
            Item.Category = SelectedCategory;
            Item.Intelligence = Convert.ToInt32(Intelligence);
            Item.Agility = Convert.ToInt32(Agility);
            Item.Strength = Convert.ToInt32(Strength);

            using (var context = new LeagueOfNinjasEntities())
            {
                context.Gears.Add(Item.ToModel());
                context.SaveChanges();
            }

            _shopVM.ShopItems.Add(Item);
            _shopVM.TempShopItems.Add(Item);
            _shopVM.RetrieveCategoryItems(SelectedCategory);

            Item = new ItemVM
            {
                Category = Item.Category,
                Price = Item.Price,
                Agility = Item.Agility,
                Intelligence = Item.Intelligence,
                Strength = Item.Strength,
                Name = Item.Name
            };
        }
    }
}