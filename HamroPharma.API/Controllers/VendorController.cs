using HamroPharma.API.Models.Domains;
using HamroPharma.API.Models.DTOs;
using HamroPharma.API.Repositories.Implementation;
using HamroPharma.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HamroPharma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorRespository _vendorRepository;

        public VendorController(VendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetAllVendors()
        {
            try
            {
                var vendors = await _vendorRepository.GetAllAysnc();
                return Ok(vendors);
            }
            catch
            {
                return StatusCode(500, "Failed to retrieve vendors");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Vendor>> AddVendor(VendorDto vendorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var vendor = new Vendor
                {
                    Id = vendorDto.Id,
                    Name = vendorDto.Name,
                    Address = vendorDto.Address,
                    City = vendorDto.City,
                    companyName = vendorDto.CompanyName,
                    Balance = vendorDto.Balance
                };

                var addedVendor = await _vendorRepository.AddAysnc(vendor);
                return CreatedAtAction(nameof(GetVendor), new { id = addedVendor.Id }, addedVendor);
            }
            catch
            {
                return StatusCode(500, "Failed to add the vendor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(Guid id)
        {
            var vendor = await _vendorRepository.GetByIdAysnc(id);

            if (vendor == null)
            {
                return NotFound();
            }

            return Ok(vendor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditVendor(Guid id, VendorDto vendorDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingVendor = await _vendorRepository.GetByIdAysnc(id);

                if (existingVendor == null)
                {
                    return NotFound("Vendor not found");
                }

                existingVendor.Name = vendorDto.Name;
                existingVendor.Address = vendorDto.Address;
                existingVendor.City = vendorDto.City;
                existingVendor.companyName = vendorDto.CompanyName;
                existingVendor.Balance = vendorDto.Balance;

                await _vendorRepository.UpdateAysnc(existingVendor);

                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Failed to update the vendor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(Guid id)
        {
            try
            {
                var vendor = await _vendorRepository.GetByIdAysnc(id);
                if (vendor == null)
                {
                    return NotFound(); // Vendor not found
                }

                var deletedVendor = await _vendorRepository.DeleteAysnc(vendor);
                if (deletedVendor == null)
                {
                    return NotFound(); // Vendor not found
                }

                return NoContent(); // Deletion successful
            }
            catch
            {
                return StatusCode(500, "Failed to delete the vendor"); // Deletion failed due to exception
            }
        }
    }
}
