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
    public class NinjaInfoVM : ViewModelBase, INotifyPropertyChanged
    {
        public NinjaVM SelectedNinja { get; set; }

        public ICommand ShowInventoryCommand { get; set; }

        public ICommand ShowShopCommand { get; set; }

        public NinjaInfoVM(NinjaVM selectedNinja)
        {
            this.SelectedNinja = selectedNinja;

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

      



    }
}
