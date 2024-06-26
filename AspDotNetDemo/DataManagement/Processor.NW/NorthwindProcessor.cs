﻿using Clio.Demo.Core.Lib.Extension;
using Clio.Demo.Core.Lib.Util;
using Clio.Demo.DataManagement.Processor.NW.DataModel;
using Clio.Demo.Domain.Data.Northwind;
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
                customer = (await _customerData.Read($"[CustomerID]={id}")).FirstOrDefault();
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
                customers = await _customerData.Read(ids.ToInClause<int>("[CustomerID]"));
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
                products = await _productData.Read(ids.ToInClause<int>("[ProductID]"));
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
                employees = await _employeeData.Read(ids.ToInClause<int>("[EmployeeID]"));
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
                orders = await _orderData.Read(ids.ToInClause<int>("[OrderID]"));

				/////////////////////////////////////////////////////////////////////////////
                // makeshift code, cache is to be used instead
                //
                IEnumerable<Employee> employees = await _employeeData.Read();
				IEnumerable<Customer> customers = await _customerData.Read();
				IEnumerable<Deal>     deals     = await _dealData    .Read();
				IEnumerable<Product>  products  = await _productData .Read();
                IEnumerable<Supplier> suppliers = await _supplierData.Read();

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
                suppliers = await _supplierData.Read(ids.ToInClause<int>("[SupplierID]"));
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
                territories = await _territoryData.Read(ids.ToInClause<int>("[TerritoryID]"));
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
            return territories;
        }
    }
}
