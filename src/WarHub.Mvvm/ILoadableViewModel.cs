// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Mvvm
{
    using System.ComponentModel;

    public interface ILoadableViewModel : INotifyPropertyChanged
    {
        bool IsLoaded { get; }

        void Load(object parameter);
        void Unload();
    }
}
