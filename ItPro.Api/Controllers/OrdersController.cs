using System.Net.Mime;
using AutoMapper;
using ItPro.Api.Models;
using ItPro.Core.Exceptions;
using ItPro.Core.Orders;
using ItPro.Core.Repository;
using ItPro.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ItPro.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class OrdersController : ControllerBase
{
    private readonly IRepository<Order> repository;
    private readonly IMapper mapper;

    public OrdersController(
        IRepository<Order> repository,
        IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Получение списка всех заказов.
    /// </summary>
    /// <returns>Список клиентов.</returns>
    [HttpGet("list")]
    [ProducesResponseType(typeof(PagedObject<OrderModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> List(OrderQueryParameters queryString)
    {
        try
        {
            var result = await this.repository.GetAllAsync(queryString, HttpContext.RequestAborted);
            var mappedResult = this.mapper.Map<PagedObject<OrderModel>>(result);
        
            return Ok(mappedResult);
        }
        catch (Exception e)
        {
            return Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Получить заказ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <returns>Информация о заказе.</returns>
    [HttpGet("get-by-id")]
    [ProducesResponseType(typeof(OrderModel), StatusCodes.Status200OK)]
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
            
            return Ok(this.mapper.Map<OrderModel>(result));
        }
        catch (Exception e)
        {
            return Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Создать новый заказ.
    /// </summary>
    /// <param name="order">Информация для добавления заказа.</param>
    /// <returns>Информация о заказе.</returns>
    [HttpPost("create")]
    [ProducesResponseType(typeof(OrderModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> Create(OrderModel order)
    {
        var entity = this.mapper.Map<Order>(order);

        try
        {
            var result = await this.repository.CreateAsync(entity, HttpContext.RequestAborted);

            return StatusCode(StatusCodes.Status201Created, this.mapper.Map<Order>(result));
        }
        catch (Exception e) when(e is AlreadyExistsException or NotFoundException)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Редактировать существующий заказ.
    /// </summary>
    /// <param name="order">Информация для обновления заказа.</param>
    /// <returns>Информация о заказе.</returns>
    [HttpPatch("update")]
    [ProducesResponseType(typeof(OrderModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> Update(OrderModel order)
    {
        var entity = this.mapper.Map<Order>(order);

        try
        {
            var result = await this.repository.UpdateAsync(entity, HttpContext.RequestAborted);

            return Ok(this.mapper.Map<OrderModel>(result));
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
    /// Удалить заказ по идентификатору.
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