using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Dtos.Remuneration;
using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Queries;
using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Dtos;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                FindByIdQueryTest().Wait();
                //CreateCallOffOrderCommand().Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            Console.ReadKey();
        }

        static async Task<bool> FindByIdQueryTest()
        {
            FindByIdQuery findByIdQuery = new FindByIdQuery();
            FindById criterion = new FindById { Id = "26270cfa2422b2c4ebf158285e054e1d" };
            CallOffOrderDto result = null;

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

        static async Task<bool> CreateCallOffOrderCommand()
        {
            var commandContext = new CreateCallOffOrderCommandContext();
            var command = new CreateCallOffOrderCommand();

            var dto = commandContext.dto;

            dto.Name = "Заказег";
            dto.Number = "123/sdhgf";

            dto.Remunerations = new List<BaseRemunerationDto>();

            var simple  = new SimpleRemunerationDto();
            simple.Id = 1;
            simple.Name = "Отпуск";
            simple.RateOptions = new RateOptionsDto{Currency = "RUR", Rate = 1000, RemunerationUnit = "День"};
            dto.Remunerations.Add(simple);

            var simple1 = new SimpleRemunerationDto();
            simple1.Id = 1;
            simple1.Name = "Отпуск";
            simple1.RateOptions = new RateOptionsDto { Currency = "RUR", Rate = 1000, RemunerationUnit = "День" };
           

            var simple2 = new SimpleRemunerationDto();
            simple2.Id = 1;
            simple2.Name = "Отпуск";
            simple2.RateOptions = new RateOptionsDto { Currency = "RUR", Rate = 1000, RemunerationUnit = "День" };
 

            var complex = new ComplexRemunerationDto();
            complex.Id = 2;
            complex.Name = "сложный";

            complex.Remunerations.Add(simple1);
            complex.Remunerations.Add(simple2);

            dto.Remunerations.Add(complex);

            var result = await command.Execute(commandContext);

            return true;
        }

    }
}