// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Mvvm.Commands
{
    /// <summary>
    ///     Parameterless command.
    /// </summary>
    public interface ICommand : System.Windows.Input.ICommand
    {
        /// <summary>
        ///     Checks whether the command can be executed or not.
        /// </summary>
        /// <returns><i>true</i> if command can be executed and <i>false</i> otherwise.</returns>
        bool CanExecute();

        /// <summary>
        ///     Executes the command.
        /// </summary>
        void Execute();
    }

    /// <summary>
    ///     Strongly-typed command.
    /// </summary>
    /// <typeparam name="T">Expected type of command parameter.</typeparam>
    public interface ICommand<in T> : System.Windows.Input.ICommand
    {
        /// <summary>
        ///     Checks whether the command can be executed or not with given strongly typed parameter.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns><i>true</i> if command can be executed and <i>false</i> otherwise.</returns>
        bool CanExecute(T parameter);

        /// <summary>
        ///     Executes the command using strongly typed parameter.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        void Execute(T parameter);
    }
}
