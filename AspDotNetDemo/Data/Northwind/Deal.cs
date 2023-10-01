using Clio.Demo.Abstraction.Interface;
using System.Text.Json.Serialization;

namespace Clio.Demo.Data.Northwind
{
	public class Deal : IEntity
	{
		public int     OrderID   { get; set; }
		public int     ProductID { get; set; }

		public int     Quantity  { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal Discount  { get; set; }

		public Product Product { get; set; }

		[JsonIgnore]
		public int    Id     => throw new NotImplementedException();
		[JsonIgnore]
		public string Code   => throw new NotImplementedException();
		[JsonIgnore]
		public string Name   => Product.ProductName;
		[JsonIgnore]
		public string Lookup => throw new NotImplementedException();

		[JsonIgnore]
		public DateTime Created { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		[JsonIgnore]
		public DateTime Updated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	}
}
