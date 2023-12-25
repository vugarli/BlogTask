using System.Diagnostics;
using BlogTask2.Models;
using BlogTask2.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogTask2.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _dbContext;

		public HomeController(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IActionResult> Index()
		{
			var model = new IndexVM();
			model.Blogs = await _dbContext.Blogs.Where(b => !b.IsDeleted)
				.Select(b=> new BlogItemVM
				{ 
					Title = b.Title,
					Author = b.Author,
					Description = b.Description,
					CoverImageUrl = b.CoverImageUrl,
					DateString = $"{b.CreatedAt.ToString("MMMM")}  {b.CreatedAt.Day}  {b.CreatedAt.Year.ToString()}"
				})
				.ToListAsync();

			return View(model);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}