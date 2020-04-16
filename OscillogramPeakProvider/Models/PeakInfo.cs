using System;
using System.Collections.Generic;
using System.Text;

namespace OscillogramPeakProvider.Models
{
    public class PeakInfo
    {
        public PeakInfo()
        {
        }

        public PeakInfo(float min, float max)
        {
            Max = max;
            Min = min;
        }

        public float Min { get; set; }
        public float Max { get; set; }
    }
}
