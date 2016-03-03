// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Mvvm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    ///     Base class for view models which use context parameter when loading into view. The base class provides additional
    ///     <see cref="Model" /> protected property, which may be automatically assigned with
    ///     <see cref="INotifyOnLoad{TArgs}.Loading" /> event parameter in event handler. Additionally the
    ///     <see cref="ModelChanged" /> event is available, which is raised when <see cref="Model" /> changes it's value.
    /// </summary>
    /// <remarks>
    ///     As it is generally considered bad practice to relay on order of event handlers, please remember to not use
    ///     <see cref="Model" /> property in <see cref="INotifyOnLoad{TArgs}.Loading" /> event handlers, as it may have not
    ///     been set. Please use rather <see cref="INotifyOnLoad{TArgs}.Loaded" /> or if it's more appropriate
    ///     <see cref="ModelChanged" /> for that purpose.
    /// </remarks>
    /// <typeparam name="TModel">Type of <see cref="Model" /> property.</typeparam>
    public class ContextualViewModelBase<TModel> : LoadableViewModelBase<ParametrizedEventArgs<TModel>, EventArgs>
    {
        private TModel _model;

        /// <summary>
        ///     Creates new instance which automatically assigns <see cref="INotifyOnLoad{TArgs}.Loading" /> event
        ///     parameter to <see cref="Model" /> property in event handler. If <paramref name="autoAssignOnLoad" /> is false,
        ///     assignment won't happen.
        /// </summary>
        public ContextualViewModelBase(bool autoAssignOnLoad = true)
        {
            if (autoAssignOnLoad)
            {
                Loading += (s, e) => Model = e.Parameter;
            }
            ModelChanged += (s, e) => RaiseModelDependentPropertiesChanged();
        }


        public override ILoadArgsFactory<ParametrizedEventArgs<TModel>, EventArgs> LoadArgFactory { get; } =
            new TypedLoadArgsFactory<TModel>();

        /// <summary>
        ///     Gets or sets the Model of this ViewModel. Changes of this property's value are broadcast using
        ///     <see cref="ModelChanged" /> event.
        /// </summary>
        /// <exception cref="ArgumentNullException">When trying to set to null.</exception>
        protected TModel Model
        {
            get { return _model; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(Model));
                if (Set(ref _model, value))
                {
                    ModelChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets a list of property names for which <see cref="INotifyPropertyChanged.PropertyChanged" /> should be
        ///     raised during <see cref="ModelChanged" />
        /// </summary>
        protected List<string> ModelDependentPropertyNames { get; } = new List<string>();

        /// <summary>
        ///     Informs that a change of value of <see cref="Model" /> property occured.
        /// </summary>
        protected event EventHandler ModelChanged;

        /// <summary>
        ///     Raises <see cref="INotifyPropertyChanged.PropertyChanged" /> for each property name in
        ///     <see cref="ModelDependentPropertyNames" />.
        /// </summary>
        private void RaiseModelDependentPropertiesChanged()
        {
            foreach (var propertyName in ModelDependentPropertyNames)
            {
                RaisePropertyChanged(propertyName);
            }
        }
    }
}
