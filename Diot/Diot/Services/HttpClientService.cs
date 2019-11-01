using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Diot.Interface;
using Newtonsoft.Json.Linq;

namespace Diot.Services
{
    public class HttpClientService : IHttpClientService
	{
        #region  Fields

        private readonly HttpClient client;

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpClientService" /> class.
        /// </summary>
        public HttpClientService()
        {
			client = new HttpClient();// {MaxResponseContentBufferSize = 256000};
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion

        /// <summary>
        ///     Gets the jObject asynchronous.
        /// </summary>
        public async Task<JObject> GetJObjectAsync(string url)
        {
            var results = await client.GetStringAsync(url);
            return JObject.Parse(results);
        }

        /// <summary>
        ///     Gets the image byte array asynchronous.
        /// </summary>
        public async Task<byte[]> GetImageByteArrayAsync(string url)
        {
            return await client.GetByteArrayAsync(url);
        }

        /// <summary>
        ///     Gets the json string asynchronous.
        /// </summary>
        public async Task<string> GetStringAsync(string url)
        {
            return await client.GetStringAsync(url);
        }

        #endregion
    }
}