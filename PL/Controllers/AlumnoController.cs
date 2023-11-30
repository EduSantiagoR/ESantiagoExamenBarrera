using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Result result = BL.Alumno.GetAll();
            ML.Alumno alumno = new ML.Alumno();
            alumno.Alumnos = result.Objects;
            return View(alumno);
        }
        [HttpGet]
        public ActionResult Form(int? idAlumno)
        {
            if(idAlumno != null)
            {
                ML.Result result = BL.Alumno.GetById(idAlumno.Value);
                ML.Alumno alumno = result.Object as ML.Alumno;
                return View(alumno);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
            if(alumno.IdAlumno == 0)
            {
                ML.Result result = BL.Alumno.Add(alumno);
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
                ML.Result result = BL.Alumno.Update(alumno);
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
        public ActionResult Delete(int idAlumno)
        {
            ML.Result result = BL.Alumno.Delete(idAlumno);
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