using System;
using System.Collections.Generic;
using System.Text;

namespace Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Dtos
{
    public class RateGroupDto
    {
        public int Id;

        public String Name;

        public ICollection<RateDto> Rates;

        public RateGroupDto()
        {
            Rates = new List<RateDto>();
        }

    }
}
