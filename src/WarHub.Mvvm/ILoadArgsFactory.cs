// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Mvvm
{
    using System;

    /// <summary>
    ///     Describes context of loadable object, which can prepare load/unload event args accordingly to context and given
    ///     parameter.
    /// </summary>
    /// <typeparam name="TLoadArgs"></typeparam>
    /// <typeparam name="TUnloadArgs"></typeparam>
    public interface ILoadArgsFactory<TLoadArgs, TUnloadArgs> where TLoadArgs : EventArgs where TUnloadArgs : EventArgs
    {
        /// <summary>
        ///     Prepares load args for use using provided navigation parameter.
        /// </summary>
        /// <param name="navigationParameter">Used to prepare valid load args.</param>
        /// <returns>Prepared load args.</returns>
        TLoadArgs CreateLoadArgs(object navigationParameter);

        /// <summary>
        ///     Prepares unload args.
        /// </summary>
        /// <returns>Prepared unload args.</returns>
        TUnloadArgs CreateUnloadArgs();
    }
}
