using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LeagueOfNinjas.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LeagueOfNinjas.ViewModel
{
   public class InventoryVM : ViewModelBase
    {
        public NinjaVM SelectedNinja;

        public ObservableCollection<ItemVM> InventoryItems { get; set; }


        public ClearInventoryCommand ClearInventory { get; set; }


        public InventoryVM(NinjaVM selectedNinja)
        {
            SelectedNinja = selectedNinja;

            InventoryItems = SelectedNinja.InventoryItems;

            ClearInventory = new ClearInventoryCommand(ExecuteMethod, CanExecuteMethod);

            UpdateStats();

        }

        private bool CanExecuteMethod(object parameter)
        {
            if(SelectedNinja.InventoryItems.Count > 0)
            {
                return true;
            }
            return false;
        }

        private void ExecuteMethod(object parameter)
        {
            int moneyBack = 0;
            int id = SelectedNinja.ToModel().Id;
            foreach (var item in SelectedNinja.InventoryItems)
            {
                using (var ctx = new LeagueOfNinjasEntities())
                {
                    Ninja ninja = (from n in ctx.Ninja
                                   where n.Id == id
                                   select n).FirstOrDefault<Ninja>();

                    Gear gear = ninja.Gear.FirstOrDefault<Gear>();

                    //removing item from ninja
                    ninja.Gear.Remove(gear);
                    moneyBack += item.Price;
                    ninja.Gold += moneyBack;

                    ctx.SaveChanges();
                }
                SelectedNinja.Gold += moneyBack;
            }

          

            SelectedNinja.InventoryItems.Clear();
            UpdateStats();           
        }

        private void UpdateStats()
        {
            SelectedNinja.Intelligence = 0;
            SelectedNinja.Strength = 0;
            SelectedNinja.Agility = 0;

            foreach (var i in InventoryItems)
            {
                SelectedNinja.Intelligence += i.Intelligence;
                SelectedNinja.Strength += i.Strength;
                SelectedNinja.Agility += i.Agility;
            }
        }
    }
}
