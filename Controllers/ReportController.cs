using Assignment3_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Assignment3_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IRepository _repository;
        public ReportController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GenerateReport")]

        public async Task<IActionResult> GenerateReport()
        {
            try
            {
                var products = await _repository.GetProductsAsync();

                var brandReport = products.GroupBy(p => p.Brand.Name)
                                          .Select(g => new { BrandName = g.Key, Count = g.Count() })
                                          .ToList();

                var productTypeReport = products.GroupBy(p => p.ProductType.Name)
                                                .Select(g => new { ProductTypeName = g.Key, Count = g.Count() })
                                                .ToList();

                var activeProducts = products.Where(p => p.IsActive)
                                             .GroupBy(p => new { ProductType = p.ProductType.Name, Brand = p.Brand.Name })
                                             .Select(g => new
                                             {
                                                 ProductTypeName = g.Key.ProductType,
                                                 Brands = g.GroupBy(p => p.Brand.Name).Select(b => new
                                                 {
                                                     BrandName = b.Key,
                                                     Products = b.Select(p => new { p.Name, p.Price }).ToList()
                                                 }).ToList()
                                             })
                                             .ToList();

                return Ok(new { brandReport, productTypeReport, activeProducts });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error, please contact support");
            }
            //try
            //{
            //    var products = await _repository.GetProductsAsync();

            //    var brandReport = products.GroupBy(p => p.Brand.Name)
            //                              .Select(g => new { Brand = g.Key, Count = g.Count() })
            //                              .ToList();

            //    var productTypeReport = products.GroupBy(p => p.ProductType.Name)
            //                                    .Select(g => new { ProductType = g.Key, Count = g.Count() })
            //                                    .ToList();

            //    var activeProducts = products.Where(p => p.IsActive)
            //                                 .GroupBy(p => new { ProductType = p.ProductType.Name, Brand = p.Brand.Name })
            //                                 .Select(g => new
            //                                 {
            //                                     ProductType = g.Key.ProductType,
            //                                     Brand = g.Key.Brand,
            //                                     Products = g.Select(p => new { p.Name, p.Description }).ToList()
            //                                 })
            //                                 .ToList();

            //    return Ok(new { brandReport, productTypeReport, activeProducts });
            //}
            //catch (Exception)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error, please contact support");
            //}
        }
    }
}
