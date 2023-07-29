using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Neo.API.Data;
using Neo.API.Models.Domain;
using Neo.API.Models.DTO;

namespace Neo.API.Controllers
{

    //http://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NeoWalksDbContext dbContext;

        //we are going to create dependency injection 
        //using contructor injection

        //We create the constructor using ctor
        //and inject the Dbcontext into it 
        //we create and asiign a private field using ctrl .
        public RegionsController(NeoWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //get all regions
        [HttpGet]
        public IActionResult GetAll()
        {
            //to access the region table to get all the data
            //create a variable called region regions (var = variable)
            //so the variable region calls the db and gets data for Region in a list

            //get data fromdatabase -Domain models 
            var regions = dbContext.Regions.ToList();

            //map(convert) Domain models to DTOs
            var regionsDto = new List<RegionDto>();
            //loop on the region and convert to Dtos
            //iterating through all the region inside the loop
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl

                });

            }

            //Return DTOs

            return Ok(regionsDto);

        }

        //getting regions by Id
        [HttpGet]
        [Route("{id:Guid}")]

        public IActionResult GetById([FromRoute] Guid id)
        {
            //there are 2 ways to pass this logic.
            //var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);


            var region = dbContext.Regions.Find(id);

            if (region == null)
            {
                return NotFound();
            }

            //map the region domain model to region dto
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            //return Dto back to client
            return Ok(regionDto);
        }



        //POST to create a new region

        [HttpPost]
        //we use the dto inside the create method since its coming from the client.
        //since its coming from the client we use [frombody]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //map or convert the Dto to Domain model
            var region = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            //Use Domain model to create region
            //let use dbcontext and add the domain model to the database
            dbContext.Regions.Add(region);
            dbContext.SaveChanges();

            //map domain model back to dto
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl

            };

            return CreatedAtAction(nameof(GetById), new { id = region.Id }, region);


        }


        //updating regions by Id
        [HttpPut]
        [Route("{id:Guid}")]

        public IActionResult GetById([FromRoute] Guid id, UpdateRegionRequestDto updateRegionRequestDto)
        {
            //there are 2 ways to pass this logic.
            //var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            //getting regions by Id
            var region = dbContext.Regions.Find(id);
            if (region == null)
            {
                return NotFound();
            }

            //Update the properties of the region
            region.Code = updateRegionRequestDto.Code;
            region.Name = updateRegionRequestDto.Name;
            region.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;


            //Use Domain model to update region
            //let use dbcontext and add the domain model to the database
            dbContext.Regions.Update(region);
            dbContext.SaveChanges();


            //map domain model back to dto
            var regionDto = new RegionDto
            {
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl

            };

            //return Dto back to client
            return Ok(regionDto);

        }

        //delete region by id
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id, DeleteRegionRequestDto deleteRegionRequestDto)
        {
            var region = dbContext.Regions.Find(id);
            if (region == null)
            {
                return NotFound();
            }

                //Use Domain model to update region
                //let use dbcontext and add the domain model to the database
                dbContext.Regions.Remove(region);
                dbContext.SaveChanges();


                //return Dto back to client
                return Ok("Region Deleted Successfully.");
          
        }

    }

}








