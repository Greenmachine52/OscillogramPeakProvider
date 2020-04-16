using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;
using OscillogramPeakProvider.Models;

namespace OscillogramPeakProvider.PeakProviders
{

    public interface IPeakProvider
    {
        void Init(ISampleProvider reader, int samplesPerPeak);
        PeakInfo GetNextPeak();
    }
}
