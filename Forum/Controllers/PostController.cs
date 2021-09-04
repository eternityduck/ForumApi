using System;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Models;
using Forum.ViewModels.PostViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Forum.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _service;
        private readonly UserManager<User> _userManager;
        public PostController(IPostService service, UserManager<User> userManager) => 
            (_service, _userManager) = (service, userManager);
        
        
        public async Task<IActionResult> Index()
        {
            var models = await _service.GetAllAsync();
            return View(models);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] PostModel postModel)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            postModel.AuthorName = user.Name;
            await _service.AddAsync(postModel);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id, Title, Text, AuthorName, AuthorId, Comments")] PostModel post)
        {
            await _service.UpdateAsync(post);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                return View(await _service.GetByIdAsync(id));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        
    }
    }
