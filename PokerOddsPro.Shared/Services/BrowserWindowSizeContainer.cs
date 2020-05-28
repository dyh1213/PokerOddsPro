using BlazorPro.BlazorSize;
using PokerOddsPro.Shared.Helpers;

namespace PokerOddsPro.Shared.Services
{
    public class BrowserWindowSizeContainer
    {
        BrowserWindowSize windowSize { get; set; }

        public bool IsWidthSuperTiny = false;

        public bool IsSmallMedia = false;

        public bool IsMobileProportion = false;

        public bool UpdateSize(BrowserWindowSize window)
        {
            var newWidth = window.Width;

            if (IsWidthSuperTiny != Helper.SuperTinyMediaThreshold > newWidth)
            {
                IsWidthSuperTiny = !IsWidthSuperTiny;
                windowSize = window;
                return true;
            }

            var IsNewSizeSmallMedia = Helper.HorizontalToVerticalCardThreshold > newWidth;
            if (IsSmallMedia != IsNewSizeSmallMedia)
            {
                IsSmallMedia = IsNewSizeSmallMedia;
                windowSize = window;
                return true;
            }

            if (IsMobileProportion != (double)window.Height / window.Width > 1.4)
            {
                IsMobileProportion = !IsMobileProportion;
                windowSize = window;
                return true;
            }

            return false;
        }
    }
}
