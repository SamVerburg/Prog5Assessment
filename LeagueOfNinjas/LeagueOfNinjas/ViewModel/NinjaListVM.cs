using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LeagueOfNinjas.Views;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;

namespace LeagueOfNinjas.ViewModel
{
    public class NinjaListVM : ViewModelBase
    {
        public ObservableCollection<NinjaVM> Ninjas { get; set; }

        public ICommand ShowAddNinja { get; set; }

        public GenericCommand ShowEditNinja { get; set; }

        public GenericCommand DeleteNinjaCommand { get; set; }

        public GenericCommand LoginCommand { get; set; }

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
            ShowEditNinja = new GenericCommand(ShowEditNinjaWindow, CanExecute);
            DeleteNinjaCommand = new GenericCommand(Delete, CanExecute);
            LoginCommand = new GenericCommand(Login, CanExecute);
        }

        private void Delete(object parameter)
        {
            using (var context = new LeagueOfNinjasEntities())
            {
                context.Entry(SelectedNinja.ToModel()).State = EntityState.Deleted;
                context.SaveChanges();
            }

            Ninjas.Remove(SelectedNinja);
        }

        public bool CanExecute(object parameter)
        {
            if (SelectedNinja != null)
            {
                return true;
            }
            return false;
        }

        private void Login(object parameter)
        {
            var window = new NinjaInfoWindow();
            window.Show();
        }

        private void ShowAddNinjaWindow()
        {
            var window = new AddNinjaWindow();
            window.Show();
        }

        private void ShowEditNinjaWindow(object parameter)
        {
            var window = new EditNinjaWindow();
            window.Show();
        }
    }
}
