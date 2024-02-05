using System.Net.Mime;
using AutoMapper;
using ItPro.Api.Models;
using ItPro.Core.Exceptions;
using ItPro.Core.Repository;
using ItPro.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ItPro.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class UsersController : ControllerBase
{
    private readonly IRepository<Client> repository;
    private readonly IMapper mapper;

    public UsersController(
        IRepository<Client> repository,
        IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Получение списка всех клиентов.
    /// </summary>
    /// <returns>Список клиентов.</returns>
    [HttpGet("list")]
    [ProducesResponseType(typeof(IReadOnlyCollection<ClientModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> List()
    {
        try
        {
            var result = await this.repository.GetAllAsync(HttpContext.RequestAborted);
            var mappedResult = this.mapper.Map<IReadOnlyCollection<ClientModel>>(result);
        
            return Ok(mappedResult);
        }
        catch (Exception e)
        {
            return Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Получить клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <returns>Информация о клиенте.</returns>
    [HttpGet("get-by-id")]
    [ProducesResponseType(typeof(ClientModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await this.repository.GetByIdAsync(id, HttpContext.RequestAborted);

            if (result is null)
            {
                return NotFound();
            }
            
            return Ok(this.mapper.Map<ClientModel>(result));
        }
        catch (Exception e)
        {
            return Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Создать нового клиента.
    /// </summary>
    /// <param name="client">Информация для добавления клиента.</param>
    /// <returns>Информация о клиенте.</returns>
    [HttpPost("create")]
    [ProducesResponseType(typeof(ClientModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> Create(ClientModel client)
    {
        var entity = this.mapper.Map<Client>(client);

        try
        {
            var result = await this.repository.CreateAsync(entity, HttpContext.RequestAborted);

            return StatusCode(StatusCodes.Status201Created, this.mapper.Map<Client>(result));
        }
        catch (AlreadyExistsException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Редактировать существующего клиента.
    /// </summary>
    /// <param name="client">Информация для обновления клиента.</param>
    /// <returns>Информация о клиенте.</returns>
    [HttpPatch("update")]
    [ProducesResponseType(typeof(ClientModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> Update(ClientModel client)
    {
        var entity = this.mapper.Map<Client>(client);

        try
        {
            var result = await this.repository.UpdateAsync(entity, HttpContext.RequestAborted);

            return Ok(this.mapper.Map<ClientModel>(result));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Удалить клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await this.repository.DeleteAsync(id, HttpContext.RequestAborted);

            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}