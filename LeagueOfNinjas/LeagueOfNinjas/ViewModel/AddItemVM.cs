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

        public string Name { get; set; }
        public string Price { get; set; }
        public string Intelligence { get; set; }
        public string Agility { get; set; }
        public string Strength { get; set; }
        public string SelectedCategory { get; set; }

        public ObservableCollection<string> Categories { get; set; }
            = new ObservableCollection<string>() { "Chest", "Shoulders", "Belt", "Head", "Boots", "Legs", };

        private ShopVM _shopVM;

        public AddItemVM(ShopVM shopvm)
        {
            AddCommand = new GenericCommand(Add, CanAdd);
            _shopVM = shopvm;
        }

        private bool CanAdd(object parameter)
        {
            return !string.IsNullOrEmpty(Name)
                && !string.IsNullOrEmpty(SelectedCategory)
                && isInteger(Intelligence)
                && isInteger(Agility)
                && isInteger(Strength)
                && isInteger(Price);
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

        private void Add(object parameter)
        {
            ItemVM Item = new ItemVM();
            Item.Name = Name;
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

            SelectedCategory = Item.Category;
            Agility = Item.Agility.ToString();
            Intelligence = Item.Intelligence.ToString();
            Strength = Item.Strength.ToString();
            Price = Item.Price.ToString();
            Name = Item.Name;
        }
    }
}