
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows.Input;

namespace LeagueOfNinjas.ViewModel
{
    public class EditItemVM
    {
        public ICommand EditItemCommand { get; set; }

        public ObservableCollection<string> Categories { get; set; }
            = new ObservableCollection<string>() { "Chest", "Shoulders", "Belt", "Head", "Boots", "Legs", };

        public ShopVM Shop { get; set; }

        private NinjaVM _selectedNinja;

        //Updated Item Info

        public string UpdatedCategory { get; set; }
        public string UpdatedName { get; set; }
        public string UpdatedPrice { get; set; }
        public string UpdatedStrength { get; set; }
        public string UpdatedIntelligence { get; set; }
        public string UpdatedAgility { get; set; }

        public EditItemVM(NinjaVM selectedNinja, ShopVM shop)
        {
            UpdatedAgility = shop.SelectedItem.Agility.ToString();
            UpdatedName = shop.SelectedItem.Name;
            UpdatedPrice = shop.SelectedItem.Price.ToString();
            UpdatedStrength = shop.SelectedItem.Strength.ToString();
            UpdatedIntelligence = shop.SelectedItem.Intelligence.ToString();
            UpdatedAgility = shop.SelectedItem.Agility.ToString();

            this.Shop = shop;
            _selectedNinja = selectedNinja;
            UpdatedCategory = shop.SelectedItem.Category;
            EditItemCommand = new RelayCommand(Edit, CanEdit);
        }

        private bool CanEdit()
        {
            return !string.IsNullOrEmpty(UpdatedName)
                   && !string.IsNullOrEmpty(UpdatedCategory)
                   && isInteger(UpdatedIntelligence)
                   && isInteger(UpdatedAgility)
                   && isInteger(UpdatedStrength)
                   && isInteger(UpdatedPrice);
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

        private void Edit()
        {
            Shop.SelectedItem.Name = UpdatedName;
            Shop.SelectedItem.Category = UpdatedCategory;
            Shop.SelectedItem.Price = Convert.ToInt32(UpdatedPrice);
            Shop.SelectedItem.Agility = Convert.ToInt32(UpdatedAgility);
            Shop.SelectedItem.Intelligence = Convert.ToInt32(UpdatedIntelligence);
            Shop.SelectedItem.Strength = Convert.ToInt32(UpdatedStrength);

            using (var context = new LeagueOfNinjasEntities())
            {
                context.Entry(Shop.SelectedItem.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }

            _selectedNinja.UpdateItem(Shop.SelectedItem);
            Shop.EditItem(UpdatedCategory);
        }
    }
}
