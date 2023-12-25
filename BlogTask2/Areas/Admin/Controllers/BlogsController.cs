using BlogTask2.Areas.Admin.Models;
using BlogTask2.Areas.Admin.Models.Blogs;
using BlogTask2.Entities;
using BlogTask2.FileRelated;
using BlogTask2.Persistance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogTask2.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize]

    public class BlogsController : Controller
	{

        private ApplicationDbContext _dbContext { get; set; }
        private IWebHostEnvironment _env { get; set; }

        public BlogsController(ApplicationDbContext dbContext,IWebHostEnvironment env)
        {
            _dbContext = dbContext;
			_env = env;
        }


        public async Task<IActionResult> Index()
		{
			var page = 1;
			var pagesize = 3;


			var pagedData = await _dbContext.Blogs.Skip((page-1)*pagesize).Take(pagesize).ToListAsync();
			var count = await _dbContext.Blogs.CountAsync();


			
			var next = Url.Action(nameof(PagedBlog), new { page = page+1, pagesize = pagesize });
			var prev = Url.Action(nameof(PagedBlog), new { page = page-1, pagesize = pagesize });



			var model = new PagedEntityModel<IEnumerable<Blog>>(pagedData,page,pagesize,count,next,prev);

			return View(model);
		}


		public async Task<IActionResult> PagedBlog(int page,int pagesize)
		{
			if (!(page > 0 && pagesize > 0)) return BadRequest();

			var pagedData = await _dbContext.Blogs.Skip((page - 1) * pagesize).Take(pagesize).ToListAsync();
			var count = await _dbContext.Blogs.CountAsync();



			var next = Url.Action(nameof(PagedBlog), new { page = page + 1, pagesize = pagesize });
			var prev = Url.Action(nameof(PagedBlog), new { page = page - 1, pagesize = pagesize });



			var model = new PagedEntityModel<IEnumerable<Blog>>(pagedData, page, pagesize, count, next, prev);

			return PartialView("_PartialPaginatedBlogs",model);
		}


		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(BlogCreateVM model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var blog = new Blog
			{
				Title = model.Title,
				Author = model.Author,
				CreatedAt = model.CreatedAt,
				Description = model.Description,
				CoverImageUrl = await model.CoverImage.SaveFileAndReturnNameAsync(PathConstants.BlogImagesPath, _env)
			};

            await _dbContext.Blogs.AddAsync(blog);
			await _dbContext.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}	
		
		public async Task<IActionResult> Update(int id)
		{
            var blog = _dbContext.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog == null) return NotFound();

            var model = new BlogUpdateVM
            {
				Id = id,
				Title = blog.Title,
				Author = blog.Author,
				CreatedAt = blog.CreatedAt,
				Description = blog.Description,
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Update(BlogUpdateVM model, int id)
		{
            var blog = _dbContext.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog == null) return NotFound();

			blog.Title = model.Title;
			blog.Description = model.Description;
			blog.Author = model.Author;
			blog.CreatedAt = model.CreatedAt;

			if (model.CoverImage != null)
			{ 
				blog.CoverImageUrl= await model.CoverImage.SaveFileAndReturnNameAsync(PathConstants.BlogImagesPath,_env);
			}
			await _dbContext.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
			var blog = _dbContext.Blogs.FirstOrDefault(b => b.Id == id);
			if (blog == null) return NotFound();
			blog.Delete();
			await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
		public async Task<IActionResult> RevokeDelete(int id)
        {
			var blog = _dbContext.Blogs.FirstOrDefault(b => b.Id == id);
			if (blog == null) return NotFound();
			blog.ReokeDelete();
			await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
