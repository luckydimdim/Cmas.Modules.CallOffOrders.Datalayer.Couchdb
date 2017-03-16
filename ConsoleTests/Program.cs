using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Dtos;
using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Queries; 
using Cmas.Backend.Modules.CallOffOrders.CommandsContexts;
using Cmas.Backend.Modules.CallOffOrders.Entities;
using Cmas.Backend.Modules.CallOffOrders.Entities.Rates;
using Cmas.Backend.Infrastructure.Domain.Criteria;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string id = CreateCallOffOrderCommand().Result;
               
                UpdateCallOffOrderCommand(id).Wait();

                var res = FindByIdQueryTest(id).Result;

                DeleteCallOffOrderCommand(id).Wait();

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

            order.RateGroups = new List<RateGroup>();

            var rate = new Rate();
            rate.Id = 1;
            rate.Name = "Отпуск";
            rate.RateOptions = new RateOptions { Currency = "RUR", Rate = 1000, RemunerationUnit = "День" };

            order.Rates.Add(rate);

            commandContext.Form = order;

            /*
            var simple1 = new RateDto();
            simple1.Id = 1;
            simple1.Name = "Отпуск";
            simple1.RateOptions = new RateOptionsDto { Currency = "RUR", Rate = 1000, RemunerationUnit = "День" };


            var simple2 = new RateDto();
            simple2.Id = 1;
            simple2.Name = "Отпуск";
            simple2.RateOptions = new RateOptionsDto { Currency = "RUR", Rate = 1000, RemunerationUnit = "День" };


            var complex = new ComplexRemunerationDto();
            complex.Id = 2;
            complex.Name = "сложный";

            complex.Remunerations.Add(simple1);
            complex.Remunerations.Add(simple2);

            dto.Remunerations.Add(complex);
            */


            var result = await command.Execute(commandContext);

            return true;
        }


    }
}