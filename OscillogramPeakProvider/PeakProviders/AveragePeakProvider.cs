using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OscillogramPeakProvider.Models;

namespace OscillogramPeakProvider.PeakProviders
{
    public class AveragePeakProvider : PeakProvider
    {
        private readonly float scale;

        public AveragePeakProvider(float scale)
        {
            this.scale = scale;
        }

        public override PeakInfo GetNextPeak()
        {
            var samplesRead = Provider.Read(ReadBuffer, 0, ReadBuffer.Length - (ReadBuffer.Length % Provider.WaveFormat.BlockAlign));
            var sum = (samplesRead == 0) ? 0 : ReadBuffer.Take(samplesRead).Select(s => Math.Abs(s)).Sum();
            var average = sum / samplesRead;

            return new PeakInfo(average * (0 - scale), average * scale);
        }
    }
}
