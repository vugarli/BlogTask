using BlogTask2.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

namespace BlogTask2.Persistance
{
	public class ApplicationDbContext : IdentityDbContext<AppUser>
	{

        public ApplicationDbContext(IConfiguration configuration)
        {
			Configuration = configuration;
		}


		public DbSet<Blog> Blogs { get; set; }

		public IConfiguration Configuration { get; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlServer(Configuration["ConnectionStrings:MSSQL"]);
		}


	}
}
