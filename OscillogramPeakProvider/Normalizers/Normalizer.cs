using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OscillogramPeakProvider.Models;
using OscillogramPeakProvider.PeakProviders;

namespace OscillogramPeakProvider.Normalizers
{
    public class Normalizer
    {
        public Task<IList<PeakInfo>> Normalize(IList<PeakInfo> peakInfos) 
        {
            var coef = 10 / peakInfos.Select(x => x.Max).Max();
            IList<PeakInfo> result = peakInfos.Select(x => new PeakInfo() { Max = (float)Math.Round(x.Max * coef), Min = (float)Math.Round(x.Min * coef) }).ToList();

            return Task.FromResult(result);
        }
    }
}
