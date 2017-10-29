using GalaSoft.MvvmLight;
using System;
using System.Data.Entity;

namespace LeagueOfNinjas.ViewModel
{
    public class AddNinjaVM : ViewModelBase
    {
        #region properties

        public NinjaVM Ninja { get; set; }

        public GenericCommand AddCommand { get; set; }

        private NinjaListVM _ninjas;

        #endregion
        
        public AddNinjaVM(NinjaListVM ninjas)
        {
            _ninjas = ninjas;
            Ninja = new NinjaVM();
            AddCommand = new GenericCommand(Add, CanAdd);
        }

        private bool CanAdd(object parameter)
        {
            return !string.IsNullOrEmpty(Ninja.Name) && Ninja.Gold > 0;
        }

        private void Add(object parameter)
        {
            _ninjas.Ninjas.Add(Ninja);
            using (var context = new LeagueOfNinjasEntities())
            {
                context.Entry(Ninja.ToModel()).State = EntityState.Added;
                context.SaveChanges();
            }
        }
    }
}
