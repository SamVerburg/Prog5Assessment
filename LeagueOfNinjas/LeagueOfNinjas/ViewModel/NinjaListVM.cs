using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Data.Entity;
using LeagueOfNinjas.Views;

namespace LeagueOfNinjas.ViewModel
{
    public class NinjaListVM : ViewModelBase
    {
        public ObservableCollection<NinjaVM> Ninjas { get; set; }

        public ICommand ShowAddNinja { get; set; }

        public ICommand ShowEditNinja { get; set; }

        public ICommand DeleteNinjaCommand { get; set; }

        public LogInCommand LogIn { get; set; }

        private NinjaVM _selectedNinja;

        public NinjaVM SelectedNinja
        {
            get { return _selectedNinja; }
            set
            {
                _selectedNinja = value;
                base.RaisePropertyChanged();
            }
        }

        public NinjaListVM()
        {

            using (var context = new LeagueOfNinjasEntities())
            {
                var ninjas = context.Ninjas.ToList();
                Ninjas = new ObservableCollection<NinjaVM>(ninjas.Select(n => new NinjaVM(n)));
            }

            ShowAddNinja = new RelayCommand(ShowAddNinjaWindow);
            ShowEditNinja = new RelayCommand(ShowEditNinjaWindow);
            DeleteNinjaCommand = new RelayCommand(DeleteNinja);
            LogIn = new LogInCommand(ExecuteMethod, CanExecuteMethod);
        }

        private void DeleteNinja()
        {
            using (var context = new LeagueOfNinjasEntities())
            {
                context.Entry(SelectedNinja.ToModel()).State = EntityState.Deleted;
                context.SaveChanges();
            }

            Ninjas.Remove(SelectedNinja);
        }

        //Validation for logging in
        private bool CanExecuteMethod(object parameter)
        {
            if (SelectedNinja != null)
            {
                return true;
            }
            return false;

        }

        private void ExecuteMethod(object parameter)
        {
            var window = new NinjaInfoWindow();
            window.Show();
        }

        private void ShowAddNinjaWindow()
        {
            var window = new AddNinjaWindow();
            window.Show();

        }

        private void ShowEditNinjaWindow()
        {
            var window = new EditNinjaWindow();
            window.Show();

        }





    }
}
