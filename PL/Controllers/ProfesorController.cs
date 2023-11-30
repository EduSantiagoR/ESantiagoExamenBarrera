using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ProfesorController : Controller
    {
        // GET: Profesor
        public ActionResult GetAll()
        {
            ML.Result result = BL.Profesor.GetAll();
            ML.Profesor profesor = new ML.Profesor();
            profesor.Profesores = result.Objects;
            return View(profesor);
        }
        [HttpGet]
        public ActionResult Form(int? idProfesor)
        {
            if(idProfesor != null)
            {
                ML.Result result = BL.Profesor.GetById(idProfesor.Value);
                ML.Profesor profesor = result.Object as ML.Profesor;
                return View(profesor);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Profesor profesor)
        {
            if(profesor.IdProfesor == 0)
            {
                ML.Result result = BL.Profesor.Add(profesor);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Agregado correctamente.";
                }
                else
                {
                    ViewBag.Mensaje = result.Message;
                }
            }
            else
            {
                ML.Result result = BL.Profesor.Update(profesor);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Actualizado correctamente.";
                }
                else
                {
                    ViewBag.Mensaje = result.Message;
                }
            }
            return PartialView("Modal");
        }
        public ActionResult Delete(int idProfesor)
        {
            ML.Result result = BL.Profesor.Delete(idProfesor);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Eliminado correctamente.";
            }
            else
            {
                ViewBag.Mensaje = result.Message;
            }
            return PartialView("Modal");
        }
    }
}