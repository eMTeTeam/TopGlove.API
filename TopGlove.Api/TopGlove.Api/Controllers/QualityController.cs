﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopGlove.Api.Data;
using TopGlove.Api.Model;

namespace TopGlove.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualityController : ControllerBase
    {
        private ProductQualityDbContext dbContext;
        public QualityController(ProductQualityDbContext _dbContext)
        {
            dbContext = _dbContext;

        }
        // GET: api/Quality
        [HttpGet]
        public IEnumerable<ProductQuality> Get()
        {
            return dbContext.ProductQualities;
        }

        // GET: api/Quality/5
        [HttpGet("{user}", Name = "Get")]
        public int Get(string user)
        {
            var res = dbContext.ProductQualities.Where(a => a.user == user);
            return res.Max(a => a.SerialNumber);
        }

        // POST: api/Quality
        [HttpPost]
        public IActionResult Post([FromBody] ProductQuality product)
        {
            product.ID = Guid.NewGuid();
            product.CreatedDateTime = DateTime.UtcNow;

            try
            {
                dbContext.ProductQualities.Add(product);
                dbContext.SaveChanges();
                return new OkObjectResult(product);
            }
            catch
            {
                return BadRequest("Something went wrong, Details not added");
            }

        }

        // PUT: api/Quality/5
        [HttpPut]
        public IActionResult Put([FromBody] ProductQuality productQuality)
        {
            try
            {
                //Guid.TryParse(productQuality.ID, out Guid result);

                var res = dbContext.ProductQualities.Where(a => a.ID == productQuality.ID).FirstOrDefault();

                res.DefectDetails = productQuality.DefectDetails;
                res.Factory = productQuality.Factory;
                res.FiringOrRework = productQuality.FiringOrRework;
                res.Quality = productQuality.Quality;
                res.Size = productQuality.Size;
                res.TypeOfFormer = productQuality.TypeOfFormer;

                if (res != null)
                {
                    dbContext.ProductQualities.Update(res);
                    dbContext.SaveChanges();
                }
                return new OkObjectResult(res);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong, item not updated" + ex.Message);
            }

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                Guid.TryParse(id, out Guid result);

                var res = dbContext.ProductQualities.Where(a => a.ID == result).FirstOrDefault();

                if (res != null)
                {
                    dbContext.ProductQualities.Remove(res);
                    dbContext.SaveChanges();
                }
                return new OkResult();
            }
            catch
            {
                return BadRequest("Something went wrong, item not deleted");
            }
        }

        [HttpPost("FilteredItems")]
        public IActionResult GetProductQualityWithUser(RequestModel requestModel)
        {
            var response = dbContext.ProductQualities.Where(a => a.CreatedDateTime.Date >= requestModel.FromDate.Date
                        && a.CreatedDateTime.Date <= requestModel.ToDate.Date);

            if (!string.IsNullOrWhiteSpace(requestModel.User))
            {
                response = response.Where(a => a.user == requestModel.User);
            }

            return new OkObjectResult(response);
        }

        [HttpPost("FilterWithUser")]
        public IActionResult FilterWithUser(string user)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(user))
                {
                    var response = dbContext.ProductQualities.Where(a => a.user == user);
                    return new OkObjectResult(response);
                }
                return Ok();
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
