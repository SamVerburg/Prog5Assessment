using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace LeagueOfNinjas.ViewModel
{
    public class NinjaListVM : ViewModelBase
    {
        public ObservableCollection<NinjaVM> Ninjas { get; set; }

        public ICommand ShowAddNinja;

        public NinjaListVM()
        {

            ShowAddNinja = new RelayCommand(ShowAddNinjaWindow);
            using (var context = new LeagueOfNinjasEntities())
            {
                var ninjas = context.Ninja.ToList();
                Ninjas = new ObservableCollection<NinjaVM>(ninjas.Select(n => new NinjaVM(n)));
            }
        }

        private void ShowAddNinjaWindow()
        {
            var window = new AddNinjaWindow();
            window.Show();

        }

    }
}
