using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueOfNinjas.ViewModel
{
    class InventoryVM
    {
        private NinjaVM _selectedNinja;

        public ObservableCollection<ItemVM> InventoryItems { get; set; }

        public InventoryVM(NinjaVM selectedNinja)
        {
            _selectedNinja = selectedNinja;
            InventoryItems = _selectedNinja.InventoryItems;
        }

        public void Clear()
        {

        }


    }
}
