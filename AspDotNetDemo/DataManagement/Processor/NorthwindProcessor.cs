using Clio.Demo.Core.Component.Master.Pattern;
using Clio.Demo.Data.Northwind;
using Clio.Demo.DataManagement.DataModel;
using Clio.Demo.Extension;
using Clio.Demo.Util.Telemetry.Seri;
using OrderService.Data;

namespace Clio.Demo.DataManager.Processor
{
    public sealed class NorthwindProcessor
    {
        #region c-tor

        private readonly IOrderDataAccess     _orderData;
        private readonly ICustomerDataAccess  _customerData;
        private readonly IEmployeeDataAccess  _employeeData;
        private readonly IProductDataAccess   _productData;
        private readonly ISupplierDataAccess  _supplierData;
		private readonly IDealDataAccess      _dealData;
		private readonly ITerritoryDataAccess _territoryData;

        public NorthwindProcessor(IOrderDataAccess     orderData,
                                  ICustomerDataAccess  customerData,
                                  IEmployeeDataAccess  employeeData,
                                  IProductDataAccess   productData,
                                  ISupplierDataAccess  supplierData,
								  IDealDataAccess      dealData,
		                          ITerritoryDataAccess territoryData)
        {
            orderData    .Inject<IOrderDataAccess    >(out _orderData);
            customerData .Inject<ICustomerDataAccess >(out _customerData);
            employeeData .Inject<IEmployeeDataAccess >(out _employeeData);
            productData  .Inject<IProductDataAccess  >(out _productData);
            supplierData .Inject<ISupplierDataAccess >(out _supplierData);
			dealData     .Inject<IDealDataAccess>     (out _dealData);
			territoryData.Inject<ITerritoryDataAccess>(out _territoryData);
        }

        #endregion c-tor

        public async Task<Customer> GetCustomer(int id)
        {
            Customer customer = null;
            try
            {
                customer = await _customerData.Read(DataKey<int>.Id(id, "[CustomerID]"));
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetCustomers(IEnumerable<int> ids = null)
        {
            IEnumerable<Customer> customers = null;
            try
            {
                customers = await _customerData.ReadAll(DataMultiKey<int>.Id(ids, "[CustomerID]"));
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
            return customers;
        }

        public async Task<IEnumerable<Product>> GetProducts(IEnumerable<int> ids = null)
        {
            IEnumerable<Product> products = null;
            try
            {
                products = await _productData.ReadAll(DataMultiKey<int>.Id(ids, "[CustomerID]"));
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
            return products;
        }

        public async Task<IEnumerable<Employee>> GetEmployees(IEnumerable<int> ids = null)
        {
            IEnumerable<Employee> employees = null;
            try
            {
                employees = await _employeeData.ReadAll(DataMultiKey<int>.Id(ids, "[CustomerID]"));
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
            return employees;
        }

        public async Task<IEnumerable<Order>> GetOrders(IEnumerable<int> ids = null)
        {
            IEnumerable<Order> orders = null;
            try
            {
                orders = await _orderData.ReadAll(DataMultiKey<int>.Id(ids, "[CustomerID]"));

				/////////////////////////////////////////////////////////////////////////////
                // makeshift code, cache is to be used instead
                //
                IEnumerable<Employee> employees = await _employeeData.ReadAll();
				IEnumerable<Customer> customers = await _customerData.ReadAll();
				IEnumerable<Deal>     deals     = await _dealData    .ReadAll();
				IEnumerable<Product>  products  = await _productData .ReadAll();
                IEnumerable<Supplier> suppliers = await _supplierData.ReadAll();

                foreach (Product product in products)
                {
                    product.Supplier = suppliers.FirstOrDefault(x => x.SupplierID == product.SupplierID);
                }
                foreach (Order order in orders)
                {
                    order.Employee = employees.FirstOrDefault(x => x.EmployeeID == order.EmployeeID);        
                    order.Customer = customers.FirstOrDefault(x => x.CustomerID == order.CustomerID);
					order.Deals    = deals.Where(x => x.OrderID == order.OrderID);

					foreach (Deal deal in order.Deals)
                    {
                        deal.Product = products.FirstOrDefault(x => x.ProductID == deal.ProductID);  
                    }
                }
				/////////////////////////////////////////////////////////////////////////////
			}
			catch (Exception ex)
            {
                Log.Error(this, ex);
            }
            return orders;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliers(IEnumerable<int> ids = null)
        {
            IEnumerable<Supplier> suppliers = null;
            try
            {
                suppliers = await _supplierData.ReadAll(DataMultiKey<int>.Id(ids, "[CustomerID]"));
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
            return suppliers;
        }

        public async Task<IEnumerable<Territory>> GetTerritories(IEnumerable<int> ids = null)
        {
            IEnumerable<Territory> territories = null;
            try
            {
                territories = await _territoryData.ReadAll(DataMultiKey<int>.Id(ids, "[CustomerID]"));
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
            return territories;
        }
    }
}
