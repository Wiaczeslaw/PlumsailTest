using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Test.Domain.Entities;

namespace Test.DAL.Sql
{
    public class DatabaseContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions, ILoggerFactory loggerFactory = null) : base(dbContextOptions) => _loggerFactory = loggerFactory;
        
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormItem> FormItems { get; set; }
        public DbSet<FormItemSelectValue> FormItemSelectValues { get; set; }
        public DbSet<FormItemTemplate> FormItemTemplates { get; set; }
        public DbSet<FormTemplate> FormTemplates { get; set; }
        
    }
}