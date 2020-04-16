using System;
using System.Collections.Generic;
using System.Text;
using OscillogramPeakProvider.PeakProviders;

namespace OscillogramPeakProvider.Models
{
    /// <summary>
    /// This is currently a stub but may be used for staticly typed information models in future development
    /// </summary>
    public class OsciMonoModel
    {
        public IEnumerable<PeakInfo> PeakInfo { get; set; }
    }
}
