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

        public ICommand BuyItemCommand { get; set; }

        public ICommand ShowCategoryCommand { get; set; }

        private NinjaVM _selectedNinja;

        public ShopVM(NinjaVM selectedNinja)
        {
            this._selectedNinja = selectedNinja;

            using (var context = new LeagueOfNinjasEntities())
            {
                var gear = context.Gear.ToList();

                ShopItems = new ObservableCollection<ItemVM>(gear.Select(g => new ItemVM(g)));
            }

            BuyItemCommand = new RelayCommand(BuyItem);
        }

        private void BuyItem()
        {
            using (var context = new LeagueOfNinjasEntities())
            {
                Ninja n = context.Ninja.Find(_selectedNinja.ToModel().Id);
                Gear g = context.Gear.Find(SelectedItem.ToModel().Id);
                n.Gear.Add(g);
                n.Gold -= SelectedItem.Price;
                context.SaveChanges();
            }
            _selectedNinja.Gold -= SelectedItem.Price;
            _selectedNinja.InventoryItems.Add(SelectedItem);
        }
    }
}