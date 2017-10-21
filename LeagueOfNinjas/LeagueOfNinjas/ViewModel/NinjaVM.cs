using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace LeagueOfNinjas.ViewModel
{
    public class NinjaVM : ViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        private Ninja n;

        public ObservableCollection<ItemVM> InventoryItems { get; set; }

        public NinjaVM(Ninja n)
        {
            this.n = n;
        }

        public NinjaVM()
        {
            n = new Ninja();
        }

        public string Name
        {
            get { return n.Name; }
            set
            {
                n.Name = value;
                OnPropertyChanged("Name");

                using (var context = new LeagueOfNinjasEntities())
                {
                    var inventoryItems = n.Gear.ToList();

                    //Retrieve Ninja's inventory Items
                    InventoryItems = new ObservableCollection<ItemVM>(inventoryItems.Select(g => new ItemVM(g)));
                }

            }
        }

        public int Gold
        {
            get { return n.Gold; }
            set
            {
                n.Gold = value;
                OnPropertyChanged("Gold");
            }
        }

        //IDataErrorInfo
        public string Error
        {
            get { return null; }

            set
            {

            }
        }


        public string this[string PropertyName]
        {
            get {           
                string result = String.Empty;

                switch(PropertyName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                            result = "Name is required!";
                        break;

                    case "Gold":
                        int ParsedGold;
                        bool correctInput = Int32.TryParse(Gold.ToString(), out ParsedGold);
                        if (String.IsNullOrEmpty(Gold.ToString()) || ParsedGold < 1 || !correctInput)
                            {
                                result = "Please enter more than 0 gold";
                            }
                        break;

                }
                return result;
            }
        }

        internal Ninja ToModel()
        {
            return n;
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}