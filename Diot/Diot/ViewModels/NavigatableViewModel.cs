using Diot.Interface;
using Diot.Interface.Manager;
using Diot.Interface.ViewModels;
using Prism.AppModel;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Diot.ViewModels
{
	public abstract class NavigatableViewModel : ViewModelBase, INavigatableViewModel, IApplicationLifecycleAware
	{
		#region Fields

		private bool _hasNetworkConnection = true;

		#endregion

		#region Properties

		/// <summary>
		///	 Gets the navigation service.
		/// </summary>
		public IExtendedNavigation NavigationService { get; }

		/// <summary>
		///		Gets the dialog service.
		/// </summary>
		public IPageDialogService PageDialogService { get; }

		/// <summary>
		///		Gets or sets the title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///		Gets the loading page service.
		/// </summary>
		public ILoadingPageService LoadingPageService { get; }

		/// <summary>
		///		Gets or sets a value indicating whether there is a network connection.
		/// </summary>
		public bool HasNetworkConnection
		{
			get => _hasNetworkConnection;
			set => SetProperty(ref _hasNetworkConnection, value);
		}

		/// <summary>
		///		Gets the connectivity manager.
		/// </summary>
		protected IConnectivityManager ConnectivityManager { get; }

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
			ILoadingPageService loadingPageService,
			IConnectivityManager connectivityManager) 
			: base()
		{
			LoadingPageService = loadingPageService ?? throw new ArgumentNullException(nameof(loadingPageService));
			NavigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
			PageDialogService = pageDialogService ?? throw new ArgumentNullException(nameof(pageDialogService));
			ConnectivityManager = connectivityManager ?? throw new ArgumentNullException(nameof(connectivityManager));

			_hasNetworkConnection = ConnectivityManager.HasNetworkConnection;

			ConnectivityManager.ConnectivityChanged += updateHasConnectivity;
		}		

		#endregion

		/// <summary>
		///		Destroys this instance.
		/// </summary>
		public virtual void Destroy()
		{
			ConnectivityManager.ConnectivityChanged -= updateHasConnectivity;
		}

		/// <summary>
		///		Updates the has connectivity flag.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="ConnectivityChangedEventArgs"/> instance containing the event data.</param>
		private void updateHasConnectivity(object sender, ConnectivityChangedEventArgs e)
		{
			HasNetworkConnection = ConnectivityManager.HasNetworkConnection;
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
