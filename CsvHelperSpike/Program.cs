using System.IO;
using CsvHelperSpike.Classes;

namespace CsvHelperSpike
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new WhateverService(new CsvHelperWrapper<WhateverRow>(), new WhateverRepo());
            var stream = service.GetCsvContents();

            var memStream = new MemoryStream();
            stream.CopyTo(memStream);

            File.WriteAllBytes("csvhelper.txt", memStream.ToArray());
        }
    }
}
