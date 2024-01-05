using Clio.Demo.Domain.Data.Northwind;
using Clio.Demo.Core.Lib.Extension;

namespace Clio.Demo.DataPresentation.Elements
{
    public class OrderElement
    {
        private readonly Order _order;

        public OrderElement(Order order)
        {
            order.Inject(out _order);
        }
        public IEnumerable<Deal> Deals => _order.Deals;
        
        public int OrderID => _order.OrderID;

        public string OrderDate       => _order.OrderDate?.ToString("MM/dd/yyyy");
        public string RequiredDate    => _order.RequiredDate?.ToString("MM/dd/yyyy");
        public string ShippedDate     => _order.ShippedDate?.ToString("MM/dd/yyyy");

        public string CompanyName     => _order.Customer.CompanyName;
        public string ContactName     => _order.Customer.ContactTitle;
        public string Address         => $"{_order.Customer.Address} {_order.Customer.City} {_order.Customer.Country}";
        public string Phone           => _order.Customer.Phone;

        public string EmployeeName    => $"{_order.Employee.FirstName} {_order.Employee.LastName}";
        public string EmployeeTitle   => _order.Employee.Title;
    }
}
