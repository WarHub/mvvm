// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Mvvm
{
    using System;

    public interface INotifyingLoadableViewModel<TLoadArgs, TUnloadArgs> : ILoadableViewModel,
        INotifyOnLoad<TLoadArgs>, INotifyOnUnload<TUnloadArgs>
        where TLoadArgs : EventArgs where TUnloadArgs : EventArgs
    {
    }
}
