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
        //public ICommand ShowInventoryCommand;

        public NinjaInfoVM(NinjaVM selectedNinja)
        {
            this.SelectedNinja = selectedNinja;
            CalculateStats();
        
            //ShowInventoryCommand = new RelayCommand();
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
