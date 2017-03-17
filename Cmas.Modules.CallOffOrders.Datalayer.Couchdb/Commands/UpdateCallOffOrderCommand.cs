using Cmas.Backend.Infrastructure.Domain.Commands;
using Cmas.Backend.Modules.CallOffOrders.CommandsContexts;
using Cmas.Backend.Modules.CallOffOrders.Entities;
using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Dtos;
using MyCouch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Commands
{
 
    public class UpdateCallOffOrderCommand : ICommand<UpdateCallOffOrderCommandContext>
    {
        static UpdateCallOffOrderCommand()
        {
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<CallOffOrder, CallOffOrderDto>();
                cfg.CreateMap<Rate, RateDto>();
            });
        }
 
        public async Task<UpdateCallOffOrderCommandContext> Execute(UpdateCallOffOrderCommandContext commandContext)
        {
            using (var client = new MyCouchClient("http://cmas-backend:backend967@cm-ylng-msk-03:5984", "call-off-orders"))
            {
                // FIXME: нельзя так делать, надо от frontend получать
                var existingDoc = (await client.Entities.GetAsync<CallOffOrderDto>(commandContext.Form.Id)).Content;
 
                var newDto = AutoMapper.Mapper.Map<CallOffOrderDto>(commandContext.Form);
                newDto._id = existingDoc._id;
                newDto.Status = existingDoc.Status;
                newDto._rev = existingDoc._rev;

                newDto.UpdatedAt = DateTime.Now;

                if (newDto.Status == "empty")
                {
                    newDto.CreatedAt = DateTime.Now;
                }

                newDto.Status = "published";

                var result = await client.Entities.PutAsync<CallOffOrderDto>(newDto._id, newDto);

                // TODO: возвращать _revid

                return commandContext;
            }

        }
    }
}
