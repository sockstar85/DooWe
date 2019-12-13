using System;
using Xamarin.Essentials;

namespace Diot.Interface.Manager
{
	public interface IConnectivityManager
	{
		#region Properties

		/// <summary>
		///		Gets a value indicating whether there is a network connection.
		/// </summary>
		bool HasNetworkConnection { get; }

		#endregion

		#region Events

		/// <summary>
		///		Occurs when connectivity changed.
		/// </summary>
		event EventHandler<ConnectivityChangedEventArgs> ConnectivityChanged;

		#endregion
	}
}
