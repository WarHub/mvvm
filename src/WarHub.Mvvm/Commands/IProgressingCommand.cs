// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Mvvm.Commands
{
    /// <summary>
    ///     Reports by property whether execution is finished or not and optionally on execution progress.
    /// </summary>
    public interface IProgressingCommand : ICommand, IProgressReportingObject
    {
    }

    /// <summary>
    ///     Reports by property whether execution is finished or not and optionally on execution progress.
    /// </summary>
    public interface IProgressingCommand<in T> : ICommand<T>, IProgressReportingObject
    {
    }
}
