using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diot.Helpers;
using Diot.Interface;
using Diot.Interface.Manager;
using Diot.Models;
using Moq;
using Prism.Navigation;
using Prism.Services;
using Xunit;

namespace Diot.ViewModels.UnitTests
{
	public class MainPageViewModelUnitTests
	{
		#region Fields

		private readonly Mock<IExtendedNavigation> _navigationService = new Mock<IExtendedNavigation>();
		public static IEnumerable<object[]> ConstructorArguments = getConstructorArguments();
		private readonly List<MovieDbModel> _moviesList = new List<MovieDbModel>
		{
			new MovieDbModel
			{
				Title = "Avengers : Endgame"
			},
			new MovieDbModel
			{
				Title = "Avengers : Age of Ultron"
			},
			new MovieDbModel
			{
				Title = "The Iron Giant"
			}
		};

		#endregion

		#region Helper Methods

		private MainPageViewModel createViewModel(
			Mock<IExtendedNavigation> navigationService = null,
			Mock<IPageDialogService> pageDialogService = null,
			Mock<ILoadingPageService> loadingPageService = null,
			Mock<IDatabaseService> databaseService = null,
			Mock<IResourceManager> resourceManager = null,
			Mock<IHttpClientService> httpClientService = null,
			Mock<IConnectivityManager> connectivityManager = null)
		{

			if (connectivityManager == null)
			{
				connectivityManager = new Mock<IConnectivityManager>();
				connectivityManager.Setup(x => x.HasNetworkConnection).Returns(true);
			}

			if (navigationService == null)
			{
				navigationService = new Mock<IExtendedNavigation>();
				navigationService.Setup(x => x.NavigateAsync(It.IsAny<string>(), It.IsAny<INavigationParameters>(), It.IsAny<bool>(), It.IsAny<bool>()))
					.ReturnsAsync(
					new NavigationResult
					{
						Success = true
					});
			}

			return new MainPageViewModel(
				navigationService?.Object ?? new Mock<IExtendedNavigation>().Object,
				pageDialogService?.Object ?? new Mock<IPageDialogService>().Object,
				loadingPageService?.Object ?? new Mock<ILoadingPageService>().Object,
				databaseService?.Object ?? new Mock<IDatabaseService>().Object,
				resourceManager?.Object ?? new Mock<IResourceManager>().Object,
				httpClientService?.Object ?? new Mock<IHttpClientService>().Object,
				connectivityManager?.Object ?? new Mock<IConnectivityManager>().Object);
		}

		private static IEnumerable<object[]> getConstructorArguments()
		{
			var parametersInfo = typeof(MainPageViewModel).GetConstructors().First().GetParameters();

			var constructorArguments =  parametersInfo.Select(x =>
			{
				var mockObjectType = typeof(Mock<>);
				object mockObject = Activator.CreateInstance(mockObjectType.MakeGenericType(x.ParameterType));
				return ((Mock)mockObject).Object;
			}).ToArray();

			for (int i = 0; i < constructorArguments.Length; i++)
			{
				var argumentList = new List<object>(constructorArguments);

				// Every iteration sets one of the parameters to null and add the name of this parameter in the list as the last item.
				argumentList[i] = null;
				var nullArgumentName = parametersInfo[i].Name;
				argumentList.Add(nullArgumentName);

				yield return argumentList.ToArray();
			}
		}

		#endregion

		#region Tests

		[Fact]
		public void Constructor_Constructs()
		{
			#region Arrange

			var viewModel = new MainPageViewModel(
				new Mock<IExtendedNavigation>().Object,
				new Mock<IPageDialogService>().Object,
				new Mock<ILoadingPageService>().Object,
				new Mock<IDatabaseService>().Object,
				new Mock<IResourceManager>().Object,
				new Mock<IHttpClientService>().Object,
				new Mock<IConnectivityManager>().Object);

			#endregion

			#region Act

			#endregion

			#region Assert

			Assert.NotNull(viewModel);

			#endregion
		}

		[Theory]
		[MemberData(nameof(ConstructorArguments))]
		public void Constructor_ThrowsArgumentNullException_WhenGivenNullParams(
			IExtendedNavigation navigationService,
			IPageDialogService pageDialogService,
			ILoadingPageService loadingPageService,
			IDatabaseService databaseService,
			IResourceManager resourceManager,
			IHttpClientService httpClientService,
			IConnectivityManager connectivityManager,
			string nullArgName)
		{
			#region Arrange

			#endregion

			#region Act

			var exception = Assert.Throws<ArgumentNullException>(() => new MainPageViewModel(
				navigationService,
				pageDialogService,
				loadingPageService,
				databaseService,
				resourceManager,
				httpClientService,
				connectivityManager));

			#endregion

			#region Assert

			Assert.Equal(nullArgName, exception.ParamName, StringComparer.OrdinalIgnoreCase);

			#endregion
		}

		[Fact]
		public void NavigateToNewPageCommand_NavigatesToNewPage()
		{
			#region Arrange

			var navigationService = new Mock<IExtendedNavigation>();
			navigationService = new Mock<IExtendedNavigation>();
			navigationService.Setup(x => x.NavigateAsync(It.IsAny<string>(), It.IsAny<INavigationParameters>(), It.IsAny<bool>(), It.IsAny<bool>()))
				.ReturnsAsync(
				new NavigationResult
				{
					Success = true
				});

			var viewModel = createViewModel(navigationService);

			#endregion

			#region Act

			viewModel.NavigateToAddNewPageCommand.Execute(null);

			#endregion

			#region Assert

			navigationService.Verify(x => x.NavigateAsync(PageNames.AddNewPage, It.IsAny<INavigationParameters>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);

			#endregion
		}

		[Fact]
		public void NavigateToAddNewPageCommand_HandlesNoNetworkConnection()
		{
			#region Arrange

			_navigationService.Setup(x => x.NavigateAsync(It.IsAny<string>(), It.IsAny<INavigationParameters>(), It.IsAny<bool>(), It.IsAny<bool>()))
				.ReturnsAsync(
				new NavigationResult
				{
					Success = true
				});

			var pageDialogService = new Mock<IPageDialogService>();			

			var viewModel = createViewModel(_navigationService, pageDialogService: pageDialogService);

			#endregion

			#region Act

			viewModel.HasNetworkConnection = false;
			viewModel.NavigateToAddNewPageCommand.Execute(null);

			#endregion

			#region Assert

			_navigationService.Verify(x => x.NavigateAsync(PageNames.AddNewPage, It.IsAny<INavigationParameters>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Never);
			pageDialogService.Verify(x => x.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

			#endregion
		}

		[Fact]
		public void NavigateToNewPageCommand_ClosesLoadingPage()
		{
			#region Arrange

			var navigationService = new Mock<IExtendedNavigation>();
			navigationService = new Mock<IExtendedNavigation>();
			navigationService.Setup(x => x.NavigateAsync(It.IsAny<string>(), It.IsAny<INavigationParameters>(), It.IsAny<bool>(), It.IsAny<bool>()))
				.ReturnsAsync(
				new NavigationResult
				{
					Success = true
				});

			var loadingPageService = new Mock<ILoadingPageService>();

			var viewModel = createViewModel(navigationService, loadingPageService: loadingPageService);

			#endregion

			#region Act

			viewModel.NavigateToAddNewPageCommand.Execute(null);

			#endregion

			#region Assert

			loadingPageService.Verify(x => x.HideLoadingPageAsync(It.IsAny<int>(), It.IsAny<string>()), Times.AtLeastOnce);

			#endregion
		}

		[Fact]
		public void NavigateToNewPageCommand_HandlesFailedNavigation()
		{
			#region Arrange

			var navigationService = new Mock<IExtendedNavigation>();
			navigationService.Setup(x => x.NavigateAsync(It.IsAny<string>(), It.IsAny<INavigationParameters>(), It.IsAny<bool>(), It.IsAny<bool>()))
				.ReturnsAsync(new NavigationResult { Exception = new Exception("Mock exception"), Success = false });

			var pageDialogService = new Mock<IPageDialogService>();

			var viewModel = createViewModel(navigationService, pageDialogService: pageDialogService);

			#endregion

			#region Act

			viewModel.NavigateToAddNewPageCommand.Execute(null);

			#endregion

			#region Assert

			navigationService.Verify(x => x.NavigateAsync(PageNames.AddNewPage, It.IsAny<INavigationParameters>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
			pageDialogService.Verify(x => x.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

			#endregion
		}

		[Fact]
		public void NavigateToMovieDetailsCommand_NavigatesToNewPage()
		{
			#region Arrange

			var navigationService = new Mock<IExtendedNavigation>();
			navigationService.Setup(x => x.NavigateAsync(It.IsAny<string>(), It.IsAny<INavigationParameters>(), It.IsAny<bool>(), It.IsAny<bool>()))
				.ReturnsAsync(
				new NavigationResult
				{
					Success = true
				});

			var viewModel = createViewModel(navigationService);

			#endregion

			#region Act

			viewModel.NavigateToMovieDetailsCommand.Execute(new MovieDbModel());

			#endregion

			#region Assert

			navigationService.Verify(x => x.NavigateAsync(PageNames.MovieDetailsPage, It.IsAny<INavigationParameters>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);

			#endregion
		}

		[Fact]
		public void SearchCommand_ReturnsCorrectValues()
		{
			#region Arrange

			var viewModel = createViewModel();

			viewModel.MoviesList = _moviesList;

			var searchCriteria = "Avengers";
			var expectedValues = new List<MovieDbModel>
			{
				_moviesList[0],
				_moviesList[1]
			};

			#endregion

			#region Act

			viewModel.TitleSearch = searchCriteria;

			#endregion

			#region Assert

			Assert.NotNull(viewModel.SortedMoviesList);
			Assert.Equal(expectedValues.Count, viewModel.SortedMoviesList.Count);
			Assert.Equal(expectedValues, viewModel.SortedMoviesList);

			#endregion
		}

		[Fact]
		public void NavigateToMovieDetailsCommand_ClosesLoadingPage()
		{
			#region Arrange

			var loadingPageService = new Mock<ILoadingPageService>();
			var viewModel = createViewModel(loadingPageService: loadingPageService);

			#endregion

			#region Act

			viewModel.NavigateToAddNewPageCommand.Execute(new MovieCreditsModel());

			#endregion

			#region Assert

			loadingPageService.Verify(x => x.HideLoadingPageAsync(It.IsAny<int>(), It.IsAny<string>()), Times.AtLeastOnce);

			#endregion
		}

		[Fact]
		public void NavigateToMovieDetailsPage_HandlesFailedNavigationCorrectly()
		{
			#region Arrange

			var pageDialogService = new Mock<IPageDialogService>();
			var loadingPageService = new Mock<ILoadingPageService>();
			var navigationService = new Mock<IExtendedNavigation>();
			navigationService.Setup(x => x.NavigateAsync(PageNames.AddNewPage, It.IsAny<INavigationParameters>(),
					It.IsAny<bool>(), It.IsAny<bool>()))
				.ReturnsAsync(new NavigationResult
				{
					Success = false,
					Exception = new Exception("Test exception")
				});

			var viewModel = createViewModel(
				pageDialogService: pageDialogService, 
				navigationService: navigationService,
				loadingPageService: loadingPageService);

			#endregion

			#region Act

			viewModel.NavigateToAddNewPageCommand.Execute(new MovieDbModel());

			#endregion

			#region Assert

			loadingPageService.Verify(x => x.HideLoadingPageAsync(It.IsAny<int>(), It.IsAny<string>()), Times.AtLeastOnce);
			pageDialogService.Verify(x => x.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

			#endregion
		}

		[Fact]
		public async Task InitializeAsync_InitializesCorrectly()
		{
			#region Arrange

			var expectedMovies = getMovieList();
			var databaseService = new Mock<IDatabaseService>();

			databaseService.Setup(x => x.GetAllMovies()).Returns(expectedMovies);

			var viewModel = createViewModel(databaseService: databaseService);

			#endregion

			#region Act

			await viewModel.InitializeAsync(new NavigationParameters());

			#endregion

			#region Assert

			Assert.Equal(expectedMovies.Count, viewModel.MoviesList.Count);
			Assert.Equal(expectedMovies, viewModel.MoviesList);
			Assert.Equal(expectedMovies.Count, viewModel.SortedMoviesList.Count);

			#endregion
		}
		
		private IList<MovieDbModel> getMovieList()
		{
			return new List<MovieDbModel>
			{
				new MovieDbModel
				{
					Id = 1,
					Title = "Movie1"
				},
				new MovieDbModel
				{
					Id = 2,
					Title = "Movie2"
				},
				new MovieDbModel
				{
					Id = 3,
					Title = "Movie3"
				}
			};
		}


		//TODO: Cover images get set to default icon if there isn't an image stream

		#endregion
	}
}
