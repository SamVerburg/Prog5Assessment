using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
        public NinjaVM SelectedNinja { get; set; };

        public GenericCommand ClearInventoryCommand { get; set; }


        public InventoryVM(NinjaVM selectedNinja)
        {
            SelectedNinja = selectedNinja;

            ClearInventoryCommand = new GenericCommand(Clear, CanClear);

        }

        private bool CanClear(object parameter)
        {
            if (SelectedNinja.InventoryItems.Count > 0)
            {
                return true;
            }
            return false;
        }

        private void Clear(object parameter)
        {
            int moneyBack = 0;
            int id = SelectedNinja.ToModel().Id;
            foreach (var item in SelectedNinja.InventoryItems)
            {
                using (var ctx = new LeagueOfNinjasEntities())
                {
                    Ninja ninja = (from n in ctx.Ninjas
                                   where n.Id == id
                                   select n).FirstOrDefault<Ninja>();

                    Gear gear = ninja.Gears.FirstOrDefault<Gear>();

                    //removing item from ninja
                    ninja.Gears.Remove(gear);
                    moneyBack += item.Price;

                    //Update ninja database
                    ninja.Gold += moneyBack;

                    ctx.SaveChanges();
                }
                SelectedNinja.Gold += moneyBack;
            }
            ClearInventory();
        }


        public void ClearInventory()
        {
            foreach (ItemVM item in SelectedNinja.InventoryItems)
            {
                SelectedNinja.Gold += item.Price;
            }
            SelectedNinja.InventoryItems.Clear();
            SelectedNinja.UpdateStats();
        }
    }
}
