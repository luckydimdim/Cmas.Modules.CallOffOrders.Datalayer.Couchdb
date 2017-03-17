using Cmas.Backend.Infrastructure.Domain.Queries;
using MyCouch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cmas.Backend.Infrastructure.Domain.Criteria;
using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Dtos;
using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Serialization;
using Newtonsoft.Json;
using System.Reflection;
using Cmas.Backend.Modules.CallOffOrders.Entities;

namespace Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Queries
{
    public class FindByIdQuery : IQuery<FindById, Task<CallOffOrder>>
    {

        static FindByIdQuery()
        {
            
                AutoMapper.Mapper.Initialize(cfg => {
                    cfg.CreateMap<CallOffOrderDto, CallOffOrder >();
                    cfg.CreateMap<RateDto,Rate >();
                });
             
        }

        public async Task<CallOffOrder> Ask(FindById criterion)
        {
            using (var client = new MyCouchClient("http://cmas-backend:backend967@cm-ylng-msk-03:5984", "call-off-orders"))
            {
 
                var dto = await client.Entities.GetAsync<CallOffOrderDto>(criterion.Id);


                // FIXME: Это ужасно. Мне стыдно
                /* TypeNameSerializationBinder binder = new TypeNameSerializationBinder("Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Dtos.{0}, Cmas.Modules.CallOffOrders.Datalayer.Couchdb");


                 JsonSerializerSettings settings = new JsonSerializerSettings
                 {
                     TypeNameHandling = TypeNameHandling.Objects,
                     Binder = binder
                 };

                 var doc = JsonConvert.DeserializeObject<CallOffOrderDto>(dto.Content, settings);*/

                CallOffOrder result = AutoMapper.Mapper.Map<CallOffOrder>(dto.Content);
                result.Id = dto.Content._id;

                return result;
 
            }

        }
    }
}
