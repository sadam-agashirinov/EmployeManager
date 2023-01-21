using EmployeeManager.Application.UseCases.Employee.Commands;
using EmployeeManager.WepApi.Controllers.Common;
using EmployeeManager.WepApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.WepApi.Controllers.v1;

[ApiController]
[Produces("application/json")]
public class EmployeeController : BaseController
{
    /// <summary>
    /// Создать сотрудника
    /// </summary>
    /// <param name="requestData">Данные запроса</param>
    /// <returns>Идентификатор нового сотрудника</returns>
    /// <response code="201">Успешно</response>
    /// <response code="400">Ошибка валидации данных</response>
    [HttpPost(ApiRouters.V1.Employee.CreateEmployee)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    public async Task<ActionResult<Guid>> CreateEmployee([FromBody] CreateEmployeeDto requestData)
    {
        var createCommand = new CreateEmployeeCommand()
        {
            LastName = requestData.LastName,
            FirstName = requestData.FirstName,
            Patronymic = requestData.Patronymic,
            Email = requestData.Email,
            Salary = requestData.Salary,
            DepartmentsId = requestData.DepartmentsId
        };
        var employeeId = await Mediator.Send(createCommand);
        return Ok(employeeId);
    }
}