using elaw_desenvolvedor_junior.Application.Dtos;
using elaw_desenvolvedor_junior.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace elaw_desenvolvedor_junior.Api
{
    [ApiController]
    [Route("api/clients")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _svc;
        public ClientController(IClientService svc) => _svc = svc;

        [HttpPost]
        public ActionResult Create([FromBody] CreateClientDto dto)
        {
            if (dto == null)
                return BadRequest("Request body cannot be null.");

            try
            {
                var created = _svc.CreateClient(dto);
                var response = new
                {
                    message = "Created successfully",
                    data = created
                };

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = created.Id },
                    response
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    "An unexpected error occurred while creating the user."
                );
            }
        }


        [HttpGet]
        public ActionResult GetAll()
        {
            var all = _svc.GetClients();
            if (!all.Any())
            {
                var response = new
                {
                    message = "No users found."
                };
                return Ok(response);
            }
            return Ok(all);
        }


        [HttpGet("{id:guid}")]
        public ActionResult<GetClientDto> GetById(Guid id)
        {
            try
            {
                var dto = _svc.GetClientById(id);
                if (dto == null) return NotFound("User not found");
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred while retrieving the user.");
            }
        }

        [HttpPut("{id:guid}")]
        public ActionResult Update(Guid id, [FromBody] UpdateClientDto dto)
        {
            if (dto == null)
                return BadRequest("Request body cannot be null.");

            try
            {
                var updated = _svc.UpdateClient(id, dto);
                if (updated == null)
                    return NotFound($"User with ID {id} not found.");

                var response = new
                {
                    message = "User updated successfully",
                    data = updated
                };

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    "An unexpected error occurred while updating the user."
                );
            }
        }


        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var existing = _svc.GetClientById(id);
                if (existing == null)
                    return NotFound($"User with ID {id} not found.");

                _svc.DeleteClientById(id);

                var response = new
                {
                    message = "User deleted successfully",
                    data = existing
                };

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    "An unexpected error occurred while deleting the user."
                );
            }
        }
    }

}
