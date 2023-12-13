using Clio.Demo.Domain.Data.Northwind;
using Clio.Demo.Extension;

namespace Clio.Demo.DataPresentation.Elements
{
    public class DealElement
    {
        private readonly Deal _deal;

        public DealElement(Deal deal)
        {
            deal.Inject(out _deal);
        }
        public int OrderID   => _deal.OrderID;
        public int ProductID => _deal.ProductID;

        public int     Quantity     => _deal.Quantity;
        public decimal OrderPrice   => _deal.UnitPrice;

        public decimal UnitPrice    => _deal.Product.UnitPrice;
        public short   UnitsInStock => _deal.Product.UnitsInStock;
        public short   UnitsOnOrder => _deal.Product.UnitsOnOrder;

        public string ProductName   => _deal.Product.ProductName;
        public string CompanyName   => _deal.Product.Supplier.CompanyName;
        public string ContactName   => _deal.Product.Supplier.ContactTitle;
        public string Address       => $"{_deal.Product.Supplier.Address} {_deal.Product.Supplier.City} {_deal.Product.Supplier.Country}";
        public string Phone         => _deal.Product.Supplier.Phone;
    }
}
