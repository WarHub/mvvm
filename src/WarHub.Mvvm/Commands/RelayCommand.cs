// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Mvvm.Commands
{
    using System;

    /// <summary>
    ///     A command whose sole purpose is to relay its functionality to other objects by invoking delegates. The default
    ///     return value for the CanExecute method is 'true'. <see cref="RaiseCanExecuteChanged" /> needs to be called whenever
    ///     <see cref="CanExecute()" /> and <see cref="CanExecute(object)" /> is expected to return a different value.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        ///     Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        /// <summary>
        ///     Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            ExecuteAction = execute;
            CanExecuteFunc = canExecute;
        }

        protected Func<bool> CanExecuteFunc { get; }

        protected Action ExecuteAction { get; }

        /// <summary>
        ///     Raised when <see cref="CanExecute(object)" /> may return different value.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Determines whether this <see cref="RelayCommand" /> can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter) => CanExecute();

        /// <summary>
        ///     Executes the <see cref="RelayCommand" /> on the current command target.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public void Execute(object parameter) => Execute();

        /// <summary>
        ///     Checks whether the command can be executed or not.
        /// </summary>
        /// <returns><i>true</i> if command can be executed and <i>false</i> otherwise.</returns>
        public bool CanExecute() => CanExecuteFunc?.Invoke() ?? true;

        /// <summary>
        ///     Executes the command.
        /// </summary>
        public void Execute() => ExecuteAction();

        /// <summary>
        ///     Method used to raise the <see cref="CanExecuteChanged" /> event     to indicate that the return value of the
        ///     <see cref="CanExecute()" /> and <see cref="CanExecute(object)" />     method has changed.
        /// </summary>
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    ///     A command whose sole purpose is to relay its functionality to other objects by invoking delegates. The default
    ///     return value for the CanExecute method is 'true'. <see cref="RaiseCanExecuteChanged" /> needs to be called whenever
    ///     <see cref="CanExecute(object)" /> and <see cref="CanExecute(T)" /> is expected to return a different value.
    /// </summary>
    public class RelayCommand<T> : ICommand<T>
    {
        /// <summary>
        ///     Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        ///     Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            ExecuteAction = execute;
            CanExecuteFunc = canExecute;
        }

        protected Func<T, bool> CanExecuteFunc { get; }

        protected Action<T> ExecuteAction { get; }

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        ///     true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        public bool CanExecute(object parameter) => CanExecute((T) parameter);

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        public void Execute(object parameter) => Execute((T) parameter);

        /// <summary>
        ///     Raised when <see cref="CanExecute(object)" /> may return different value.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Checks whether the command can be executed or not with given strongly typed parameter.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns><i>true</i> if command can be executed and <i>false</i> otherwise.</returns>
        public bool CanExecute(T parameter) => CanExecuteFunc?.Invoke(parameter) ?? true;

        /// <summary>
        ///     Executes the command using strongly typed parameter.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        public void Execute(T parameter) => ExecuteAction(parameter);

        /// <summary>
        ///     Method used to raise the <see cref="CanExecuteChanged" /> event to indicate that the return value of the
        ///     <see cref="CanExecute(T)" /> and <see cref="CanExecute(object)" /> method has changed.
        /// </summary>
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
