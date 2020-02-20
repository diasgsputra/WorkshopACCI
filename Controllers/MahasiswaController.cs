using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WorkshopASP.DAL;
using WorkshopASP.Models;

namespace WorkshopASP.Controllers
{
    public class MahasiswaController : Controller
    {
        private IMahasiswa _mhs;
        public MahasiswaController(IMahasiswa mhs)
        {
            _mhs = mhs;
        }
        public IActionResult Index(){
            var models = _mhs.GetAll();
            return View(models);
        }

        public IActionResult Details(string id){
            var model = _mhs.GetById(id);
            return View(model);
        }
        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public IActionResult Create(Mahasiswa mhs){
            try{
                _mhs.Insert(mhs);
                return RedirectToAction("Index");
            }catch(Exception ex){
                ViewBag.Pesan = $"<span class='alert alert-danger'>{ex.Message}</span>";
            }
            return View();
        }

        public IActionResult Update(string id){
            var model = _mhs.GetById(id);
            return View("Details",model);
        }

        [HttpPost]
        public IActionResult Update(Mahasiswa mhs){
            try{
                _mhs.Update(mhs);
                return RedirectToAction("Index");
            }catch(Exception ex){
                ViewBag.Error = ex.Message;
                return View("Details",mhs);
            }
        }

        public IActionResult Delete(string id){
            var model = _mhs.GetById(id);
            return View(model);
        }

        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeletePost(string id){
            try{
                _mhs.Delete(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex){
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}