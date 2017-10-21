using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LeagueOfNinjas.ViewModel
{
    class NinjaInfoVM
    {
        public NinjaVM SelectedNinja { get; set; }

        private NinjaListVM _ninjaList;

        public ICommand ShowInventoryCommand;

        public NinjaInfoVM(NinjaListVM ninjaList)
        {
            _ninjaList = ninjaList;
            SelectedNinja = _ninjaList.SelectedNinja;

            //ShowInventoryCommand = new RelayCommand();
        }
    }
}
