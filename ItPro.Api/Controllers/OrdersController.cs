using System.Net.Mime;
using AutoMapper;
using ItPro.Api.Models;
using ItPro.Core.Exceptions;
using ItPro.Core.Helpful;
using ItPro.Core.Orders;
using ItPro.Core.Repository;
using ItPro.Core.Statistics;
using ItPro.Data.Entities;
using ItPro.Data.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ItPro.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class OrdersController : ControllerBase
{
    private readonly IRepository<Order> repository;
    private readonly IOrderStatistics orderStatistics;
    private readonly IMapper mapper;

    public OrdersController(
        IRepository<Order> repository,
        IMapper mapper,
        IOrderStatistics orderStatistics)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.orderStatistics = orderStatistics;
    }

    /// <summary>
    /// Получение списка всех заказов.
    /// </summary>
    /// <returns>Список клиентов.</returns>
    [HttpGet("list")]
    [ProducesResponseType(typeof(PagedObject<OrderModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> List([FromQuery] OrderQueryParameters queryString)
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

    /// <summary>
    /// Получить сумму заказов со статусом выполнен по каждому клиенту, произведенных в день рождения клиента.
    /// </summary>
    /// <returns></returns>
    [HttpGet("birthday-receipts-statistics")]
    [ProducesResponseType(typeof(PagedObject<BirthDaysReceiptStatistics>), StatusCodes.Status200OK)]
    public async Task<IActionResult> BirthdayReceiptStatistics()
    {
        var result = await this.orderStatistics.GetBirthdayReceiptsStatisticsAsync(HttpContext.RequestAborted);

        return Ok(result);
    }

    [HttpGet("hourly-average-receipt-sum")]
    [ProducesResponseType(typeof(IEnumerable<HourlyAverageReceiptSumStatistics>), StatusCodes.Status200OK)]
    public async Task<IActionResult> HourlyAverageReceiptSum(Status status)
    {
        var result = await this.orderStatistics.GetHourlyAverageReceiptSumStatisticsAsync(
            status,
            HttpContext.RequestAborted);

        return Ok(result);
    }
}