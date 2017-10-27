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
                Ninja ninja = (from s in ctx.Ninjas
                               where s.Id == n.Id
                               select s).FirstOrDefault<Ninja>();


                var inventoryItems = ninja.Gears.ToList();

                foreach (var item in inventoryItems)
                {
                    InventoryItems.Add(new ItemVM(item));

                }
                ctx.SaveChanges();
            }

            UpdateStats();
        }

        public NinjaVM()
        {
            n = new Ninja();

            this.InventoryItems = new ObservableCollection<ItemVM>();

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
            ItemVM foundItem = null;
            foreach (ItemVM item in InventoryItems)
            {
                if (item.ToModel().Id == selectedItem.ToModel().Id)
                {
                    foundItem = selectedItem;
                    break;
                }

            }
            if (foundItem != null)
            {
                InventoryItems.Remove(foundItem);
                this.Gold += selectedItem.Price;
                UpdateStats();
            }
        }

        public void AddItem(ItemVM selectedItem)
        {
            InventoryItems.Add(selectedItem);
            this.Gold -= selectedItem.Price;
            UpdateStats();
        }

        public void UpdateItem(ItemVM updatedItem)
        {
            bool found = false;
            int counter = 0;
            foreach (ItemVM item in InventoryItems)
            {
                if (item.ToModel().Id == updatedItem.ToModel().Id)
                {
                    found = true;
                    break;
                }
                counter++;
            }
            if (found)
            {
                InventoryItems[counter] = updatedItem;
                UpdateStats();
            }
        }
    }
}