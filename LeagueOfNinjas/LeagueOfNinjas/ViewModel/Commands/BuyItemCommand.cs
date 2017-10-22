using System;
using System.Windows.Input;

namespace LeagueOfNinjas.ViewModel
{
    public class BuyItemCommand : ICommand
    {
        Action<object> executeMethod;
        Func<object, bool> canexecuteMethod;


        public BuyItemCommand(Action<object> executeMethod, Func<object, bool> canexecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canexecuteMethod = canexecuteMethod;

        }

        public bool CanExecute(object parameter)
        {
            return canexecuteMethod(parameter);
        }

        public void Execute(object parameter)
        {
            executeMethod(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}

