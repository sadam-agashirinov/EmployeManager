namespace EmployeeManager.Persistence;

public class DbInitializer
{
    public static async Task Initialize(AppDbContext dbContext)
    {
        await dbContext.Database.EnsureCreatedAsync();
    }
}