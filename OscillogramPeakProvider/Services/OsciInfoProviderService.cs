using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Wave;
using OscillogramPeakProvider.Models;
using OscillogramPeakProvider.PeakProviders;

namespace OscillogramPeakProvider.Services
{
    public class OsciInfoProviderService
    {

        public static IList<PeakInfo>  GetAveragePeakInfo(string selectedFile, int pointOnXAccess) 
        {
            var provider = new AveragePeakProvider(4);
            return GetInfo(provider, selectedFile, pointOnXAccess);
        }

        /// <summary>
        /// This method is currently a stub and may be removed upon development
        /// </summary>
        /// <param name="selectedFile"></param>
        /// <param name="pointOnXAccess"></param>
        /// <returns></returns>
        public static IList<PeakInfo> GetDecibelPeakInfo(string selectedFile, int pointOnXAccess)
        {
            throw new NotImplementedException();
         //   var provider = new DecibelPeakProvider(4);
           // return GetInfo(provider, selectedFile, pointOnXAccess);
        }


        public static IList<PeakInfo> GetMaxPeakInfo(string selectedFile, int pointOnXAccess)
        {
            var provider = new MaxPeakProvider();
            return GetInfo(provider, selectedFile, pointOnXAccess);
        }

        /// <summary>
        /// This method is currently a stub and may be removed upon development
        /// </summary>
        /// <param name="selectedFile"></param>
        /// <param name="pointOnXAccess"></param>
        /// <returns></returns>
        public static IList<PeakInfo> GetRmsPeakInfo(string selectedFile, int pointOnXAccess)
        {
            throw new NotImplementedException();
            /*  var provider = new RmsPeakProvider();
              return GetInfo(provider, selectedFile, pointOnXAccess); */
        }

        private static IList<PeakInfo> GetInfo(IPeakProvider peakProvider, string selectedFile, int pointOnXAccess)
        {  
            var settings = new PeakProviderSettings();

            using (var reader = new AudioFileReader(selectedFile))
            {
                int bytesPerSample = (reader.WaveFormat.BitsPerSample / 8);
                settings.Width = pointOnXAccess;
                var samples = reader.Length / (bytesPerSample);
                var samplesPerPixel = (int)(samples / settings.Width);
                var stepSize = settings.PixelsPerPeak + settings.SpacerPixels;
                peakProvider.Init(reader, samplesPerPixel * stepSize);
                return GetInfo(peakProvider, settings);    
            }
        }


        private static IList<PeakInfo> GetInfo(IPeakProvider peakProvider, PeakProviderSettings settings)
        {        
                var infos  = new List<PeakInfo>();

                int x = 0;
                var currentPeak = peakProvider.GetNextPeak();
                while (x < settings.Width)
                {
                    var nextPeak = peakProvider.GetNextPeak();

                    for (int n = 0; n < settings.PixelsPerPeak; n++)
                    {
                        x++;
                    }

                    for (int n = 0; n < settings.SpacerPixels; n++)
                    {
                        // spacer bars are always the lower of the 
                        var max = Math.Min(currentPeak.Max, nextPeak.Max);
                        var min = Math.Max(currentPeak.Min, nextPeak.Min);
                        x++;
                    }
                    infos.Add(currentPeak);
                    currentPeak = nextPeak;
                }
                infos.Where(x => x.Max > 0);
                return infos;
        }
    }
}
