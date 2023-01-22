using EmployeeManager.Application.UseCases.Employee.Commands;
using EmployeeManager.Application.UseCases.Employee.Commands.DeleteEmployee;
using EmployeeManager.Application.UseCases.Employee.Commands.UpdateEmployee;
using EmployeeManager.WepApi.Controllers.Common;
using EmployeeManager.WepApi.Dto.Common;
using EmployeeManager.WepApi.Dto.Employee;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.WepApi.Controllers;

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
    [HttpPost(ApiRouters.Employee.Create)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ValidationError>))]
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

    /// <summary>
    /// Удалить сотрудника
    /// </summary>
    /// <param name="id">идентификатор сотрудника</param>
    /// <returns>Идентификатор удаленного сотрудника</returns>
    /// <response code="201">Успешно</response>
    /// <response code="400">Ошибка валидации данных</response>
    [HttpDelete(ApiRouters.Employee.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ValidationError>))]
    public async Task<ActionResult<Guid>> DeleteEmployee(Guid id)
    {
        var deleteQuery = new DeleteEmployeeCommand()
        {
            Id = id
        };

        var deletedEmployeeId = await Mediator.Send(deleteQuery);
        return Ok(deletedEmployeeId);
    }

    /// <summary>
    /// Обновить данные сотрудника
    /// </summary>
    /// <param name="id">идентификатор сотрдуника</param>
    /// <param name="requestData">обновленные данные</param>
    /// <returns>идентификатор сотрудника</returns>
    [HttpPut(ApiRouters.Employee.Update)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ValidationError>))]
    public async Task<ActionResult<Guid>> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeDto requestData)
    {
        var updateEmployeeCommand = new UpdateEmployeeCommand()
        {
            Id = id,
            LastName = requestData.LastName,
            FirstName = requestData.FirstName,
            Patronymic = requestData.Patronymic,
            Email = requestData.Email,
            Salary = requestData.Salary,
            DepartmentsId = requestData.DepartmentsId
        };

        var updatedEmployeeId = await Mediator.Send(updateEmployeeCommand);

        return Ok(updatedEmployeeId);
    }
}