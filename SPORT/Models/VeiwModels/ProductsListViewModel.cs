﻿

namespace SPORT.Models.VeiwModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        = Enumerable.Empty<Product>();
        public PagingInfo PagingInfo { get; set; } = new();
    }
}