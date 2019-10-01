using System;
using System.Globalization;
using Diot.Helpers;
using Diot.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diot.Views.Extensions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        #region  Fields

        private static ResourceManager _resourceManager;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Returns the translated string.
        /// </summary>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_resourceManager == null)
            {
                throw new InvalidOperationException("Call TranslateExtension.Init(ResourceManager) in your App.cs");
            }

            var translatedString = _resourceManager.GetString(Text);

            return string.IsNullOrWhiteSpace(translatedString) ? string.Empty : translatedString;
        }

        /// <summary>
        ///     Initializes the specified resource manager.
        /// </summary>
        public static void Init(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        #endregion
    }
}