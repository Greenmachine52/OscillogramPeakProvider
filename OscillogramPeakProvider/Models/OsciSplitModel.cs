using System.Collections.Generic;
using OscillogramPeakProvider.PeakProviders;

namespace OscillogramPeakProvider.Models
{    
    /// <summary>
     /// This is currently a stub but may be used for staticly typed information models in future development
     /// </summary>
    public class OsciSplitModel
    {
        public IEnumerable<PeakInfo> PeakInfoleft { get; set;}
        public IEnumerable<PeakInfo> PeakInfoRight { get; set; }
    }
}
