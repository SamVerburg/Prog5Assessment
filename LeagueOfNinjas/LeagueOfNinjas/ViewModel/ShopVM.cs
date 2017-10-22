using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LeagueOfNinjas.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup.Localizer;

namespace LeagueOfNinjas.ViewModel
{
    public class ShopVM : ViewModelBase
    {
        public ObservableCollection<ItemVM> ShopItems { get; set; }
        public ObservableCollection<ItemVM> TempShopItems { get; set; }

        public ItemVM SelectedItem { get; set; }

        public BuyItemCommand BuyItem { get; set; }

        public ICommand ShowAddItemCommand { get; set; }

        //Change tabs

        public ICommand ShowHeadCategory { get; set; }

        public ICommand ShowLegsCategory { get; set; }

        public ICommand ShowBeltCategory { get; set; }

        public ICommand ShowChestCategory { get; set; }

        public ICommand ShowBootsCategory { get; set; }

        public ICommand ShowShouldersCategory { get; set; }

        public NinjaVM SelectedNinja { get; set; }

        public ShopVM(NinjaVM selectedNinja)
        {
            this.SelectedNinja = selectedNinja;

            using (var context = new LeagueOfNinjasEntities())
            {
                var gear = context.Gear.ToList();

                ShopItems = new ObservableCollection<ItemVM>(gear.Select(g => new ItemVM(g)));
                TempShopItems = new ObservableCollection<ItemVM>();
            }

            ShowAddItemCommand = new RelayCommand(ShowAddItem);

            //Switch tabs
            ShowHeadCategory = new RelayCommand(RetrieveHeadItems);
            ShowLegsCategory = new RelayCommand(RetrieveLegsItems);
            ShowBeltCategory = new RelayCommand(RetrieveBeltItems);
            ShowChestCategory = new RelayCommand(RetrieveChestItems);
            ShowBootsCategory = new RelayCommand(RetrieveBootsItems);
            ShowShouldersCategory = new RelayCommand(RetrieveShouldersItems);

            BuyItem = new BuyItemCommand(ExecuteMethod, CanExecuteMethod);
        }

        private void ShowAddItem()
        {
            var window = new AddItemWindow();
            window.Show();

        }

        private bool CanExecuteMethod(object parameter)
        {
            if (SelectedItem != null)
            {
                foreach (var item in SelectedNinja.InventoryItems)
                {
                    if (item.Category == SelectedItem.Category)
                    {
                        return false;
                    }

                }
                if (SelectedNinja.Gold <= SelectedItem.Price)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        private void ExecuteMethod(object parameter)
        {
            using (var context = new LeagueOfNinjasEntities())
            {
                Ninja n = context.Ninja.Find(SelectedNinja.ToModel().Id);
                Gear g = context.Gear.Find(SelectedItem.ToModel().Id);
                n.Gear.Add(g);
                n.Gold -= SelectedItem.Price;
                context.SaveChanges();
            }
            SelectedNinja.Gold -= SelectedItem.Price;
            SelectedNinja.InventoryItems.Add(SelectedItem);
            SelectedNinja.Agility += SelectedItem.Agility;
            SelectedNinja.Intelligence += SelectedItem.Intelligence;
            SelectedNinja.Strength += SelectedItem.Strength;




        }

        private void RetrieveCategoryItems(String categoryName)
        {
            if (TempShopItems.Count > 0)
            {
                TempShopItems.Clear();

            }
            foreach (var item in ShopItems)
            {
                if (item.Category.ToLower().Equals(categoryName.ToLower()))
                {
                    TempShopItems.Add(item);
                }
            }

        }

        private void RetrieveHeadItems()
        {
            this.RetrieveCategoryItems("Head");
        }

        private void RetrieveShouldersItems()
        {
            this.RetrieveCategoryItems("Shoulders");
        }

        private void RetrieveChestItems()
        {
            this.RetrieveCategoryItems("Chest");
        }

        private void RetrieveBeltItems()
        {
            this.RetrieveCategoryItems("Belt");
        }

        private void RetrieveLegsItems()
        {
            this.RetrieveCategoryItems("Legs");
        }

        private void RetrieveBootsItems()
        {
            this.RetrieveCategoryItems("Boots");


        }
    }
}