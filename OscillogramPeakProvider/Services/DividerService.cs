using System;
using System.Collections.Generic;
using System.Text;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using OscillogramPeakProvider.PeakProviders;
using OscillogramPeakProvider.Models;
using System.Threading.Tasks;

namespace OscillogramPeakProvider.Services
{
    /// <summary>
    /// not currently used.
    /// </summary>
    public static class DividerService
    {
        public static  async Task<(String first, String second)> DivideFile(string fullName)
        {
            string saveFolder = Path.GetTempPath();

            using (var reader = new WaveFileReader(fullName))
                {
                    var fileName = Path.GetFileNameWithoutExtension(fullName);

                    if (reader.WaveFormat.Channels == 1)
                    {
                        throw new Exception("File is Mono");
                    }

                    var buffer = new byte[2 * reader.WaveFormat.SampleRate * reader.WaveFormat.Channels];
                    var writers = new WaveFileWriter[reader.WaveFormat.Channels];

                    for (int n = 0; n < writers.Length; n++)
                    {
                        var format = new WaveFormat(reader.WaveFormat.SampleRate, 16, 1);
                        writers[n] = new WaveFileWriter(Path.Combine(saveFolder, String.Format("{0}-channel{1}.wav", fileName, n)), format);
                    }
                    int bytesRead;
                    while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        int offset = 0;
                        while (offset < bytesRead)
                        {
                            for (int n = 0; n < writers.Length; n++)
                            {
                                // write one sample
                                writers[n].Write(buffer, offset, 2);
                                offset += 2;
                            }
                        }
                    }
                    for (int n = 0; n < writers.Length; n++)
                    {
                        writers[n].Dispose();
                    }
                    var channelOneName = Path.Combine(saveFolder, String.Format("{0}-channel{1}.wav", fileName, 0));
                    var channelTwoName = Path.Combine(saveFolder, String.Format("{0}-channel{1}.wav", fileName, 1));

                    return (first : channelOneName, second : channelTwoName);
                }
        }

    }
}
