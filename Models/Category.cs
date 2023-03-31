using System;
namespace ItemLog.Models
{
	public class Category
	{
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
    }
}

