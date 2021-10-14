using Business.Apstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WepAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductReviewsController : Controller
    {
        IProductReviewsService _productReviewsService;

        public ProductReviewsController(IProductReviewsService productReviewsService)
        {
            _productReviewsService = productReviewsService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _productReviewsService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("add")]
        public IActionResult Add(ProductComment productReview )
        {
            var result = _productReviewsService.Add(productReview);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

    }
    
}
