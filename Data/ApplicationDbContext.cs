using AzureOcrFlutterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AzureOcrFlutterAPI.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
