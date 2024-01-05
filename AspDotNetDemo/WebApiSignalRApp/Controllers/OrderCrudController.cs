using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Lib.Extension;
using Clio.Demo.Domain.Data.Hbc;
using Clio.Demo.Domain.Data.Processor;
using Microsoft.AspNetCore.Mvc;

namespace Clio.Demo.OrderCrudServer.Controllers
{
    [ApiController]
    public class OrderCrudController : ControllerBase
    {
        private readonly OrderCrudProcessor _processor;

        public OrderCrudController(OrderCrudProcessor processor)
        {
            processor.Inject<OrderCrudProcessor>(out _processor);
        }

        [Route("api/employees")]
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            IEnumerable<Employee> employees = null;
            try
            {
                employees = await _processor.GetEmployees() ?? throw new CrudException<Employee>();
            }
            catch (Exception ex)
            {
                return this.Failed(nameof(GetEmployees), ex.Message);
            }
            return this.Success(employees, nameof(GetEmployees));
        }
    }

    public class CrudException<T> : Exception where T : class, IEntity
    {
        public CrudException(bool multi = true) : base($"NULL {typeof(T).Name} {(multi ? "collection" : "")} returned by processor")
        { }
    }
}
