// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Mvvm
{
    using System;

    public class DefaultLoadArgsFactory : ILoadArgsFactory<EventArgs, EventArgs>
    {
        public EventArgs CreateLoadArgs(object navigationParameter)
        {
            return EventArgs.Empty;
        }

        public EventArgs CreateUnloadArgs()
        {
            return EventArgs.Empty;
        }
    }
}
