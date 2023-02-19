using MagicVilla_webapi.Models;
using Microsoft.EntityFrameworkCore;
namespace MagicVilla_webapi.data{
    public class ApplicationDbContext:DbContext{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options){
            
        }
        public DbSet<Villa>Villa{get;set;}
    }
}
