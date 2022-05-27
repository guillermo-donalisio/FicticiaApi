using Api_Bitsion.Core.Interfaces;
using Api_Bitsion.Core.Models.Clients;
using Api_Bitsion.DataAccess.UnitOfWork.Interface;
using Api_Bitsion.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api_Bitsion.Controllers;

[ApiController]
public class ClientController : Controller
{
    private readonly IClientBusiness _clientBusiness;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public ClientController(IClientBusiness clientBusiness, IMapper mapper, IUnitOfWork uow)
    {
        this._clientBusiness = clientBusiness;
        this._mapper = mapper;
        this._uow = uow;
    }

    // GET List/Clients
    [HttpGet]    
    [Route("List/Clients")]
    public async Task<IActionResult> GetAllClients() 
    {
        try
        {
            var client = _clientBusiness.FindClientAsync(c => c.IsActive == true);
            return client != null ? Ok(_mapper.Map<IEnumerable<ClientGetModelDTO>>(client)) 
                                    : NotFound("The list of clients has not been found");                
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }           
    }

    // GET List/ClientsById
    [HttpGet]        
    [Route("List/ClientsById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            if(id == 0)
                return NotFound("Please, set an ID.");

            var client = await _clientBusiness.GetByIdClientdAsync(id);
            return client != null ? Ok(_mapper.Map<ClientGetModelDTO>(client)) 
                                    : NotFound("Client doesn't exists");            
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // POST Create/Client
    [HttpPost]       
    [Route("Create/Client")]
    public async Task<IActionResult> Create([FromBody] ClientCreateModelDTO model)
    {          
        if(ModelState.IsValid)
        {
            try
            {
                // validations                    
                if(string.IsNullOrEmpty(model.FullName))
                {
                    return Ok("Full name required");                    
                }                   

                // request                    
                await _clientBusiness.InsertClientAsync(_mapper.Map<Client>(model));
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }            
        return Ok(new 
        {
            Status = "Success",
            Message = "Client creation successfully!"
        });                
    }

    // PUT Update/Client/{id}
    [HttpPut]       
    [Route("Update/Client/{id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] ClientUpdateModelDTO model)
    { 
        if (id != model.ID)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new
            {
                Status = "Error",
                Message = "Id number not found!"
            });
        }  

        if(ModelState.IsValid)
        {
            try
            {
                // validations                    
                if(string.IsNullOrEmpty(model.FullName))
                {
                    return Ok("Name required");                    
                }

                // request    
                var client = await _clientBusiness.GetByIdClientdAsync(id);
                _mapper.Map(model,client); 
                var updated = await _clientBusiness.UpdateClientAsync(client);

                if(updated == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        Status = "Error",
                        Message = "Error updating data "
                    });              
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        return Ok(new 
        {
            Status = "Success",
            Message = "Client updated successfully!"
        }); 
    }

    // DELETE Delete/Client/{id}
    [HttpDelete]       
    [Route("Delete/Client/{id}")]
    public async Task<IActionResult> Delete(int? id)
    {
        // validation
        if(id == 0)
            return NotFound("Please, set a valid ID.");

        try
        {
            var client = await _clientBusiness.GetByIdClientdAsync(id.Value);

            if(client == null)
                return NotFound("Client not found or doesn't exist.");

            // request
            await _clientBusiness.SoftDeleteClientAsync(client, id);
            await _clientBusiness.UpdateClientAsync(client);            
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Ok(new 
        {
            Status = "Success",
            Message = "Client deleted successfully!"
        }); 
    }
}
