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

            //Retrieve inventory items from database
            using (var ctx = new LeagueOfNinjasEntities())
            {
                Ninja ninja = (from s in ctx.Ninja
                               where s.Id == n.Id
                               select s).FirstOrDefault<Ninja>();


                var inventoryItems = ninja.Gear.ToList();

                foreach (var item in inventoryItems)
                {
                    InventoryItems.Add(new ItemVM(item));

                }
                ctx.SaveChanges();
            }
            n.Strength = n.Strength;
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

        private int _strength;
        public int Strength
        {
            get
            { return _strength; }

            set
            {
                _strength = value;
                OnPropertyChanged("Strength");
            }
        }


        private int _agility;
        public int Agility
        {
            get
            { return _agility; }
            
            set
            {
                _agility = value;
                OnPropertyChanged("Agility");
            }
        }



        private int _intelligence;
        public int Intelligence
        {
            get
            { return _intelligence; }

            set
            {
                _intelligence = value;
                OnPropertyChanged("_intelligence");
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