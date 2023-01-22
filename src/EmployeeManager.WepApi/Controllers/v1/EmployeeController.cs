using EmployeeManager.Application.UseCases.Employee.Commands;
using EmployeeManager.Application.UseCases.Employee.Commands.DeleteEmployee;
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
    [HttpPost(ApiRouters.V1.Employee.Create)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    [HttpDelete(ApiRouters.V1.Employee.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> DeleteEmployee(Guid id)
    {
        var deleteQuery = new DeleteEmployeeQuery()
        {
            Id = id
        };

        var deletedEmployeeId = await Mediator.Send(deleteQuery);
        return Ok(deletedEmployeeId);
    }
}