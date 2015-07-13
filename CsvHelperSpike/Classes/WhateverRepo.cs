using System;
using System.Collections.Generic;

namespace CsvHelperSpike.Classes
{
    public interface IWhateverRepository
    {
        IList<WhateverRow> Get();
    }

    public class WhateverRepo : IWhateverRepository
    {
        public IList<WhateverRow> Get()
        {
            return new List<WhateverRow>()
            {
                new WhateverRow() {Id = Guid.NewGuid(), Name = "Herro"},
                new WhateverRow() {Id = Guid.NewGuid(), Name = "Hello"},
                new WhateverRow() {Id = Guid.NewGuid(), Name = "Salut"}
            };
        }
    }
}
