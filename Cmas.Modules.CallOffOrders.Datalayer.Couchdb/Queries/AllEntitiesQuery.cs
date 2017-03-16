using Cmas.Backend.Infrastructure.Domain.Criteria;
using Cmas.Backend.Infrastructure.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cmas.Backend.Modules.CallOffOrders.Entities;
using Cmas.Backend.Modules.CallOffOrders.Entities.Rates;
using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Dtos;
using MyCouch;
using MyCouch.Requests;
using Newtonsoft.Json;
using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Serialization;

namespace Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Queries
{
    public class AllEntitiesQuery : IQuery<AllEntities, Task<IEnumerable<CallOffOrder>>>
    {
        static AllEntitiesQuery()
        {
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<CallOffOrderDto, CallOffOrder>();
                cfg.CreateMap<RateDto, Rate>();
                cfg.CreateMap<RateOptionsDto, RateOptions>();
            });
        }

        public async Task<IEnumerable<CallOffOrder>> Ask(AllEntities criterion)
        {
            using (var client = new MyCouchClient("http://cmas-backend:backend967@cm-ylng-msk-03:5984", "call-off-orders"))
            {
                var result = new List<CallOffOrder>();

                var query = new QueryViewRequest("call-off-orders", "all");

                var viewResult = await client.Views.QueryAsync<CallOffOrderDto>(query);

                // FIXME: Это ужасно. Мне стыдно
                /*TypeNameSerializationBinder binder = new TypeNameSerializationBinder("Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Dtos.{0}, Cmas.Modules.CallOffOrders.Datalayer.Couchdb");


                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    Binder = binder
                };*/

 
                foreach (var row in viewResult.Rows)
                { 
 
                    result.Add(AutoMapper.Mapper.Map<CallOffOrder>(row.Value));
                }

                return result;
            }
        }
    }
}
