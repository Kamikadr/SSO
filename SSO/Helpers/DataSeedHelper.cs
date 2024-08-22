using Microsoft.EntityFrameworkCore;
using SSO.Entities;

namespace SSO.Helpers;

public static class DataSeedHelper
{
    public static void SeedInitialData(ModelBuilder modelBuilder)
    {
        //var baseUser = new User("admin@mail.ru", "ADMIN")
        var role = new Role("Admin");
        var permissions = new List<Permission>
        {
            new Permission("AddService")
        };
        modelBuilder.Entity<Permission>().HasData(permissions);
    }
    
    
}