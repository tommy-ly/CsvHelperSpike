using System;
using System.Collections.Generic;
using System.IO;
using CsvHelperSpike.Classes;
using NUnit.Framework;

namespace CsvHelperTests.Integration
{
    [TestFixture]
    public class CsvWrapperTests
    {
        private Stream _stream;
        private WhateverRow _whateverRow1;
        private WhateverRow _whateverRow2;

        [TestFixtureSetUp]
        public void WhenProcessingCsv()
        {
            var headers = new List<string>()
            {
                "asdf1",
                "asdf2",
            };

            _whateverRow1 = new WhateverRow()
            {
                Id = Guid.NewGuid(),
                Name = "Herro"
            };

            _whateverRow2 = new WhateverRow()
            {
                Id = Guid.NewGuid(),
                Name = "Another row"
            };
            var csvRows = new List<WhateverRow>()
            {
                _whateverRow1,
                _whateverRow2
            };

            var csvHelperWrapper = new CsvHelperWrapper<WhateverRow>();
            _stream = csvHelperWrapper.Process(headers, csvRows);
        }

        [Test]
        public void ThenTheCsvIsCreatedInTheCorrectFormat()
        {
            var csvContents = new List<string>();

            using (var reader = new StreamReader(_stream))
            {
                while (!reader.EndOfStream)
                    csvContents.Add(reader.ReadLine());
            }

            var expectedHeaders = "\"asdf1\",\"asdf2\"";
            var expectedRow1 = "\"" + _whateverRow1.Id + "\",\"" + _whateverRow1.Name + "\"";
            var expectedRow2 = "\"" + _whateverRow2.Id + "\",\"" + _whateverRow2.Name + "\"";

            Assert.That(csvContents.Count, Is.EqualTo(3));
            Assert.That(csvContents[0], Is.EqualTo(expectedHeaders));
            Assert.That(csvContents[1], Is.EqualTo(expectedRow1));
            Assert.That(csvContents[2], Is.EqualTo(expectedRow2));
        }
    }
}
