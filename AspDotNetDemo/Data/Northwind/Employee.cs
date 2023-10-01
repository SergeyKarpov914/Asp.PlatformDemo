using Clio.Demo.Abstraction.Interface;
using System.Text.Json.Serialization;

namespace Clio.Demo.Data.Northwind
{
    public class Employee : IEntity
    {
        public int       EmployeeID      { get; set; }
        public string    LastName        { get; set; }
        public string    FirstName       { get; set; }
        public string    Title           { get; set; }
        public string    TitleOfCourtesy { get; set; }
        public DateTime? BirthDate       { get; set; }
        public DateTime? HireDate        { get; set; }
        public string    Address         { get; set; }
        public string    City            { get; set; }
        public string    Region          { get; set; }
        public string    PostalCode      { get; set; }
        public string    Country         { get; set; }
        public string    HomePhone       { get; set; }
        public string    Extension       { get; set; }
        public byte[]    Photo           { get; set; }
        public string    Notes           { get; set; }
        public int       ReportsTo       { get; set; }
        public string    PhotoPath       { get; set; }

        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public string Code => EmployeeID.ToString();
        [JsonIgnore]
        public string Lookup => EmployeeID.ToString();
        [JsonIgnore]
        public string Name { get; set; }


        [JsonIgnore]
        public DateTime Created { get; set; }
        [JsonIgnore]
        public DateTime Updated { get; set; }

        public override string ToString()
        {
            return $"{EmployeeID} {FirstName} {LastName} {Title} {City}";
        }
    }
}
