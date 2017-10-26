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

        public NinjaListVM NinjaList { get; set; }

        public ItemVM SelectedItem { get; set; }

        #region Commands
        public GenericCommand BuyItemCommand { get; set; }

        public ICommand ShowAddItemCommand { get; set; }

        public GenericCommand DeleteItemCommand { get; set; }
        public ICommand ShowHeadCategory { get; set; }

        public ICommand ShowLegsCategory { get; set; }

        public ICommand ShowBeltCategory { get; set; }

        public ICommand ShowChestCategory { get; set; }

        public ICommand ShowBootsCategory { get; set; }

        public ICommand ShowShouldersCategory { get; set; }

        public GenericCommand EditItem { get; set; }

        #endregion
        

        public ShopVM(NinjaListVM ninjaList)
        {
            this.NinjaList = ninjaList;
            using (var context = new LeagueOfNinjasEntities())
            {
                var gear = context.Gears.ToList();

                ShopItems = new ObservableCollection<ItemVM>(gear.Select(g => new ItemVM(g)));
                TempShopItems = new ObservableCollection<ItemVM>();
            }
            ShowAddItemCommand = new RelayCommand(ShowAddItem);
            DeleteItemCommand = new GenericCommand(DeleteItem, CanDeleteItem);

            //Switch tabs

            EditItem = new GenericCommand(ShowEditItem, CanEditMethod);
            ShowHeadCategory = new RelayCommand(RetrieveHeadItems);
            ShowLegsCategory = new RelayCommand(RetrieveLegsItems);
            ShowBeltCategory = new RelayCommand(RetrieveBeltItems);
            ShowChestCategory = new RelayCommand(RetrieveChestItems);
            ShowBootsCategory = new RelayCommand(RetrieveBootsItems);
            ShowShouldersCategory = new RelayCommand(RetrieveShouldersItems);

            BuyItemCommand = new GenericCommand(BuyItem, CanBuyItem);
        }

        private void ShowEditItem(object parameter)
        {
            var window = new EditItemWindow();
            window.Show();
        }

        private void DeleteItem(object parameter)
        {
            NinjaList.SelectedNinja.RemoveItem(SelectedItem);
            
            ShopItems.Remove(SelectedItem);
            using (var context = new LeagueOfNinjasEntities())
            {
                context.Entry(NinjaList.SelectedNinja.ToModel()).State = EntityState.Modified;
                context.Entry(SelectedItem.ToModel()).State = EntityState.Deleted;

                context.SaveChanges();
            }

            TempShopItems.Remove(SelectedItem);
        }

        private bool CanDeleteItem(object parameter)
        {
            if (SelectedItem != null)
            {
                return true;
            }
            return false;
        }

        private void ShowAddItem()
        {
            var window = new AddItemWindow();
            window.Show();
        }

        private bool CanBuyItem(object parameter)
        {
            if (SelectedItem != null)
            {
                foreach (var item in NinjaList.SelectedNinja.InventoryItems)
                {
                    if (item.Category == SelectedItem.Category)
                    {
                        return false;
                    }
                }
                if (NinjaList.SelectedNinja.Gold < SelectedItem.Price)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool CanEditMethod(object parameter)
        {
            if (SelectedItem != null)
            {
                return true;
            }
            return false;
        }

        private void BuyItem(object parameter)
        {
            using (var context = new LeagueOfNinjasEntities())
            {
                Ninja n = context.Ninjas.Find(NinjaList.SelectedNinja.ToModel().Id);
                Gear g = context.Gears.Find(SelectedItem.ToModel().Id);
                n.Gears.Add(g);
                n.Gold -= SelectedItem.Price;
                context.SaveChanges();
            }
            NinjaList.SelectedNinja.GiveItem(SelectedItem);
        }

        public void RetrieveCategoryItems(String categoryName)
        {
            TempShopItems.Clear();

            foreach (var item in ShopItems)
            {
                if (item.Category.ToLower().Equals(categoryName.ToLower()))
                {
                    TempShopItems.Add(item);
                }
            }
        }

        public void UpdateItem(string updatedCategory)
        {
            string oldCategory = SelectedItem.Category;
            SelectedItem.Category = updatedCategory;

            int counter = 0;
            foreach (ItemVM item in ShopItems)
            {
                if (item.ToModel().Id == SelectedItem.ToModel().Id)
                {
                    break;
                }
                counter++;
            }
            ShopItems[counter] = SelectedItem;
            RetrieveCategoryItems(oldCategory);
        }

        #region Switch Tabs
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
        #endregion

    }
}