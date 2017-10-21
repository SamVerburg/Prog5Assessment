using GalaSoft.MvvmLight.Command;
using LeagueOfNinjas.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LeagueOfNinjas.ViewModel
{
    public class NinjaInfoVM
    {
        public NinjaVM SelectedNinja { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }

        public ICommand ShowInventoryCommand { get; set; }

        public ICommand ShowShopCommand { get; set; }

        public NinjaInfoVM(NinjaVM selectedNinja)
        {
            this.SelectedNinja = selectedNinja;
            CalculateStats();

            ShowInventoryCommand = new RelayCommand(ShowInventoryWindow);
            ShowShopCommand = new RelayCommand(ShowShopWindow);


        }

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
        

        private void CalculateStats()
        {
            foreach (var i in SelectedNinja.InventoryItems)
            {
                Strength += i.Strength;
                Intelligence += i.Intelligence;
                Agility += i.Agility;
            }

        }
    }
}
