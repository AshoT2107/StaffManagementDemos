using Microsoft.AspNetCore.Mvc;
using StaffManagement.Models;
using StaffManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffManagement.Controllers
{
    
       
    public class HomeController:Controller
    {
        private readonly IStaffRepository _staffrepository;

        public HomeController(IStaffRepository staffrepository)
        {
            _staffrepository = staffrepository;
        }

        public ViewResult Index()
        {
            HomeIndexViewModels indexViewModels = new()
            {
                Staffs = _staffrepository.GetAll()
            };
            return View(indexViewModels);
        }
        public ViewResult Details(int? id)
        {
            Staff model = _staffrepository.Get(id??1);

            HomeDetailsViewModels detailsViewModels = new()
            {
                Staffs = model,
                Title = "Staff Details"
            };
            return View(detailsViewModels);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Staff staff)
        {
            if(ModelState.IsValid)
            {
                var newStaff = _staffrepository.Create(staff);

                return RedirectToAction("details", new { id = newStaff.Id });
            }
            return View();
        }


    }
}
