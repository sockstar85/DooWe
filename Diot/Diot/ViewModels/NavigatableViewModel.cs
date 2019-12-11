using Diot.Interface;
using Diot.Interface.ViewModels;
using Prism.AppModel;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Threading.Tasks;

namespace Diot.ViewModels
{
	public abstract class NavigatableViewModel : ViewModelBase, INavigatableViewModel, IApplicationLifecycleAware
	{
		#region Properties

		/// <summary>
		///	 Gets the navigation service.
		/// </summary>
		public IExtendedNavigation NavigationService { get; }

		/// <summary>
		///		Gets the dialog service.
		/// </summary>
		public IPageDialogService DialogService { get; }

		/// <summary>
		///		Gets or sets the title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///		Gets the loading page service.
		/// </summary>
		public ILoadingPageService LoadingPageService { get; }

		#endregion

		#region Methods

		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="NavigatableViewModel"/> class.
		/// </summary>
		/// <param name="navigationService">The navigation service.</param>
		/// <param name="pageDialogService">The page dialog service.</param>
		/// <param name="loadingPageService">The loading page service.</param>
		/// <exception cref="ArgumentNullException">
		/// loadingPageService
		/// or
		/// navigationService
		/// or
		/// pageDialogService
		/// </exception>
		public NavigatableViewModel(IExtendedNavigation navigationService,
			IPageDialogService pageDialogService,
			ILoadingPageService loadingPageService) 
			: base()
		{
			LoadingPageService = loadingPageService ?? throw new ArgumentNullException(nameof(loadingPageService));
			NavigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
			DialogService = pageDialogService ?? throw new ArgumentNullException(nameof(pageDialogService));
		}

		#endregion

		/// <summary>
		///		Destroys this instance.
		/// </summary>
		public virtual void Destroy()
		{
		}

		/// <summary>
		///		Initializes the specified parameters.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		public virtual void Initialize(INavigationParameters parameters)
		{
		}

		/// <summary>
		///		Initializes the asynchronous.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		public virtual async Task InitializeAsync(INavigationParameters parameters)
		{
			await Task.CompletedTask;
		}

		/// <summary>
		///		Called when the implementer has been navigated away from.
		/// </summary>
		/// <param name="parameters">The navigation parameters.</param>
		public virtual void OnNavigatedFrom(INavigationParameters parameters)
		{
		}

		/// <summary>
		///		Called when the implementer has been navigated to.
		/// </summary>
		/// <param name="parameters">The navigation parameters.</param>
		public virtual void OnNavigatedTo(INavigationParameters parameters)
		{
		}

		/// <summary>
		///		Called when [resume].
		/// </summary>
		public virtual void OnResume()
		{
		}

		/// <summary>
		///		Called when [sleep].
		/// </summary>
		public virtual void OnSleep()
		{
		}

		#endregion
	}
}
