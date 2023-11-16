using SalesWebMvc.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
	public class SalesRecord
	{
		public int Id { get; set; }

		[DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime Date { get; set; }

		[DisplayFormat(DataFormatString = "{0:F2}")]
		public double Ammount { get; set; }

		public SalesStatus Status { get; set; }

		public Seller Seller { get; set; }
		[Display(Name = "Seller")]
		public int SellerId { get; set; }

		public SalesRecord() { }

		public SalesRecord(DateTime birthDate, double ammount, SalesStatus status, Seller seller)
		{
			Date = birthDate;
			Ammount = ammount;
			Status = status;
			Seller = seller;
		}
	}
}
