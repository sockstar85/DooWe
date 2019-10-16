using Diot.Interface;
using Diot.Interface.ViewModels;
using Diot.Services;
using Diot.ViewModels;
using Diot.Views.Extensions;
using Diot.Views.Pages;
using DryIoc;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Diot
{
    public partial class App
    {
        #region Properties

        /// <summary>
        ///     Gets the application container.
        /// </summary>
        public static IContainer AppContainer { get; private set; }

        #endregion

        #region Methods

        #region Constructors

        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="App" /> class.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        #endregion

        /// <summary>
        ///     Run the bootstrapper process.
        /// </summary>
        public override void Initialize()
        {
            InitializeComponent();

            base.Initialize();

            ResourceManager.Init(Container.Resolve<IResourceManager>());
            TranslateExtension.Init(new ResourceManager());
        }


        /// <summary>
        ///     Called when the PrismApplication has completed it's initialization process.
        /// </summary>
        protected override async void OnInitialized()
        {
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        /// <summary>
        ///     Used to register types with the container that will be used by your application.
        /// </summary>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            #region Navigation

            containerRegistry.Register<IExtendedNavigation, ExtendedNavigation>();

            #endregion

            #region Interfaces

            containerRegistry.Register<IResourceManager, ResourceManager>();

            #endregion

            #region Pages

            containerRegistry.RegisterForNavigation<AddNewPage, IAddNewPageViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, IMainPageViewModel>();
            containerRegistry.RegisterForNavigation<MovieDetailsPage, IMovieDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<NavigationPage>();

            #endregion

            #region ViewModels

            containerRegistry.Register<IViewModelBase, ViewModelBase>();
            containerRegistry.Register<IAddNewPageViewModel, AddNewPageViewModel>();
            containerRegistry.Register<IMainPageViewModel, MainPageViewModel>();
            containerRegistry.Register<IMovieDetailsPageViewModel, MovieDetailsPageViewModel>();
            containerRegistry.Register<ISelectableMovieViewModel, SelectableMovieViewModel>();

            #endregion

            #region Required Types

            containerRegistry.RegisterSingleton<IDatabaseService, DatabaseService>();

            #endregion

            AppContainer = containerRegistry.GetContainer();
        }

        #endregion
    }
}