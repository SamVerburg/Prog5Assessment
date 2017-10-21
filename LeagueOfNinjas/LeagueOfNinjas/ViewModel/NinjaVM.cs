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

            this.InventoryItems = new ObservableCollection<ItemVM>();
            foreach (Gear g in n.Gear)
            {
                InventoryItems.Add(new ItemVM(g));
            }

        }

        public NinjaVM()
        {
            n = new Ninja();

            this.InventoryItems = new ObservableCollection<ItemVM>();
            foreach (Gear g in n.Gear)
            {
                InventoryItems.Add(new ItemVM(g));
            }
        }



        public string Name
        {
            get { return n.Name; }
            set
            {
                n.Name = value;
                OnPropertyChanged("Name");
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

        //Statistics

        public int Strength
        {
            get
            {
                int value = 0;
                foreach (var i in InventoryItems)
                {
                    value += i.Strength;
                }
                return value;
            }
            set
            {
                Strength = value;
                OnPropertyChanged("Strength");
            }
        }

        public int Agility
        {
            get {
                int value = 0;
                foreach (var i in InventoryItems)
                {
                    value += i.Agility;
                }
                return value;
            }
            set
            {
                Agility = value;
                OnPropertyChanged("Agility");
            }
        }


        public int Intelligence
        {
            get {
                int value = 0;
                foreach (var i in InventoryItems)
                {
                    value += i.Intelligence;
                }
                return value;
            }
            set
            {
                Intelligence = value;
                OnPropertyChanged("Intelligence");
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
            get
            {
                string result = String.Empty;

                switch (PropertyName)
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