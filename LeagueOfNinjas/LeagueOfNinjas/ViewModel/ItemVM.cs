using GalaSoft.MvvmLight;
using System;
using System.ComponentModel;

namespace LeagueOfNinjas.ViewModel
{
    public class ItemVM : ViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        private Gear g;

        public ItemVM(Gear g)
        {
            this.g = g;
        }

        public ItemVM()
        {
            g = new Gear();
        }

        public string Name
        {
            get { return g.Name; }
            set
            {
                g.Name = value;
                OnPropertyChanged("Name");
            }
        }
        public int Price
        {
            get { return g.Price; }
            set
            {
                g.Price = value;
                OnPropertyChanged("Price");
            }
        }


        public string Category
        {
            get { return g.Category; }
            set
            {
                g.Category = value;
                OnPropertyChanged("Category");
            }
        }
        public int Strength
        {
            get { return g.Strength; }
            set
            {
                g.Strength = value;
                OnPropertyChanged("Strength");
            }
        }

        public int Intelligence
        {
            get { return g.Intelligence; }
            set
            {
                g.Intelligence = value;
                OnPropertyChanged("Intelligence");
            }
        }
        public int Agility
        {
            get { return g.Agility; }
            set
            {
                g.Agility = value;
                OnPropertyChanged("Agility");
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
                    case "Category":
                        if (string.IsNullOrEmpty(Category))
                            result = "Category is required!";
                        break;

                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                            result = "Name is required!";
                        break;

                    case "Price":
                        if(Price < 1)
                        {
                            result = "Please enter more than 0 gold";
                        }
                        break;
                    case "Strength":
                        int temp;
                        if (!int.TryParse(Strength.ToString(), out temp))
                        {
                            result = "Please enter more than 0 points in Strength";
                        }
                        break;
                    case "Agility":
                        temp = 0;
                        if (!int.TryParse(Agility.ToString(), out temp))
                        {
                            result = "Please enter more than 0 points in Agility";
                        }
                        break;
                    case "Intelligence":
                        temp = 0;
                        if (!int.TryParse(Intelligence.ToString(), out temp))
                        {
                            result = "Please enter more than 0 points in Intelligence";
                        }
                        break;

                }
                return result;
            }
        }

        internal Gear ToModel()
        {
            return g;
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
