using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelperSpike.Classes;
using Moq;
using NUnit.Framework;

namespace CsvHelperTests.Unit
{
    [TestFixture]
    public class WhateverServiceTests
    {
        private Stream _result;
        private Mock<ICsvHelperWrapper<WhateverRow>> _csvWrapper;
        private Mock<IWhateverRepository> _repo;
        private List<WhateverRow> _whatevers;
        private MemoryStream _stream;

        [TestFixtureSetUp]
        public void WhenProcessingWhatevers()
        {
            _stream = new MemoryStream();

            _whatevers = new List<WhateverRow>();

            _repo = new Mock<IWhateverRepository>();
            _repo.Setup(r => r.Get())
                .Returns(_whatevers);

            _csvWrapper = new Mock<ICsvHelperWrapper<WhateverRow>>();
            _csvWrapper.Setup(csv => csv.Process(It.IsAny<IList<string>>(), It.IsAny<IList<WhateverRow>>()))
                       .Returns(_stream);

            var service = new WhateverService(_csvWrapper.Object, _repo.Object);
            _result = service.GetCsvContents();
        }

        [Test]
        public void ThenWhateversAreRetrieved()
        {
            _repo.Verify(r => r.Get());
        }

        [Test]
        public void ThenTheCsvStreamIsRetrieved()
        {
            _csvWrapper.Verify(w => w.Process(It.Is<IList<string>>(h => ContainsRequiredHeaders(h)), _whatevers));
        }

        [Test]
        public void ThenTheResultIsTheCsvStream()
        {
            Assert.That(_result, Is.EqualTo(_stream));
        }

        private bool ContainsRequiredHeaders(IList<string> headers)
        {
            var requiredHeaders = new List<string>();
            requiredHeaders.Add("header1");
            requiredHeaders.Add("header2");

            return headers.All(requiredHeaders.Contains) && headers.Count == requiredHeaders.Count;
        }
    }
}
