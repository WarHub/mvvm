// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Mvvm.Commands
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    ///     Base class providing awaited execution method, which by default reports progress.
    /// </summary>
    public abstract class ProgressingAsyncCommandBase : ProgressingCommandBase
    {
        /// <summary>
        ///     Calls <see cref="ExecuteCoreAsync" /> in <code>using (progress)</code> block.
        /// </summary>
        public override async void Execute()
        {
            try
            {
                using (BeginProgress())
                {
                    await ExecuteCoreAsync();
                }
            }
            catch (Exception e)
            {
                if (UseHandleExecutionException)
                    HandleExecutionException(e);
                if (RethrowExecutionException)
                    throw;
            }
        }

        /// <summary>
        ///     Unused, empty method. Please refer to <see cref="Execute" /> and <see cref="ExecuteCoreAsync" />.
        /// </summary>
        protected sealed override void ExecuteCore()
        {
        }

        /// <summary>
        ///     Invoked and awaited in <see cref="Execute" /> in
        ///     <code>
        /// using (progress)
        /// </code>
        ///     block.
        /// </summary>
        /// <returns></returns>
        protected abstract Task ExecuteCoreAsync();
    }

    /// <summary>
    ///     Base class providing awaited execution method, which by default reports progress.
    /// </summary>
    /// <typeparam name="T">Expected type of command parameter.</typeparam>
    public abstract class ProgressingAsyncCommandBase<T> : ProgressingCommandBase<T>
    {
        /// <summary>
        ///     Calls <see cref="ExecuteCoreAsync(T)" /> in <code>using (progress)</code> block.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        public override async void Execute(T parameter)
        {
            try
            {
                using (BeginProgress())
                {
                    await ExecuteCoreAsync(parameter);
                }
            }
            catch (Exception e)
            {
                if (UseHandleExecutionException)
                    HandleExecutionException(e);
                if (RethrowExecutionException)
                    throw;
            }
        }

        /// <summary>
        ///     Unused, empty method. Please refer to <see cref="Execute(T)" /> and <see cref="ExecuteCoreAsync(T)" />.
        /// </summary>
        /// <param name="parameter">Ignored.</param>
        protected sealed override void ExecuteCore(T parameter)
        {
        }

        /// <summary>
        ///     Invoked and awaited in <see cref="Execute(T)" /> in
        ///     <code>
        /// using (progress)
        /// </code>
        ///     block.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns></returns>
        protected abstract Task ExecuteCoreAsync(T parameter);
    }
}
