using System;
using System.Collections.Generic;
using System.Text;

namespace Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Dtos.Remuneration
{
    public class ComplexRemunerationDto: BaseRemunerationDto
    {
        public ICollection<SimpleRemunerationDto> Remunerations;

        public ComplexRemunerationDto()
        {
            Remunerations = new List<SimpleRemunerationDto>();
        }

    }
}
