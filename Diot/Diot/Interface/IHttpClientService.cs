using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Diot.Interface
{
	public interface IHttpClientService
	{
		/// <summary>
		///     Gets the jObject asynchronous.
		/// </summary>
		Task<JObject> GetJObjectAsync(string url);

		// <summary>
		///     Gets the image byte array asynchronous.
		/// </summary>
		Task<byte[]> GetImageByteArrayAsync(string url);

		/// <summary>
		///     Gets the json string asynchronous.
		/// </summary>
		Task<string> GetStringAsync(string url);
	}
}
