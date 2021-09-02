using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using DAL.Models;
using Forum.ViewModels.PostViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _service;

        public PostController(IPostService service) => _service = service;
        
        
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Post postModel)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PostModel, Post>()).CreateMapper();
            await _service.AddAsync(mapper.Map<PostModel>(postModel));
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}