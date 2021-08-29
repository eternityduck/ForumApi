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
            return View(await _service.GetAll());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] PostModel postModel)
        {
           
                await _service.AddAsync(postModel);
                return RedirectToAction(nameof(Index));

              
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}