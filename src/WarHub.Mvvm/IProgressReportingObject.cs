// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Mvvm
{
    using System.ComponentModel;

    /// <summary>
    ///     Provides (real-time) operation progress.
    /// </summary>
    public interface IProgressReportingObject : INotifyPropertyChanged
    {
        /// <summary>
        ///     Gets progress status. If true, operation is in progress.
        /// </summary>
        bool IsInProgress { get; }

        /// <summary>
        ///     Gets or sets optional title of executing operation.
        /// </summary>
        string OperationTitle { get; set; }

        /// <summary>
        ///     Gets percent value of execution progress. Null means indeterminate operation.
        /// </summary>
        int? ProgressPercent { get; }
    }
}
