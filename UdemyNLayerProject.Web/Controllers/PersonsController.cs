using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.Web.ApiService;
using UdemyNLayerProject.Web.DTOs;

namespace UdemyNLayerProject.Web.Controllers
{
    public class PersonsController : Controller
    {
        private readonly PersonApiService _personApiService;
        private readonly IMapper _mapper;

        public PersonsController(PersonApiService personApiService, IMapper mapper)
        {
            _personApiService = personApiService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var people = await _personApiService.GetAllAsync();
            return View(_mapper.Map<PersonDto>(people));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonDto personDto)
        {
            await _personApiService.AddAsync(personDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var person = await _personApiService.GetByIdAsync(id);
            return View(_mapper.Map<PersonDto>(person));
        }

        [HttpPost]
        public async Task<IActionResult> Update(PersonDto personDto)
        {
            await _personApiService.Update(personDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _personApiService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
