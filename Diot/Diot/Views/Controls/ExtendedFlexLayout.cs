using System.Collections;
using Xamarin.Forms;

namespace Diot.Views.Controls
{
    /// <summary>
    ///     A class that has the same functionality of a <see cref="FlexLayout"/> but is able to bind to a collection.
    /// </summary>
    /// <seealso cref="FlexLayout" />
    public class ExtendedFlexLayout : FlexLayout
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the items source.
        /// </summary>
        public IEnumerable ItemsSource
        {
            get => (IEnumerable) GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        ///     Gets or sets the item template.
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate) GetValue(DataTemplateProperty);
            set => SetValue(DataTemplateProperty, value);
        }

        #endregion

        #region Bindable Properties

        /// <summary>
        ///     Bindable property for <see cref="ItemsSource"/>.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(ExtendedFlexLayout),
                propertyChanged:
                (bindable, value, newValue) => updateLayoutChildren(bindable, newValue));

        /// <summary>
        ///     Bindable property for <see cref="ItemTemplate"/>.
        /// </summary>
        public static readonly BindableProperty DataTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(ExtendedFlexLayout));

        #endregion

        #region Methods

        /// <summary>
        ///     Updates the layout children.
        /// </summary>
        private static void updateLayoutChildren(BindableObject bindable, object newValue)
        {
            var layout = (ExtendedFlexLayout) bindable;
            IEnumerable itemsSource = newValue as IEnumerable;

            //Remove all children as we will add them again
            layout.Children.Clear();

            if (itemsSource == null)
            {
                return;
            }

            foreach (var item in itemsSource)
            {
                layout.Children.Add(layout.createChildView(item));
            }
        }

        /// <summary>
        ///     Creates the child view.
        /// </summary>
        /// <param name="item">The item.</param>
        private View createChildView(object item)
        {
            ItemTemplate.SetValue(BindableObject.BindingContextProperty, item);
            return (View) ItemTemplate.CreateContent();
        }

        #endregion
    }
}
