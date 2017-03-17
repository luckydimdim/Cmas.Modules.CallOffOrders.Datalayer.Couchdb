using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Dtos;
using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Queries; 
using Cmas.Backend.Modules.CallOffOrders.CommandsContexts;
using Cmas.Backend.Modules.CallOffOrders.Entities;
using Cmas.Backend.Infrastructure.Domain.Criteria;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                AllTest().Wait();
                /*string id = CreateCallOffOrderCommand().Result;
               
                UpdateCallOffOrderCommand(id).Wait();

                var res = FindByIdQueryTest(id).Result;*/

                //DeleteCallOffOrderCommand(id).Wait();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            Console.ReadKey();
        }

        static async Task<bool> FindByIdQueryTest(string _id)
        {
            FindByIdQuery findByIdQuery = new FindByIdQuery();
            FindById criterion = new FindById(_id);
            CallOffOrder result = null;

            try
            {
                result = await findByIdQuery.Ask(criterion);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            Console.WriteLine(result.Id);

            return true;
        }

        static async Task<string> CreateCallOffOrderCommand()
        {
            var commandContext = new CreateCallOffOrderCommandContext();
            var command = new CreateCallOffOrderCommand();
              
            var result = await command.Execute(commandContext);

            return result.id;
        }

        static async Task<bool> DeleteCallOffOrderCommand(string id)
        {
            var commandContext = new DeleteCallOffOrderCommandContext();
            var command = new DeleteCallOffOrderCommand();

            commandContext.id = id;

            var result = await command.Execute(commandContext);

            return true;
        }

        static async Task<bool> UpdateCallOffOrderCommand(string id)
        {
            var commandContext = new UpdateCallOffOrderCommandContext();
            var command = new UpdateCallOffOrderCommand();

            var order = new CallOffOrder();

            order.Id = id;

            order.Name = "Заказег";
            order.Number = "123/sdhgf";

            {
                var rate = new Rate();
                rate.Id = 1;
                rate.Name = "Услуга";
 
                order.Rates.Add(rate);
            }

            {
                var rate = new Rate();
                rate.Id = 2;
                rate.Name = "Выходные";
                rate.Amount = 1000;
                rate.Currency = "RUR";
                rate.UnitName = "дн.";
                rate.IsRate = true;

                order.Rates.Add(rate);
            }

            commandContext.Form = order;

           


            var result = await command.Execute(commandContext);

            return true;
        }

        static async Task<bool> AllTest()
        {
            AllEntitiesQuery query = new AllEntitiesQuery();
            AllEntities criterion = new AllEntities();
            IEnumerable<CallOffOrder> result = null;

            try
            {
                result = await query.Ask(criterion);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            Console.WriteLine(result.Count());

            return true;
        }
    }
}