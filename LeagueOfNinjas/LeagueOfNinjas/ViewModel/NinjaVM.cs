﻿using GalaSoft.MvvmLight;
using System;

namespace LeagueOfNinjas.ViewModel
{
    public class NinjaVM : ViewModelBase
    {
        private Ninja n;

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
            set { n.Name = value; }
        }

        public int Gold
        {
            get { return n.Gold; }
            set { n.Gold = value; }
        }

        internal Ninja ToModel()
        {
            return n;
        }
    }
}