/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:LeagueOfNinjas"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace LeagueOfNinjas.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private NinjaListVM _ninjas;
        private ShopVM _shop;
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            _ninjas = new NinjaListVM();
            _shop = new ShopVM(_ninjas);
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public AddNinjaVM AddNinja
        {
            get
            {
                return new AddNinjaVM(_ninjas);
            }
        }

        public EditNinjaVM EditNinja
        {
            get
            {
                return new EditNinjaVM(_ninjas.SelectedNinja);
            }
        }

        public NinjaListVM Ninjas
        {
            get
            {
                return _ninjas;
            }
        }

        public AddItemVM AddItem
        {
            get
            {
                return new AddItemVM(_shop);
            }
        }

        public EditItemVM EditItem
        {
            get
            {
                return new EditItemVM(_ninjas.SelectedNinja, Shop);
            }
        }


        public NinjaInfoVM NinjaInfo
        {
            get
            {
                return new NinjaInfoVM(_ninjas.SelectedNinja);
            }
        }

        public InventoryVM NinjaInventory
        {
            get
            {
                return new InventoryVM(_ninjas.SelectedNinja);
            }
        }

        public ShopVM Shop
        {
            get { return _shop; }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}