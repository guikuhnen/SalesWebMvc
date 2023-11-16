using System;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models.ViewModels
{
	public class SalesRecordViewModel
	{
		[Display(Name = "Min Date")]
		public DateTime? MinDate { get; set; }

		[Display(Name = "Max Date")]
		public DateTime? MaxDate { get; set; }
	}
}
