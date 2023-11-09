using System;

namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Department() { }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /*public void AddSeller(Seller seller)
        {

        }

        public double TotalSales(DateTime initial, DateTime final)
        {

        }*/
    }
}
