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
   public class InventoryVM
    {
        private NinjaVM _selectedNinja;

        public ICommand ClearInventory { get; set; }


        public InventoryVM(NinjaVM selectedNinja)
        {
            _selectedNinja = selectedNinja;

            ClearInventory = new RelayCommand(Clear,CanClear);

        }

        private bool CanClear()
        {
            if(_selectedNinja.InventoryItems.Count > 0)
            {
                return true;
            }
            return false;
        }

        private void Clear()
        {
            int inventoryValue = 0;

            foreach (var item in _selectedNinja.InventoryItems)
            {
                inventoryValue += item.Price;
                _selectedNinja.InventoryItems.Remove(item);

                using (var context = new LeagueOfNinjasEntities())
                {
                    context.Entry(item.ToModel()).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            _selectedNinja.Gold += inventoryValue;


        }


    }
}
