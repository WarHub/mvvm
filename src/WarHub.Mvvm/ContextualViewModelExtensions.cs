// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Mvvm
{
    public static class ContextualViewModelExtensions
    {
        public static T WithModel<T, TModel>(this T viewModel, TModel model) where T : ContextualViewModelBase<TModel>
        {
            viewModel.Unload();
            viewModel.Load(model);
            return viewModel;
        }
    }
}
