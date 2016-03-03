// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Mvvm
{
    using System;

    /// <summary>
    ///     Executes provided action on disposal.
    /// </summary>
    public class DisposeActionExecutor : IDisposable
    {
        /// <summary>
        ///     Creates new token which calls provided action on disposal.
        /// </summary>
        /// <param name="disposalAction"></param>
        public DisposeActionExecutor(Action disposalAction = null)
        {
            DisposalAction = disposalAction;
        }

        private Action DisposalAction { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                DisposalAction();
            }
        }
    }
}
