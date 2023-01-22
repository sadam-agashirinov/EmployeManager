namespace EmployeeManager.WepApi.Dto.Common;

public class ValidationError
{
    public string PropertyName { get; set; } = null!;
    public string ErrorMessage { get; set; } = null!;
}