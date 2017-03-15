using Cmas.Backend.Infrastructure.Domain.Commands;
using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Dtos;
using MyCouch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Commands
{
    public class CreateCallOffOrderCommandContext: ICommandContext
    {
        public CallOffOrderDto dto;
        public String Id;

        public CreateCallOffOrderCommandContext()
        {
            dto = new CallOffOrderDto();
        }
    }

    public class CreateCallOffOrderCommand : ICommand<CreateCallOffOrderCommandContext>
    {
        public async Task<CreateCallOffOrderCommandContext> Execute(CreateCallOffOrderCommandContext commandContext)
        {
            using (var store = new MyCouchStore("http://cmas-backend:backend967@cm-ylng-msk-03:5984", "call-off-orders"))
            {

                var doc = commandContext.dto;

                doc.UpdatedAt = DateTime.Now;
                doc.CreatedAt = DateTime.Now;
                doc.Status = "empty";

                var result = await store.Client.Entities.PostAsync(doc);

                commandContext.Id = result.Id;


                return commandContext;
            }

        }
    }
}
