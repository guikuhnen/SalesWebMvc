using SalesWebMvc.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
	public class Department
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<Seller> Sellers { get; private set; } = new List<Seller>();

		public Department() { }

		public Department(string name)
		{
			Name = name;
		}

		public void AddSeller(Seller seller)
		{
			Sellers.Add(seller);
		}

		public double TotalSales(DateTime initial, DateTime final)
		{
			return Sellers.Sum(x => x.TotalSales(initial, final));
		}
	}
}
