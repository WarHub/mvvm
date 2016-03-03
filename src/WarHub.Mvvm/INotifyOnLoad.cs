// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Mvvm
{
    using System;

    public interface INotifyOnLoad<TArgs> where TArgs : EventArgs
    {
        event EventHandler<TArgs> Loading;

        event EventHandler<TArgs> Loaded;
    }
}
