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
            = new ObservableCollection<ItemVM>();

        public NinjaVM(Ninja n)
        {
            this.n = n;
            using (var ctx = new LeagueOfNinjasEntities())
            {
                Ninja ninja = (from s in ctx.Ninjas
                               where s.Id == n.Id
                               select s).FirstOrDefault<Ninja>();


                var inventoryItems = ninja.Gears.ToList();

                foreach (var item in inventoryItems)
                {
                    InventoryItems.Add(new ItemVM(item));

                }
            }
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
            { return n.Strength; }

            set
            {
                n.Strength = value;
                OnPropertyChanged("Strength");
            }
        }

        public int Agility
        {
            get
            { return n.Agility; }

            set
            {
                n.Agility = value;
                OnPropertyChanged("Agility");
            }
        }

        public int Intelligence
        {
            get
            { return n.Intelligence; }
            set
            {
                n.Intelligence = value;
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

        public void RemoveItem(ItemVM selectedItem)
        {
            foreach (ItemVM item in InventoryItems)
            {
                if (item.ToModel().Id != selectedItem.ToModel().Id) continue;
                Gold += item.Price;
                InventoryItems.Remove(item);
                UpdateStats();
                break;
            }
        }

        public void ClearInventory()
        {
            foreach (ItemVM item in InventoryItems)
            {
                Gold += item.Price;
            }
            InventoryItems.Clear();
            UpdateStats();
        }

        public void AddItem(ItemVM selectedItem)
        {
            Gold -= selectedItem.Price;
            InventoryItems.Add(selectedItem);
            UpdateStats();
        }

        public void UpdateStats()
        {
            Intelligence = 0;
            Strength = 0;
            Agility = 0;
            foreach (var i in InventoryItems)
            {
                Intelligence += i.Intelligence;
                Strength += i.Strength;
                Agility += i.Agility;
            }
        }
    }
}