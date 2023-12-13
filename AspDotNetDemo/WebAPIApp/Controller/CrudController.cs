using Clio.Demo.Core.Extension;
using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Domain.Data.Northwind;
using Clio.Demo.DataManager.Processor;
using Clio.Demo.Extension;
using Microsoft.AspNetCore.Mvc;

namespace Clio.Demo.WebAPIApp.Controller
{
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly NorthwindProcessor _processor;

        public CrudController(NorthwindProcessor processor)
        {
            processor.Inject<NorthwindProcessor>(out _processor);
        }

        [Route("api/customers/{key}")]
        [HttpGet]
        public async Task<IActionResult> GetCustomer(int key)
        {
            Customer customer = null;
            try
            {
                customer = await _processor.GetCustomer(key) ?? throw new CrudException<Customer>(false);
            }
            catch (Exception ex)
            {
                return this.Failed(nameof(GetCustomer), ex.Message);
            }
            return this.Success(customer, nameof(GetCustomer));
        }

        [Route("api/orders")]
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            IEnumerable<Order> orders = null;
            try
            {
                orders = await _processor.GetOrders() ?? throw new CrudException<Order>();
            }
            catch (Exception ex)
            {
                return this.Failed(nameof(GetOrders), ex.Message);
            }
            return this.Success(orders, nameof(GetOrders));
        }

        //[Route("api/orders/{order}")]
        //[HttpPut]
        //public async Task<IActionResult> PutOrder(Order order)
        //{
        //    try
        //    {
        //        await _processor.Put<Order>(order);
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.Failed(nameof(GetOrder), ex.Message);
        //    }
        //    return this.Success(order, nameof(GetOrder));
        //}

        [Route("api/products/{key}")]
        [HttpGet]
        public async Task<IActionResult> GetProducts(string key)
        {
            IEnumerable<Product> products = null;
            try
            {
                products = await _processor.GetProducts() ?? throw new CrudException<Product>();
            }
            catch (Exception ex)
            {
                return this.Failed(nameof(GetProducts), ex.Message);
            }
            return this.Success(products, nameof(GetProducts));
        }

		[Route("api/products")]
		[HttpGet]
		public async Task<IActionResult> GetProducts()
		{
			IEnumerable<Product> products = null;
			try
			{
				products = await _processor.GetProducts() ?? throw new CrudException<Product>();
			}
			catch (Exception ex)
			{
				return this.Failed(nameof(GetProducts), ex.Message);
			}
			return this.Success(products, nameof(GetProducts));
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

        [Route("api/suppliers")]
        [HttpGet]
        public async Task<IActionResult> GetSuppliers()
        {
            IEnumerable<Supplier> suppliers = null;
            try
            {
                suppliers = await _processor.GetSuppliers() ?? throw new CrudException<Supplier>();
            }
            catch (Exception ex)
            {
                return this.Failed(nameof(GetSuppliers), ex.Message);
            }
            return this.Success(suppliers, nameof(GetSuppliers));
        }

        [Route("api/customers")]
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            IEnumerable<Customer> customers = null;
            try
            {
                customers = await _processor.GetCustomers() ?? throw new CrudException<Customer>();
            }
            catch (Exception ex)
            {
                return this.Failed(nameof(GetCustomers), ex.Message);
            }
            return this.Success(customers, nameof(GetCustomers));
        }
    }

    public class CrudException<T> : Exception where T : class, IEntity
    {
        public CrudException(bool multi = true) : base($"NULL {typeof(T).Name} {(multi ? "collection" : "")} returned by processor")
        { }
    }

}
