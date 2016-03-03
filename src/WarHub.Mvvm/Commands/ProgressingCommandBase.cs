// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Mvvm.Commands
{
    using System;

    /// <summary>
    ///     Always executable base class for <see cref="ICommand" /> implementations.
    ///     Ignores provided parameters.
    /// </summary>
    public abstract class ProgressingCommandBase : ProgressReportingBase, IProgressingCommand
    {
        protected ProgressingCommandBase()
        {
            PreProgressAction = OnPreProgress;
            PostProgressAction = OnPostProgress;
        }

        /// <summary>
        ///     Gets or sets whether <see cref="CanExecute()" /> should return false while <see cref="Execute()" /> is executed.
        /// </summary>
        protected bool IsExecutionBlocking { get; set; } = true;

        /// <summary>
        ///     Gets or sets whether an exception thrown in <see cref="ExecuteCore" /> will be rethrown (directly, using 'throw;'
        ///     clause). True by default. Independent from <see cref="UseHandleExecutionException" />.
        /// </summary>
        protected bool RethrowExecutionException { get; set; } = true;

        /// <summary>
        ///     Gets or sets whether an exception thrown in <see cref="ExecuteCore" /> will be passed to
        ///     <see cref="HandleExecutionException" />. False by default. Independent from
        ///     <see cref="RethrowExecutionException" />.
        /// </summary>
        protected bool UseHandleExecutionException { get; set; } = false;

        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Checks if <see cref="IsExecutionBlocking" /> is true and <see cref="Execute()" /> is executed, otherwise, invokes
        ///     <see cref="CanExecuteCore" />.
        /// </summary>
        /// <returns>
        ///     If <see cref="IsExecutionBlocking" /> is true and <see cref="Execute()" /> is executed, returns false. Otherwise,
        ///     result of <see cref="CanExecuteCore" /> is returned.
        /// </returns>
        public bool CanExecute()
        {
            return (!IsExecutionBlocking || !IsInProgress) && CanExecuteCore();
        }

        /// <summary>
        ///     Invokes parameterless <see cref="CanExecute()" />.
        /// </summary>
        /// <param name="parameter">Ignored.</param>
        public bool CanExecute(object parameter)
        {
            return CanExecute();
        }

        /// <summary>
        ///     Invokes <see cref="ExecuteCore" /> in <code>using (progress)</code> block.
        /// </summary>
        public virtual void Execute()
        {
            try
            {
                using (BeginProgress())
                {
                    ExecuteCore();
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
        ///     Invokes parameterless <see cref="Execute()" />.
        /// </summary>
        /// <param name="parameter">Ignored.</param>
        public void Execute(object parameter)
        {
            Execute();
        }

        /// <summary>
        ///     By default set as <see cref="ProgressReportingBase.PreProgressAction" />. Raises <see cref="CanExecuteChanged" />.
        /// </summary>
        protected virtual void OnPreProgress()
        {
            RaiseCanExecuteChanged();
        }

        /// <summary>
        ///     By default set as <see cref="ProgressReportingBase.PostProgressAction" />. Raises <see cref="CanExecuteChanged" />.
        /// </summary>
        protected virtual void OnPostProgress()
        {
            RaiseCanExecuteChanged();
        }

        /// <summary>
        ///     Unless overriden in derived class, always returns true;
        /// </summary>
        /// <returns>True unless overriden.</returns>
        protected virtual bool CanExecuteCore()
        {
            return true;
        }

        /// <summary>
        ///     Invoked when exception is thrown in <see cref="ExecuteCore" /> and <see cref="UseHandleExecutionException" /> is
        ///     true. This implementation is empty.
        /// </summary>
        /// <param name="e">Exception thrown.</param>
        protected virtual void HandleExecutionException(Exception e)
        {
        }

        /// <summary>
        ///     Invoked in <see cref="Execute()" /> in <code>using (progress)</code> block.
        /// </summary>
        protected abstract void ExecuteCore();

        /// <summary>
        ///     Raises <see cref="CanExecuteChanged" />.
        /// </summary>
        protected void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    ///     Typed base class for <see cref="ICommand" /> implementations. Executable only
    ///     for null parameters or non-null parameters of <typeparamref name="TParameter" /> type.
    ///     If incompatible type is passed, <see cref="ArgumentException" /> will be
    ///     thrown.
    /// </summary>
    /// <typeparam name="TParameter">Expected type of command parameter.</typeparam>
    public abstract class ProgressingCommandBase<TParameter> : ProgressReportingBase, IProgressingCommand<TParameter>
    {
        protected ProgressingCommandBase()
        {
            PreProgressAction = OnPreProgress;
            PostProgressAction = OnPostProgress;
        }

        /// <summary>
        ///     Gets or sets whether <see cref="CanExecute(TParameter)" /> should return false while
        ///     <see cref="Execute(TParameter)" /> is executed.
        /// </summary>
        protected bool IsExecutionBlocking { get; set; } = true;

        /// <summary>
        ///     Gets or sets whether an exception thrown in <see cref="ExecuteCore" /> will be rethrown (directly, using 'throw;'
        ///     clause). True by default. Independent from <see cref="UseHandleExecutionException" />.
        /// </summary>
        protected bool RethrowExecutionException { get; set; } = true;

        /// <summary>
        ///     Gets or sets whether an exception thrown in <see cref="ExecuteCore" /> will be passed to
        ///     <see cref="HandleExecutionException" />. False by default. Independent from
        ///     <see cref="RethrowExecutionException" />.
        /// </summary>
        protected bool UseHandleExecutionException { get; set; } = false;

        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Checks if <see cref="IsExecutionBlocking" /> is true and <see cref="Execute(TParameter)" /> is executed, otherwise,
        ///     invokes
        ///     <see cref="CanExecuteCore" />.
        /// </summary>
        /// <returns>
        ///     If <see cref="IsExecutionBlocking" /> is true and <see cref="Execute(TParameter)" /> is executed, returns false.
        ///     Otherwise,
        ///     result of <see cref="CanExecuteCore" /> is returned.
        /// </returns>
        public bool CanExecute(TParameter parameter)
        {
            return (!IsExecutionBlocking || !IsInProgress) && CanExecuteCore(parameter);
        }

        /// <summary>
        ///     If <paramref name="parameter" /> is not null and not of type
        ///     <typeparamref name="TParameter" />, false is returned. Otherwise,
        ///     <see cref="CanExecute(TParameter)" /> is invoked.
        /// </summary>
        /// <param name="parameter">Checked for type compatibility.</param>
        /// <returns>False or result of <see cref="CanExecute(TParameter)" />.</returns>
        public bool CanExecute(object parameter)
        {
            if (parameter == null || parameter is TParameter)
            {
                return CanExecute((TParameter) parameter);
            }
            return false;
        }

        /// <summary>
        ///     If <paramref name="parameter" /> is not null and not of type
        ///     <typeparamref name="TParameter" />, exception is thrown. Otherwise,
        ///     <see cref="Execute(TParameter)" /> is invoked.
        /// </summary>
        /// <param name="parameter">Checked for type compatibility.</param>
        /// <exception cref="ArgumentException">
        ///     If <paramref name="parameter" /> is not null and not of type
        ///     <typeparamref name="TParameter" />.
        /// </exception>
        public void Execute(object parameter)
        {
            if (parameter == null || parameter is TParameter)
            {
                Execute((TParameter) parameter);
            }
            else
            {
                throw new ArgumentException($"{GetType().FullName}: Expected parameter" +
                                            $" of type '{typeof(TParameter).FullName}'," +
                                            $" but received '{parameter.GetType().FullName}'.", nameof(parameter));
            }
        }

        /// <summary>
        ///     Invokes <see cref="ExecuteCore(TParameter)" /> in <code>using (progress)</code> block.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        public virtual void Execute(TParameter parameter)
        {
            try
            {
                using (BeginProgress())
                {
                    ExecuteCore(parameter);
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
        ///     By default set as <see cref="ProgressReportingBase.PreProgressAction" />. Raises <see cref="CanExecuteChanged" />.
        /// </summary>
        protected virtual void OnPreProgress()
        {
            RaiseCanExecuteChanged();
        }

        /// <summary>
        ///     By default set as <see cref="ProgressReportingBase.PostProgressAction" />. Raises <see cref="CanExecuteChanged" />.
        /// </summary>
        protected virtual void OnPostProgress()
        {
            RaiseCanExecuteChanged();
        }

        /// <summary>
        ///     Unless overriden in derived class, always returns true;
        /// </summary>
        /// <param name="parameter">Ignored unless overriden.</param>
        /// <returns>True unless overriden.</returns>
        protected virtual bool CanExecuteCore(TParameter parameter)
        {
            return true;
        }

        /// <summary>
        ///     Invoked when exception is thrown in <see cref="ExecuteCore" /> and <see cref="UseHandleExecutionException" /> is
        ///     true. This implementation is empty.
        /// </summary>
        /// <param name="e">Exception thrown.</param>
        protected virtual void HandleExecutionException(Exception e)
        {
        }

        /// <summary>
        ///     Invoked in <see cref="Execute(TParameter)" /> in <code>using (progress)</code> block.
        /// </summary>
        /// <param name="parameter"></param>
        protected abstract void ExecuteCore(TParameter parameter);

        /// <summary>
        ///     Raises <see cref="CanExecuteChanged" />.
        /// </summary>
        protected void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
