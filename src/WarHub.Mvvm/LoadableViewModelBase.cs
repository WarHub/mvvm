// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Mvvm
{
    using System;
    using System.Threading;

    /// <summary>
    ///     Base class for contextual ViewModels.
    /// </summary>
    /// <typeparam name="TLoadArgs">Type of context's load args.</typeparam>
    /// <typeparam name="TUnloadArgs">Type of context's unload args.</typeparam>
    public abstract class LoadableViewModelBase<TLoadArgs, TUnloadArgs> : ViewModelBase,
        INotifyingLoadableViewModel<TLoadArgs, TUnloadArgs>
        where TLoadArgs : EventArgs where TUnloadArgs : EventArgs
    {
        private int _loadCount;

        public abstract ILoadArgsFactory<TLoadArgs, TUnloadArgs> LoadArgFactory { get; }

        public bool IsLoaded => _loadCount > 0;

        public event EventHandler<TLoadArgs> Loaded;

        public event EventHandler<TLoadArgs> Loading;

        public event EventHandler<TUnloadArgs> Unloaded;

        public event EventHandler<TUnloadArgs> Unloading;

        public void Load(object parameter)
        {
            if (Interlocked.Exchange(ref _loadCount, 1) == 1)
                return;
            var loadArgs = LoadArgFactory.CreateLoadArgs(parameter);
            Loading?.Invoke(this, loadArgs);
            Loaded?.Invoke(this, loadArgs);
        }

        public void Unload()
        {
            if (Interlocked.Exchange(ref _loadCount, 0) == 0)
                return;
            var unloadArgs = LoadArgFactory.CreateUnloadArgs();
            Unloading?.Invoke(this, unloadArgs);
            Unloaded?.Invoke(this, unloadArgs);
        }
    }

    public abstract class LoadableViewModelBase : LoadableViewModelBase<EventArgs, EventArgs>
    {
        public override ILoadArgsFactory<EventArgs, EventArgs> LoadArgFactory { get; } = new DefaultLoadArgsFactory();
    }
}
