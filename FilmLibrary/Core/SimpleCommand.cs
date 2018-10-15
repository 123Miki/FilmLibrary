using System;
using System.Windows.Input;

namespace FilmLibrary.Core
{
    public class SimpleCommand<T> : ICommand
    {
        public virtual event EventHandler CanExecuteChanged;

        private Action<T> _executeMethod;
        private Func<T, bool> _canExecuteMethod;

        public SimpleCommand(Action<T> executeMethod)
        {
            if (null == executeMethod)
            {
                throw new NullReferenceException("executeMethode ne doit pas être null.");
            }

            _executeMethod = executeMethod;
        }

        public SimpleCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
            : this(executeMethod)
        {
            _canExecuteMethod = canExecuteMethod;
        }

        public virtual bool CanExecute(object parameter)
        {
            if (null != _canExecuteMethod)
            {
                return _canExecuteMethod((T)parameter);
            }

            return true;
        }

        public virtual void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                _executeMethod((T)parameter);
            }
        }

        public virtual void RaiseCanExecuteChanged()
        {
            if (null != CanExecuteChanged)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }
    }

    public class SimpleCommand : SimpleCommand<object>
    {
        public SimpleCommand(Action executeMethod)
            : base(parameter => executeMethod())
        {

        }

        public SimpleCommand(Action executeMethod, Func<bool> canExecuteMethod)
            : base(parameter => executeMethod(), parameter => canExecuteMethod())
        {

        }
    }

}