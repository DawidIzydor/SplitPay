using System.Windows;

namespace PaySplit.Wpf
{
    public static class UiExtensions
    {
        public static void Show(this UIElement uiElement)
        {
            ChangeVisibility(uiElement, true);
        }
        public static void Hide(this UIElement uiElement)
        {
            ChangeVisibility(uiElement, false);
        }

        private static void ChangeVisibility(UIElement uiElement, bool doShow)
        {
            uiElement.IsEnabled = doShow;
            uiElement.Visibility = doShow ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}