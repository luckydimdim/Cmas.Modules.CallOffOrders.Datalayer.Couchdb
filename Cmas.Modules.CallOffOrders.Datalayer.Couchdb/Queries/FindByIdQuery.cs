using Cmas.Backend.Infrastructure.Domain.Queries;
using MyCouch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cmas.Backend.Infrastructure.Domain.Criteria;
using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Dtos;

namespace Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Queries
{
    public class FindById : ICriterion
    {
        public string Id;
    }


    public class FindByIdQuery : IQuery<FindById, Task<CallOffOrderDto>>
    {

        public async Task<CallOffOrderDto> Ask(FindById criterion)
        {
            using (var client = new MyCouchClient("http://cmas-backend:backend967@cm-ylng-msk-03:5984", "call-off-orders"))
            {
 
                var dto = await client.Entities.GetAsync<CallOffOrderDto>(criterion.Id);

                return dto.Content;
 
            }

        }
    }
}
