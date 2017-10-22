using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup.Localizer;

namespace LeagueOfNinjas.ViewModel
{
    public class ShopVM : ViewModelBase
    {
        public ObservableCollection<ItemVM> ShopItems { get; set; }

        public ItemVM SelectedItem { get; set; }

        public BuyItemCommand BuyItem { get; set; }

        public ICommand ShowCategoryCommand { get; set; }

        public NinjaVM SelectedNinja;

        public ShopVM(NinjaVM selectedNinja)
        {
            this.SelectedNinja = selectedNinja;

            using (var context = new LeagueOfNinjasEntities())
            {
                var gear = context.Gear.ToList();

                ShopItems = new ObservableCollection<ItemVM>(gear.Select(g => new ItemVM(g)));
            }

            BuyItem = new BuyItemCommand(ExecuteMethod, CanExecuteMethod);
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
                if(SelectedNinja.Gold <= SelectedItem.Price)
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
    }
}