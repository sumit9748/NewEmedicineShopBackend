

using Emedicine.BAL.MedcineBased;
using Emedicine.DAL.DataManupulation;
using Emedicine.DAL.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Emedicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        //couple IMedicinemain interface to call its method
        private readonly IMedicineMain md;
        public MedicineController(IMedicineMain _md)
        {
            md = _md;
        }

        //get medicalshopItems like medicines.
        [HttpGet("medicalShopItems/{medicalshopid}")]
        [EnableCors("AllowOrigin")]
        [Authorize]
        public Task<IEnumerable<Medicine>> GetMedicalShopMedicines(int medicalshopid)
        {
            try
            {
                return md.GetMedicalShopItem(medicalshopid);
            }
            catch (Exception ex)
            {
                IEnumerable<Medicine> medicines = new List<Medicine>();
                return (Task<IEnumerable<Medicine>>)medicines;
            }
        }
        //Add a medicine to database
        [HttpPost("Medicine")]
        [EnableCors("AllowOrigin")]
        [Authorize]

        public async Task<IActionResult> AddMedicine(MedicineVm medicine)
        {
            try
            {
                if (medicine == null)
                    return BadRequest("Object cannot be null");
                if (await md.AddMedicine(medicine))
                {
                    return StatusCode(
                       StatusCodes.Status200OK,
                       "Medicine added successfully");
                }
                else return StatusCode(StatusCodes.Status406NotAcceptable, "Medicine already exists");
            }
            catch (Exception es)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Something went wrong");
            }

        }
        
        //Get medicine by id
        [HttpGet("{id}")]
        [EnableCors("AllowOrigin")]
        [Authorize]

        public Task<Medicine> GetMedicne(int id)
        {
             return md.GetMedicineById(id);
        }
        //Update a medicine by its id
        [HttpPut("{id}")]
        [EnableCors("AllowOrigin")]
        [Authorize]

        public async Task<IActionResult> UpdateMedicine(int id, [FromBody] MedicineVm2 medicine)
        {
            try
            {
                //helps to get the medicine with id parameter
                Medicine med = await md.GetMedicineById(id);
                if (med == null) return NotFound();
                med.Manufacturer = medicine.Manufacturer;
                med.ExpDate= medicine.ExpDate;
                med.Status= medicine.Status;
                med.Discount= medicine.Discount;
                med.ImgUrl= medicine.ImgUrl;
                med.Name= medicine.Name;
                med.Type= medicine.Type;
                med.UnitPrice= medicine.UnitPrice;

                md.UpdateMedicine(med);
                //Ok is under IActionResult
                return Ok("Medicine updated successfully");
            }
            catch (Exception es)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error updating medicine");
            }
        }
        //Delete medicine from database.
        [HttpDelete("{id}")]
        [EnableCors("AllowOrigin")]
        [Authorize]

        public async Task<IActionResult> DeleteMedicine(int id)
        {
            try
            {
                Medicine med = await md.GetMedicineById(id);
                if (med == null) return NotFound();
                md.DeleteMedicine(med);
                return Ok("Medicine removed successfully");
            }
            catch (Exception es)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error deleting medicine");
            }

        }
        [HttpGet("Allmedicines")]
        [EnableCors("AllowOrigin")]
        [Authorize]

        public async Task<IActionResult> GetAllMedicine()
        {
            try
            {
                var medicines = await md.GetAllMedicine();
                return Ok(medicines);
            }
            catch (Exception es)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Something went wrong");
            }

        }

        [HttpGet("GetMedicineBytype/{type}")]
        [EnableCors("AllowOrigin")]
        [Authorize]

        public async Task<IActionResult> GetMedicineByType(string type)
        {
            try
            {
                var medicines = await md.GetMedicineByType(type);
                return Ok(medicines);
            }
            catch (Exception es)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Something went wrong");
            }

        }

    }
}