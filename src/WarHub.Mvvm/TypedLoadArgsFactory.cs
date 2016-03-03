// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Mvvm
{
    using System;

    public abstract class TypedLoadArgsFactory<TModel, TUnloadArgs> :
        ILoadArgsFactory<ParametrizedEventArgs<TModel>, TUnloadArgs>
        where TUnloadArgs : EventArgs
    {
        public ParametrizedEventArgs<TModel> CreateLoadArgs(object navigationParameter)
        {
            try
            {
                return new ParametrizedEventArgs<TModel>((TModel) navigationParameter);
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException($"Invalid type: received '{navigationParameter?.GetType() ?? null}'," +
                                            $" expected '{typeof(TModel)}'.", nameof(navigationParameter));
            }
        }

        public abstract TUnloadArgs CreateUnloadArgs();
    }

    public class TypedLoadArgsFactory<TModel> : TypedLoadArgsFactory<TModel, EventArgs>
    {
        public override EventArgs CreateUnloadArgs() => EventArgs.Empty;
    }
}
