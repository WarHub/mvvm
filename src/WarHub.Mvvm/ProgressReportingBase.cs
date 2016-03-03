// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Mvvm
{
    using System;

    /// <summary>
    ///     Provides derivable base for progress reporting objects with a range of helper methods.
    /// </summary>
    public class ProgressReportingBase : NotifyPropertyChangedBase, IProgressReportingObject
    {
        private bool _isInProgress;
        private string _operationTitle;
        private int? _progressPercent;

        /// <summary>
        ///     Gets or sets action invoked as last in <see cref="DisposeActionExecutor.DisposalAction" /> of
        ///     <see cref="BeginProgress" />'s return value.
        /// </summary>
        protected Action PostProgressAction { get; set; }

        /// <summary>
        ///     Gets or sets action invoked directly before <see cref="BeginProgress" /> returns.
        /// </summary>
        protected Action PreProgressAction { get; set; }

        public bool IsInProgress
        {
            get { return _isInProgress; }
            private set { Set(ref _isInProgress, value); }
        }

        public string OperationTitle
        {
            get { return _operationTitle; }
            set { Set(ref _operationTitle, value); }
        }

        public int? ProgressPercent
        {
            get { return _progressPercent; }
            protected set { Set(ref _progressPercent, value); }
        }

        /// <summary>
        ///     Begins progress operation by setting <see cref="IsInProgress" /> to <i>true</i>. Dispose
        ///     of returned <see cref="DisposeActionExecutor" /> to set <see cref="IsInProgress" /> back to <i>false</i>.
        /// </summary>
        /// <returns>Token to dispose at the end of operation.</returns>
        protected DisposeActionExecutor BeginProgress()
        {
            IsInProgress = true;
            ProgressPercent = 0;
            PreProgressAction?.Invoke();
            return new DisposeActionExecutor(() =>
            {
                IsInProgress = false;
                ProgressPercent = null;
                PostProgressAction?.Invoke();
            });
        }
    }
}
