using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LeagueOfNinjas.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LeagueOfNinjas.ViewModel
{
    public class NinjaInfoVM : ViewModelBase
    {
        public NinjaVM SelectedNinja { get; set; }

        public ICommand ShowInventoryCommand { get; set; }

        public ICommand ShowShopCommand { get; set; }

        public NinjaInfoVM(NinjaVM selectedNinja)
        {
            this.SelectedNinja = selectedNinja;

            ShowInventoryCommand = new RelayCommand(ShowInventoryWindow);
            ShowShopCommand = new RelayCommand(ShowShopWindow);
            UpdateStats();
        }

        //Commands
        private void ShowShopWindow()
        {
            var window = new ShopWindow();
            window.Show();
        }

        private void ShowInventoryWindow()
        {
            var window = new InventoryWindow();
            window.Show();
        }

        private void UpdateStats()
        {
            SelectedNinja.Intelligence = 0;
            SelectedNinja.Strength = 0;
            SelectedNinja.Agility = 0;

            foreach (var i in SelectedNinja.InventoryItems)
            {
                SelectedNinja.Intelligence += i.Intelligence;
                SelectedNinja.Strength += i.Strength;
                SelectedNinja.Agility += i.Agility;
            }
        }





    }
}
