using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace CsvHelperSpike.Classes
{
    public interface ICsvHelperWrapper<T>
    {
        Stream Process(IList<string> headers, IList<T> csvRow);
    }

    public class CsvHelperWrapper<T> : ICsvHelperWrapper<T>
    {
        public Stream Process(IList<string> headers, IList<T> csvRow)
        {
            var config = new CsvConfiguration()
            {
                HasHeaderRecord = false,
                QuoteAllFields = true
            };

            var stream = new MemoryStream();
            var sw = new StreamWriter(stream);
            var csvHelper = new CsvWriter(sw, config);

            foreach (var header in headers)
                csvHelper.WriteField(header);

            csvHelper.NextRecord();

            csvHelper.WriteRecords(csvRow);
            sw.Flush();
            stream.Position = 0;

            return stream;
        }
    }
}
