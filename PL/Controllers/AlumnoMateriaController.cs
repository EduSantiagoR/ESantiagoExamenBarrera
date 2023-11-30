using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoMateriaController : Controller
    {
        public ActionResult GetMateriasAsignadas(int idAlumno, string nombreAlumno)
        {
            ML.Result result = BL.AlumnoMateria.GetMateriasAsignadas(idAlumno);
            Session["idAlumno"] = idAlumno;
            Session["nombreAlumno"] = nombreAlumno;
            ML.Materia materia  = new ML.Materia();
            materia.Materias = result.Objects;
            return View(materia);
        }
        public ActionResult GetMateriasNoAsignadas()
        {
            ML.Result result = BL.AlumnoMateria.GetMateriasNoAsignadas(int.Parse(Session["idAlumno"].ToString()));
            ML.Materia materia = new ML.Materia();
            materia.Materias = result.Objects;
            return View(materia);
        }
        public ActionResult AsignarMaterias(List<int> materias)
        {
            int contador = 0;
            foreach(int materia in materias)
            {
                ML.Result result = BL.AlumnoMateria.Add(int.Parse(Session["idAlumno"].ToString()),materia);
                if (result.Correct)
                {
                    contador++;
                    ViewBag.Mensaje = $"Has agregado un total de {contador} materias.";
                }
                else
                {
                    ViewBag.Mensaje = "No se ha podido asignar materias.";
                }
            }
            return PartialView("Modal");
        }
        public ActionResult Delete(int idMateria)
        {
            ML.Result result = BL.AlumnoMateria.Delete(int.Parse(Session["idAlumno"].ToString()), idMateria);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Se ha eliminado la materia.";
            }
            else
            {
                ViewBag.Mensaje = result.Message;
            }
            return PartialView("Modal");
        }
        public ActionResult Boleta()
        {
            ML.Result result = BL.Boleta.GetBoleta(int.Parse(Session["idAlumno"].ToString()));
            ML.Boleta boleta = new ML.Boleta();
            boleta.Calificaciones = result.Objects;
            return View(boleta);
        }
    }
}