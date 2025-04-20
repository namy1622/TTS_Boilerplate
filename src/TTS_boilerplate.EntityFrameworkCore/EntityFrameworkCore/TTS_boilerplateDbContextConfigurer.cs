using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace TTS_boilerplate.EntityFrameworkCore
{
    public static class TTS_boilerplateDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<TTS_boilerplateDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<TTS_boilerplateDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
