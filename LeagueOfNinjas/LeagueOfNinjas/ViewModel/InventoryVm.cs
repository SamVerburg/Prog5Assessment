using GalaSoft.MvvmLight;
using System.Data.Entity;
using System.Linq;

namespace LeagueOfNinjas.ViewModel
{
    public class InventoryVM : ViewModelBase
    {
        public NinjaVM SelectedNinja { get; set; }

        public GenericCommand ClearInventoryCommand { get; set; }


        public InventoryVM(NinjaVM selectedNinja)
        {
            SelectedNinja = selectedNinja;

            ClearInventoryCommand = new GenericCommand(Clear, CanClear);

        }

        private bool CanClear(object parameter)
        {
            return SelectedNinja.InventoryItems.Count > 0;
        }

        private void Clear(object parameter)
        {
            int id = SelectedNinja.ToModel().Id;
            foreach (var item in SelectedNinja.InventoryItems)
            {
                using (var ctx = new LeagueOfNinjasEntities())
                {
                    Ninja ninja = (from n in ctx.Ninjas
                        where n.Id == id
                        select n).FirstOrDefault<Ninja>();

                    Gear gear = ninja.Gears.FirstOrDefault<Gear>();
                    ninja.Intelligence -= item.Intelligence;
                    ninja.Strength -= item.Strength;
                    ninja.Agility -=item.Agility;
                    ninja.Gold += item.Price;

                    //removing item from ninja
                    ninja.Gears.Remove(gear);
                    ctx.SaveChanges();
                }
            }
            SelectedNinja.ClearInventory();
        }
    }
}
