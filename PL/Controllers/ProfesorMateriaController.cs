using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ProfesorMateriaController : Controller
    {
        public ActionResult GetMateriasAsignadas(int idProfesor, string nombreProfesor)
        {
            Session["idProfesor"] = idProfesor;
            Session["nombreProfesor"] = nombreProfesor;
            ML.Result result = BL.ProfesorMateria.GetMateriasAsignadas(idProfesor);
            ML.Materia materia = new ML.Materia();
            materia.Materias = result.Objects;
            return View(materia);
        }
        public ActionResult GetMateriasNoAsignadas()
        {
            ML.Result result = BL.ProfesorMateria.GetMateriasNoAsignadas(int.Parse(Session["idProfesor"].ToString()));
            ML.Materia materia = new ML.Materia();
            materia.Materias = result.Objects;
            return View(materia);
        }
        public ActionResult AsignarMateria(List<int> materias)
        {
            int contador = 0;
            foreach(int materia in materias)
            {
                ML.Result result = BL.ProfesorMateria.Add(int.Parse(Session["idProfesor"].ToString()), materia);
                if (result.Correct)
                {
                    contador++;
                    ViewBag.Mensaje = $"Se han asignado un total de {contador} materias.";
                }
            }
            return PartialView("Modal");
        }
        public ActionResult Delete(int idProfesor, int idMateria)
        {
            ML.Result result = BL.ProfesorMateria.Delete(idProfesor, idMateria);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Se ha eliminado correctamente";
            }
            else
            {
                ViewBag.Mensaje = result.Message;
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult Boleta(int numeroBoleta)
        {
            Session["numeroBoleta"] = numeroBoleta;
            ML.Result result = BL.Boleta.GetByNumeroBoleta(numeroBoleta);
            ML.Boleta boleta = new ML.Boleta();
            boleta.Calificaciones = result.Objects;
            return View(boleta);
        }
        [HttpPost]
        public ActionResult Boleta(List<int> materias, List<decimal> calificaciones)
        {
            int index = 0;
            int numeroBoleta = int.Parse(Session["numeroBoleta"].ToString());
            foreach(int materia in materias)
            {
                ML.Result result = BL.Boleta.AsignarCalificacion(calificaciones[index], materia, numeroBoleta);
                index++;
                if (result.Correct)
                {
                    ViewBag.Mensaje = $"Has cambiado la calificación de {index} materias.";
                }
                else
                {
                    ViewBag.Mensaje = result.Message;
                }
            }
            return PartialView("Modal");
        }
    }
}