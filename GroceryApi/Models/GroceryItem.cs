﻿using System;
namespace GroceryApi.Models
{
	public class GroceryItem
	{
		public long Id { get; set; }
		public string? Name { get; set; }
		public int? Quantity { get; set; }
		public bool InCart { get; set; }
	}
}

