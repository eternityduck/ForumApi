using System;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _service;
        private readonly IPostService _postService;
        private readonly UserManager<User> _userManager;
        public CommentController(ICommentService service, UserManager<User> userManager, IPostService postService)
        {
            _postService = postService;
            (_service, _userManager) = (service, userManager);
        }


        public async Task<IActionResult> Index()
        {
            var models = await _service.GetAllAsync();
            return View(models);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [FromForm] CommentModel commentModel)
        {
            //TODO
            var user = await _userManager.GetUserAsync(HttpContext.User);
            commentModel.Author = user;
            commentModel.CreatedAt = DateTime.Now;
            commentModel.Post = await _postService.GetByIdAsync(id);
            await _service.AddAsync(commentModel);
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {

            var post = await _service.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _service.GetByIdAsync(id));
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Title, Text, Author, CreatedAt")] CommentModel comment)
        {
            await _service.UpdateAsync(comment);
            return RedirectToAction(nameof(Index));
        }
       
    }
}