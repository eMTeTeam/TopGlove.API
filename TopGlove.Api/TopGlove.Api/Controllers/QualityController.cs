using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TopGlove.Api.Data;
using TopGlove.Api.Model;

namespace TopGlove.Api.Controllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class QualityController : ControllerBase
    {
        private readonly ProductQualityDbContext _dbContext;

        public QualityController(ProductQualityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Quality
        [HttpGet("GetAllDetails")]
        public IEnumerable<ProductQuality> GetAllDetails()
        {
            return _dbContext.ProductQualities;
        }

        // GET: api/Quality/5
        //[HttpGet("{user}", Name = "Get")]
        [HttpGet("GetMaxCount")]
        public int GetMaxCount(string user)
        {
            try
            {
                var res = _dbContext.ProductQualities.Where(a => a.User == user && a.CreatedDateTime.Date == DateTime.Today.Date);
                if (res.Any())
                {
                    return res.Max(a => a.SerialNumber);
                }
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }

        // POST: api/Quality
        [HttpPost("AddQualityDetail")]
        public IActionResult AddQualityDetail([FromBody] ProductQuality product)
        {
            product.ID = Guid.NewGuid();
            // product.CreatedDateTime = DateTime.UtcNow;

            try
            {
                _dbContext.ProductQualities.Add(product);
                _dbContext.SaveChanges();
                return Ok(product);
            }
            catch
            {
                return BadRequest("Something went wrong, Details not added");
            }

        }

        // PUT: api/Quality/5
        [HttpPost("Update")]
        public IActionResult Update([FromBody] ProductQuality productQuality)
        {
            try
            {
                //Guid.TryParse(productQuality.ID, out Guid result);

                var res = _dbContext.ProductQualities.FirstOrDefault(a => a.ID == productQuality.ID);

                if (res != null)
                {
                    res.DefectDetails = productQuality.DefectDetails;
                    res.Factory = productQuality.Factory;
                    res.FiringOrRework = productQuality.FiringOrRework;
                    res.Quality = productQuality.Quality;
                    res.Size = productQuality.Size;
                    res.TypeOfFormer = productQuality.TypeOfFormer;
                    res.Shift = productQuality.Shift;

                    _dbContext.ProductQualities.Update(res);
                    _dbContext.SaveChanges();
                }

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong, item not updated" + ex.Message);
            }

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                Guid.TryParse(id, out Guid result);

                var res = _dbContext.ProductQualities.FirstOrDefault(a => a.ID == result);
                if (res != null)
                {
                    _dbContext.ProductQualities.Remove(res);
                    _dbContext.SaveChanges();
                }

                return Ok(true);
            }
            catch
            {
                return BadRequest("Something went wrong, item not deleted");
            }
        }

        [HttpPost("FilteredItems")]
        public IActionResult GetProductQualityWithUser(RequestModel requestModel)
        {
            var response = _dbContext.ProductQualities.Where(a => a.CreatedDateTime.Date >= requestModel.FromDate.Date
                        && a.CreatedDateTime.Date <= requestModel.ToDate.Date);

            if (!string.IsNullOrWhiteSpace(requestModel.User) && response.Any())
            {
                response = response.Where(a => a.User == requestModel.User);
            }

            if (!string.IsNullOrWhiteSpace(requestModel.Factory) && response.Any())
            {
                response = response.Where(a => a.Factory == requestModel.Factory);
            }

            if (!string.IsNullOrWhiteSpace(requestModel.Quality) && response.Any())
            {
                response = response.Where(a => a.Quality == requestModel.Quality);
            }

            if (!string.IsNullOrWhiteSpace(requestModel.Defect) && response.Any())
            {
                response = response.Where(a => a.DefectDetails == requestModel.Defect);
            }

            if (!string.IsNullOrWhiteSpace(requestModel.WorkStation) && response.Any())
            {
                response = response.Where(a => a.WorkStation == requestModel.WorkStation);
            }

            return Ok(response);
        }
    }
}
