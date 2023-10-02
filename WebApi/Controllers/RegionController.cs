using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models.Domain;
using WebApi.Models.DTO;

//we have inject dbcontext in our application using di so we can use dbcontext inside the controller through constructor injection

// DTO AND DOMAIN MODEL
// DTO is a data transfer object used to transfer object between different layers . are subset of the properties in domain model or entity
// designed to used for specific purpose such as transfer data over network
//advantage of dto performance/security/ versionong/ seperation of concerns

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase {

        private readonly WalksDbContext dbContext;// TO REUSE IN ACTION METHODS
        public RegionController(WalksDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        //get: HTTP/portnumber/api:because its a api app/region
        [HttpGet]
        public IActionResult getAllRegion()
        {
            // we use dbcontext to talk to region and return all region in alist


            // without DTOs
            // var regions = dbContext.Regions.ToList();
            // return Ok(regions);
            /** var regions = new List<Region>
             {
                 new Region
                 {
                     Id =Guid.NewGuid(),
                     Name=" ZZZZ",
                     Code="123",
                     RegionImageUrl="FFFFFFFFF"
                 },
                  new Region
                 {
                     Id =Guid.NewGuid(),
                     Name=" ABCDEE",
                     Code="12456",
                     RegionImageUrl="ACVFER"
                 }
             };**/


            //with DTOs (best practice)

            //get data from db-domain model
            var region = dbContext.Regions.ToList();

            //Map domain model to dto // in DTO
            var regionDTO = new List<RegionDTO>();
            foreach (var regions in region)
            {
                regionDTO.Add(new RegionDTO()
                {
                    Id = regions.Id,
                    Code = regions.Code,
                    Name = regions.Name,
                    RegionImageUrl = regions.RegionImageUrl,
                });
            }

            //return dto // SEND DTO TO THE CLIENT

            return Ok(regionDTO);

        }
        // get single region by id
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult getRegionById([FromRoute] Guid id) {
            // without DTO
            //var regions = dbContext.Regions.Find(id); // find method used only for primary key
            // other method
            {/* var  regions  = dbContext.Regions.FirstOrDefault(r => r.Id == id); // use it for any property in the table
            if(regions == null) { 
                return NotFound();
            }
           
            
                return Ok(regions);
            */
            }
            // WITH DTO
            var regionsDomain = dbContext.Regions.FirstOrDefault(r => r.Id == id); // use it for any property in the table
            if (regionsDomain == null)
            {
                return NotFound();
            }
            // map region domain to region dto
            var RegionDTO = new RegionDTO {
                Id = regionsDomain.Id,
                Code = regionsDomain.Code,
                Name = regionsDomain.Name,
                RegionImageUrl = regionsDomain.RegionImageUrl,
            };

            // return dto to client
            return Ok(RegionDTO);

        }
        // post method to add new region
        [HttpPost]
        public IActionResult addNewRegion([FromBody] AddNewRegionDTO addNewRegionDTO)
        {
            // convert dto to domain model
            var newRegionModel = new Region
            {
                Code = addNewRegionDTO.Code,
                Name = addNewRegionDTO.Name,
                RegionImageUrl = addNewRegionDTO.RegionImageUrl,
            };

            // use domain model to create region
            dbContext.Regions.Add(newRegionModel);
            dbContext.SaveChanges();
            //map  domain model to dto
            var regionDTO = new RegionDTO
            {
                Id = newRegionModel.Id,
                Code = newRegionModel.Code,
                Name = newRegionModel.Name,
                RegionImageUrl = newRegionModel.RegionImageUrl,
            };
            return CreatedAtAction(nameof(getRegionById), new { id = regionDTO.Id }, regionDTO); // related to 201 response which gives location in response of api (link)
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult RemoveRegion ([FromRoute] Guid id)
        { 
            var region = dbContext.Regions.Find(id);
            if (region == null)
            {
                return BadRequest();
            }
            dbContext.Regions.Remove(region); 
            dbContext.SaveChanges();  
            return Ok();
        

        }
    }

}
