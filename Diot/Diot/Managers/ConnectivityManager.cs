using Diot.Interface.Manager;
using System;
using Xamarin.Essentials;

namespace Diot.Managers
{
	public class ConnectivityManager : IConnectivityManager
	{
		#region Properties

		/// <summary>
		///		Gets a value indicating whether there is a network connection.
		/// </summary>
		public bool HasNetworkConnection => Connectivity.NetworkAccess != NetworkAccess.None;

		#endregion

		#region Events

		/// <summary>
		///		Occurs when connectivity changed.
		/// </summary>
		public event EventHandler<ConnectivityChangedEventArgs> ConnectivityChanged
		{
			add => Connectivity.ConnectivityChanged += value;
			remove => Connectivity.ConnectivityChanged -= value;
		}

		#endregion
	}
}
