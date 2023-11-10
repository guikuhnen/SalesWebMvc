using SalesWebMvc.Models.Enums;
using System;

namespace SalesWebMvc.Models
{
	public class SalesRecord
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public double Ammount { get; set; }
		public SalesStatus Status { get; set; }
		public Seller Seller { get; set; }

		public SalesRecord() { }

		public SalesRecord(int id, DateTime birthDate, double ammount, SalesStatus status, Seller seller)
		{
			Id = id;
			Date = birthDate;
			Ammount = ammount;
			Status = status;
			Seller = seller;
		}
	}
}
