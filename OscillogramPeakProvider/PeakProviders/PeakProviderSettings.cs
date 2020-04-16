using System.Drawing;
using System.Drawing.Drawing2D;

namespace OscillogramPeakProvider.PeakProviders
{
    public class PeakProviderSettings
    {
        public PeakProviderSettings()
        {
            PixelsPerPeak = 1;
            SpacerPixels = 0;
        }

        public int Width { get; set; }
        public int PixelsPerPeak { get; set; }
        public int SpacerPixels { get; set; }

        public bool DecibelScale { get; set; }

    }
}