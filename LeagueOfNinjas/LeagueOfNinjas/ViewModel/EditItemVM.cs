using LeagueOfNinjas.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace LeagueOfNinjas.ViewModel
{
    public class EditItemVM
    {
      public  ICommand EditItemCommand { get; set; }

        public ObservableCollection<string> Categories { get; set; }
            = new ObservableCollection<string>() { "Chest", "Shoulders", "Belt", "Head", "Boots", "Legs", };

        
        public ShopVM Shop { get; set; }

        private NinjaVM _selectedNinja;

        //Updated Item Info

        public string UpdatedCategory { get; set; }
        public string UpdatedName { get; set; }
        public int UpdatedPrice { get; set; }
        public int UpdatedStrength { get; set; }
        public int UpdatedIntelligence { get; set; }
        public int UpdatedAgility { get; set; }
        
        public EditItemVM(NinjaVM selectedNinja, ShopVM shop)
        {
            UpdatedAgility = shop.SelectedItem.Agility;
            UpdatedName = shop.SelectedItem.Name;
            UpdatedPrice = shop.SelectedItem.Price;
            UpdatedStrength = shop.SelectedItem.Strength;
            UpdatedIntelligence = shop.SelectedItem.Intelligence;
            UpdatedAgility = shop.SelectedItem.Agility;

            this.Shop = shop;
            _selectedNinja = selectedNinja;
            UpdatedCategory = shop.SelectedItem.Category;
            EditItemCommand = new RelayCommand(Edit);
        }

        public void Edit()
        {
            ItemVM foundItem = null;

            int index = 0;
            foreach (ItemVM i in _selectedNinja.InventoryItems)
            {
                if (i.ToModel().Id == Shop.SelectedItem.ToModel().Id)
                {
                    _selectedNinja.InventoryItems[index].Name = UpdatedName;
                    _selectedNinja.InventoryItems[index].Category = UpdatedCategory;
                    _selectedNinja.InventoryItems[index].Price = UpdatedPrice;
                    _selectedNinja.InventoryItems[index].Strength = UpdatedStrength;
                    _selectedNinja.InventoryItems[index].Intelligence = UpdatedIntelligence;
                    _selectedNinja.InventoryItems[index].Agility = UpdatedAgility;
                    _selectedNinja.UpdateStats();
                    break;
                }
                index++;
            }

            string oldCategory = Shop.SelectedItem.Category;
            Shop.SelectedItem.Category = UpdatedCategory;

            index = 0;
            foreach (ItemVM i in Shop.ShopItems)
            {
                if (i.ToModel().Id == Shop.SelectedItem.ToModel().Id)
                {
                    Shop.ShopItems[index].Name = UpdatedName;
                    Shop.ShopItems[index].Category = UpdatedCategory;
                    Shop.ShopItems[index].Price = UpdatedPrice;
                    Shop.ShopItems[index].Strength = UpdatedStrength;
                    Shop.ShopItems[index].Intelligence = UpdatedIntelligence;
                    Shop.ShopItems[index].Agility = UpdatedAgility;
                    break;
                }
                index++;
            }
            
            using (var context = new LeagueOfNinjasEntities())
            {
                context.Entry(Shop.SelectedItem.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }

            Shop.RetrieveCategoryItems(oldCategory);
        }
    }
}
