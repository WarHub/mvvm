// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Mvvm
{
    using System;

    /// <summary>
    ///     Event args containing single strongly typed parameter.
    /// </summary>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
    public class ParametrizedEventArgs<TParameter> : EventArgs
    {
        /// <summary>
        ///     Initializes new instance of <see cref="ParametrizedEventArgs{TParameter}" />.
        /// </summary>
        /// <param name="parameter">Set to be <see cref="Parameter" /> value.</param>
        public ParametrizedEventArgs(TParameter parameter)
        {
            Parameter = parameter;
        }

        /// <summary>
        ///     Gets strongly typed event parameter.
        /// </summary>
        public TParameter Parameter { get; }
    }
}
