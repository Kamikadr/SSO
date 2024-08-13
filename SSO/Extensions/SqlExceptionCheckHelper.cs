using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace SSO.Extensions;

public static class SqlExceptionCheckHelper
{
    public static bool IsUniqueConstraintViolation(this DbUpdateException exception)
    {
        if (exception.InnerException is PostgresException postgresException)
        {
            return postgresException.SqlState == "23505";
        }

        return false;
    }
}