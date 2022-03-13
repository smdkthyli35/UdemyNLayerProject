﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.Web.ApiService;
using UdemyNLayerProject.Web.DTOs;

namespace UdemyNLayerProject.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductApiService _productApiService;
        private readonly IMapper _mapper;

        public ProductsController(ProductApiService productApiService, IMapper mapper)
        {
            _productApiService = productApiService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productApiService.GetAllAsync();
            return View(_mapper.Map<ProductDto>(products));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            var product = await _productApiService.AddAsync(productDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _productApiService.GetByIdAsync(id);
            return View(_mapper.Map<ProductDto>(product));
        }

        public async Task<IActionResult> Update(ProductDto productDto)
        {
            await _productApiService.Update(productDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _productApiService.Remove(id);
            return RedirectToAction("Index");
        }

    }
}
