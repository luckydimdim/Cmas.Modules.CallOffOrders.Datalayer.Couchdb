using Cmas.Backend.Infrastructure.Domain.Commands;
using Cmas.Backend.Modules.CallOffOrders.CommandsContexts;
using MyCouch;
using System.Threading.Tasks;

namespace Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Commands
{
    public class DeleteCallOffOrderCommand : ICommand<DeleteCallOffOrderCommandContext>
    {
        public async Task<DeleteCallOffOrderCommandContext> Execute(DeleteCallOffOrderCommandContext commandContext)
        {
            using (var store = new MyCouchStore("http://cmas-backend:backend967@cm-ylng-msk-03:5984", "call-off-orders"))
            {

                await store.DeleteAsync(commandContext.id);
                 
                return commandContext;
            }

        }
    }
}
