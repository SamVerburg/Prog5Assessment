using GalaSoft.MvvmLight;
using System.Data.Entity;

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
            using (var context = new LeagueOfNinjasEntities())
            {
                SelectedNinja.ClearInventory();
                context.Entry(SelectedNinja.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
