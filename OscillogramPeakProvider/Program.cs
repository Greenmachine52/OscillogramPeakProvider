using System;
using System.Threading.Tasks;
using OscillogramPeakProvider.Services;

namespace OscillogramPeakProvider
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("EnterFileName");


            var path = @"" + Console.ReadLine();
            OsciInfoProviderService.GetAveragePeakInfo(path, 700);
          //  DividerService.DivideToChannels(path);
    
        }
    }
}
