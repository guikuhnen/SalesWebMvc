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

		[Required, StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1} characters")]
		public string Name { get; set; }

		[Required, DataType(DataType.EmailAddress), EmailAddress(ErrorMessage = "Enter a valid e-mail")]
		public string Email {  get; set; }

        [Display(Name = "Birth Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Base Salary"), DisplayFormat(DataFormatString = "{0:F2}"), Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        public double BaseSalary { get; set; }

		public Department Department { get; set; }
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

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
