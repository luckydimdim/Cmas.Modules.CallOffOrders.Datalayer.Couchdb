using Cmas.Backend.Infrastructure.Domain.Commands;
using Cmas.Backend.Modules.CallOffOrders.CommandsContexts;
using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Dtos;
using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Serialization;
using MyCouch;
using MyCouch.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Commands
{
    public class CreateCallOffOrderCommand : ICommand<CreateCallOffOrderCommandContext>
    {
        public async Task<CreateCallOffOrderCommandContext> Execute(CreateCallOffOrderCommandContext commandContext)
        {
            using (var store = new MyCouchStore("http://cmas-backend:backend967@cm-ylng-msk-03:5984", "call-off-orders"))
            {
                 
                var query = new QueryViewRequest("call-off-orders", "empty");

                var viewResult = await store.Client.Views.QueryAsync(query);

                if (viewResult.RowCount > 0)
                {
                    commandContext.id = viewResult.Rows.First().Id;
                }
                else
                {
                    var doc = new CallOffOrderDto();

                    doc.UpdatedAt = DateTime.Now;
                    doc.CreatedAt = DateTime.Now;
                    doc.Status = "empty";

                    var result = await store.Client.Entities.PostAsync<CallOffOrderDto>(doc);

                    commandContext.id = result.Id;
                } 
                 
                return commandContext;
            }

        }
    }
}
