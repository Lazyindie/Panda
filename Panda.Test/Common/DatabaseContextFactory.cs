using Microsoft.EntityFrameworkCore;
using Panda.EntityFramework;
using System;
namespace Panda.Test.Common;

internal class DatabaseContextFactory
{
    public static DatabaseContext CreateDatabase()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new DatabaseContext(options);
    }
}
