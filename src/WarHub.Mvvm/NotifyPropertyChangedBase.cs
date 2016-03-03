// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Mvvm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///     Base class for all objects which want implement <see cref="INotifyPropertyChanged" /> easily.
    ///     Provides <see cref="Set{T}(ref T, T, string)" /> method which takes care of invoking the event when property has
    ///     new value.
    /// </summary>
    public class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     If the new <paramref name="value" /> is different than the one currently held in <paramref name="field" />,
        ///     it is set, <see cref="PropertyChanged" /> is null-safely invoked and true is returned.
        /// </summary>
        /// <typeparam name="T">Type of property.</typeparam>
        /// <param name="field">Backing field of property whose value is being set.</param>
        /// <param name="value">New value to be set.</param>
        /// <param name="propertyName">Name of property whose value is being set.</param>
        /// <returns>True if set, false if both values are equal.</returns>
        protected bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }
            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        ///     If <paramref name="equals" /> returns true, nothing happens and false is returned. Otherwise,
        ///     <paramref name="assign" /> is invoked, <see cref="PropertyChanged" /> is null-safely invoked and true is returned.
        /// </summary>
        /// <param name="equals">Equality comparison to avoid assignment of equal value.</param>
        /// <param name="assign">Assignment delegate.</param>
        /// <param name="propertyName">Name of property whose value is being set.</param>
        /// <returns>True if set, false if both values are equal.</returns>
        protected bool Set(Func<bool> @equals, Action assign, [CallerMemberName] string propertyName = null)
        {
            if (@equals())
            {
                return false;
            }
            assign();
            RaisePropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        ///     Raises <see cref="PropertyChanged" /> event using provided name of property.
        /// </summary>
        /// <param name="propertyName">Name of changed property to put in event data.</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Raises <see cref="PropertyChanged" /> with the same property name as the one in args.
        ///     Use to forward change of (possibly hidden) source property of the same name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ForwardAsSelfChange(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
        }
    }
}
