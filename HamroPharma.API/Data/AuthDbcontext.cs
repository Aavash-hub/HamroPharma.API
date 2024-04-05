using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HamroPharma.API.Data
{
    public class AuthDbcontext : IdentityDbContext
    {
        public AuthDbcontext(DbContextOptions<AuthDbcontext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var ReaderRoleId = "56d409c1-954a-46cf-9eb8-aaf070832bb3]";
            var WriterRoleId = "[32d7a896-ed71-4cad-aa2a-91af6ba2f0ab]";
            //Create Admin and Staff Roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = ReaderRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = ReaderRoleId
                },
                new IdentityRole()
                {
                    Id = WriterRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp= WriterRoleId
                }
            };
            // Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            //Create an Admin User
            var adminUserId = "03de100c-233a-41c7-b6f5-5201232840fd";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "Aavash",
                Email = "Wcaavash@gmail.com",
                NormalizedEmail = "Wcaavash@gmail.com".ToUpper(),
                NormalizedUserName= "Aavash".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Aavash@123");

            builder.Entity<IdentityUser>().HasData(admin);
            //Give Roles To Admin 
            var AdminRoles = new List<IdentityUserRole<String>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = ReaderRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = WriterRoleId
                }

            };
            builder.Entity<IdentityUserRole<String>>().HasData(AdminRoles);
        }
    }
}
