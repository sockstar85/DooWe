using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using Diot.Helpers;
using Diot.Interface;
using Diot.Resources;
using Xamarin.Forms;

namespace Diot.Services
{
    public class ResourceManager : IResourceManager
    {
        #region  Fields

        private readonly System.Resources.ResourceManager _resourceManager;

        #endregion

        #region Properties


        /// <summary>
        ///     The supported cultures
        /// </summary>
        public static List<string> SupportedCultures = new List<string> {CultureCodes.EnglishUS, CultureCodes.Spanish};

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        internal static IResourceManager Instance { get; private set; }

        /// <summary>
        /// Gets or sets the current culture.
        /// </summary>
        public CultureInfo CurrentCulture { get; set; } = CultureInfo.CurrentUICulture;

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResourceManager" /> class.
        /// </summary>
        public ResourceManager()
        {
            var typeInfo = typeof(AppResources).GetTypeInfo();

            if (typeInfo != null)
            {
                _resourceManager = new System.Resources.ResourceManager(typeInfo.FullName ?? throw new InvalidOperationException(), typeInfo.Assembly);
            }
        }

        #endregion

        /// <summary>
        ///     Gets the app resource.
        /// </summary>
        public object GetResource(string key)
        {
            object retVal = null;

            try
            {
                Application.Current.Resources.TryGetValue(key, out retVal);
            }
            catch (Exception)
            {
                Debug.WriteLine($"Resource key \"{key}\" was not found");
            }

            return retVal;
        }

        /// <summary>
        ///     Gets the string from the appropriate resx file.
        /// </summary>
        public string GetString(string key)
        {
            var retVal = _resourceManager.GetString(key, CurrentCulture);

            if (!string.IsNullOrEmpty(retVal))
            {
                //replace with environment new lines
                return retVal.Replace("\\n", Environment.NewLine);
            }

            Debug.WriteLine($"Resource key \"{key}\" was not found");
            return retVal;
        }

        /// <summary>
        ///     Initializes the specified resource manager.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        public static void Init(IResourceManager resourceManager)
        {
            Instance = resourceManager;
        }

        #endregion
    }
}