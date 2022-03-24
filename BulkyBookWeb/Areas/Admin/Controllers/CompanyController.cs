﻿using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.iRepository;
using BulkyBook.Models;
using BulkyBooks.Models;
using BulkyBooks.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;

        public CompanyController(IUnitOfWork UnitOfWork, ApplicationDbContext Db)
        {
            _unitOfWork = UnitOfWork;
            _db = Db;
        }

        public IActionResult Index()
        {
            return View();
        }


        //Get
        public IActionResult Upsert(int? id)
        {
            Company company = new();

            if (id == null || id == 0)
            {
                return View(company);
            }
            else
            {
                company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
                return View(company);
            }
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                    TempData["success"] = "Product Created successfully";
                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                    TempData["success"] = "Product Updated successfully";
                }

                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(obj);
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            //there seems to be a bug in here

            //var companyList = _unitOfWork.Company.GetAll();

            var companyList = _db.Companies.ToList();

            return Json(new { data = companyList });
        }

        //post
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Company.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting. Not found" });
            }

            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}