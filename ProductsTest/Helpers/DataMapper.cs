using ProductsTest.Models;
using ProductsTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsTest.Helpers
{
    public class DataMapper
    {
        public Product MapData(ProductViewModel viewModel)
        {
            return new Product
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Code = viewModel.Code
            };
        }

        public ProductViewModel MapData(Product model)
        {
            return new ProductViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Code = model.Code
            };
        }
    }
}