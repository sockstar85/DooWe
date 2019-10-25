using System.Threading.Tasks;
using Xamarin.Forms;

namespace Diot.Views.Controls
{
    public class ExpandingEntry : Entry
    {
        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }

        #endregion

        #region Bindable Properties

        /// <summary>
        ///     The bindable property for <see cref="IsExpanded"/>.
        /// </summary>
        public static readonly BindableProperty IsExpandedProperty =
            BindableProperty.Create(
                nameof(IsExpanded),
                typeof(bool),
                typeof(ExpandingEntry),
                propertyChanged:
                (bindable, oldValue, newValue) =>
                    ((ExpandingEntry)bindable).updateIsExpanded());

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpandingEntry"/> class.
        /// </summary>
        public ExpandingEntry() : base()
        {
            updateIsExpanded();
        }

        #endregion

        /// <summary>
        ///     Updates and animates the control expansion.
        /// </summary>
        private void updateIsExpanded()
        {
            Task.Run(async () =>
            {
                if (IsExpanded)
                {
                    await Device.InvokeOnMainThreadAsync(() => { IsVisible = IsExpanded; });
                    await this.ScaleTo(1, easing: Easing.SpringOut);
                }
                else
                {
                    await this.ScaleTo(0, easing: Easing.SpringIn);
                    await Device.InvokeOnMainThreadAsync(() => { IsVisible = IsExpanded; });
                }
            });
        }

        #endregion
        
    }
}
