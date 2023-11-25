using Clio.Demo.Domain.Data.Entity;
using Clio.Demo.Domain.Data.Processor.DataModel;
using Clio.Demo.Extension;
using Clio.Demo.Util.Telemetry.NLog;

namespace Clio.Demo.Domain.Data.Processor
{
    public class OrderCrudProcessor
    {
        #region c-tor

        private readonly IOrderData    _orderData;
        private readonly ICustomerData _customerData;
        private readonly IEmployeeData _employeeData;
        private readonly IProductData  _productData;

        public OrderCrudProcessor(IOrderData    orderData,
                                  ICustomerData customerData,
                                  IEmployeeData employeeData,
                                  IProductData  productData)
        {
            orderData   .Inject<IOrderData>   (out _orderData);
            customerData.Inject<ICustomerData>(out _customerData);
            employeeData.Inject<IEmployeeData>(out _employeeData);
            productData .Inject<IProductData> (out _productData);
        }

        #endregion c-tor

        public async Task<IEnumerable<Employee>> GetEmployees(IEnumerable<int> ids = null)
        {
            IEnumerable<Employee> employees = null;
            try
            {
                employees = await _employeeData.Read();
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
            return employees;
        }
    }
}
