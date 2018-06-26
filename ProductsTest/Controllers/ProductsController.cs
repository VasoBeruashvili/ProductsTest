using ProductsTest.DataLayer;
using ProductsTest.Helpers;
using ProductsTest.Models;
using ProductsTest.Repositories;
using ProductsTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductsTest.Controllers
{
    public class ProductsController : Controller
    {
        private GenericRepository<Product> _repo = null;
        private DataContext _ctx = null;
        private DataMapper _mapper = null;

        public ProductsController()
        {
            if (_ctx == null)
                _ctx = new DataContext();

            if (_repo == null)
                _repo = new GenericRepository<Product>(_ctx);

            if (_mapper == null)
                _mapper = new DataMapper();
        }

        public ActionResult Index()
        {
            return View(GetProducts());
        }


        [HttpGet]
        public ActionResult AddEdit(int? id)
        {
            var viewModel = new ProductViewModel();

            if (id.HasValue)
                viewModel = _mapper.MapData(_repo.GetById(id.Value));

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddEdit(ProductViewModel viewModel)
        {
            var model = _mapper.MapData(viewModel);

            if (model.Id == 0)
                _repo.Add(model);
            else
                _repo.Update(model);

            _repo.Save();

            return View("Index", GetProducts());
        }

        public ActionResult Delete(int id)
        {
            var model = _repo.GetById(id);

            if(model != null)
                _repo.Delete(model);

            _repo.Save();

            return View("Index", GetProducts());
        }

        private List<ProductViewModel> GetProducts()
        {
            return _repo.Table.ToList().Select(p => _mapper.MapData(p)).ToList();
        }
    }
}