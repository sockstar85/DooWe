using Diot.Interface.ViewModels;
using Prism.Mvvm;
using Prism.Navigation;

namespace Diot.ViewModels
{
    /// <summary>
    ///     The base to all view models.
    /// </summary>
    /// <seealso cref="BindableBase" />
    /// <seealso cref="INavigationAware" />
    /// <seealso cref="IDestructible" />
    public class ViewModelBase : BindableBase, IViewModelBase
    {
	}
}