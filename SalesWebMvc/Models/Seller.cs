using SalesWebMvc.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
	public class Seller
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email {  get; set; }
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Base Salary")]
        public double BaseSalary { get; set; }
		public Department Department { get; set; }
		public ICollection<SalesRecord> Sales { get; private set; } = new List<SalesRecord>();

		public Seller () { }

		public Seller(string name, string email, DateTime birthDate, double baseSalary, Department department)
		{
			Name = name;
			Email = email;
			BirthDate = birthDate;
			BaseSalary = baseSalary;
			Department = department;
		}

		public void AddSales(SalesRecord record)
		{
			Sales.Add(record);
		}

		public void RemoverSales(SalesRecord record)
		{
			Sales.Remove(record);
		}

		public double TotalSales(DateTime initial, DateTime final)
		{
			return Sales.Where(x => x.Date >= initial && x.Date <= final).Sum(y => y.Ammount);
		}
	}
}
