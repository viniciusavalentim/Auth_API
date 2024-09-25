using JwtApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtApplication.Database
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
        public DbSet<UserModel> Users {  get; set; } 
    }
}
 