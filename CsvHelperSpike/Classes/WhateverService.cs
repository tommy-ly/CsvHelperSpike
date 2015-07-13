using System.Collections.Generic;
using System.IO;

namespace CsvHelperSpike.Classes
{
    public interface IWhateverService
    {
        Stream GetCsvContents();
    }

    public class WhateverService : IWhateverService
    {
        private readonly ICsvHelperWrapper<WhateverRow> _csvHelperWrapper;
        private readonly IWhateverRepository _whateverRepository;

        public WhateverService(ICsvHelperWrapper<WhateverRow> csvHelperWrapper, IWhateverRepository whateverRepository)
        {
            _csvHelperWrapper = csvHelperWrapper;
            _whateverRepository = whateverRepository;
        }

        public Stream GetCsvContents()
        {
            var csvRows = _whateverRepository.Get();
            var headers = new List<string>()
            {
                "header1",
                "header2"
            };

            var stream = _csvHelperWrapper.Process(headers, csvRows);

            return stream;
        }
    }
}
