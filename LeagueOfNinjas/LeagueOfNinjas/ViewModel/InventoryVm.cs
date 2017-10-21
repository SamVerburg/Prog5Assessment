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

        public ObservableCollection<ItemVM> InventoryItems { get; set; }


        public ICommand ClearInventory { get; set; }


        public InventoryVM(NinjaVM selectedNinja)
        {
            _selectedNinja = selectedNinja;

            InventoryItems = _selectedNinja.InventoryItems;

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

            foreach (var item in _selectedNinja.InventoryItems)
            {
                _selectedNinja.Gold += item.Price;

                using (var context = new LeagueOfNinjasEntities())
                {

                    context.Entry(_selectedNinja.ToModel()).State = EntityState.Modified;

                    context.SaveChanges();
                }
            }
            _selectedNinja.InventoryItems.Clear();



        }


    }
}
