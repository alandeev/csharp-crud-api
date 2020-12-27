using Microsoft.EntityFrameworkCore;

namespace CrudApi.Models {
  public class UserContext : DbContext {
    public UserContext(DbContextOptions<UserContext> options) : base(options) {}
    public DbSet<UserItem> Users { get; set; }
  }
}