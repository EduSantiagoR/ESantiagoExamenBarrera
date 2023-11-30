using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Boleta
    {
        public static ML.Result GetBoleta(int idAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    var query = context.BoletaGetCalificaciones(idAlumno);
                    if(query!= null)
                    {
                        result.Objects = new List<object>();
                        foreach(var item in query)
                        {
                            ML.Boleta boleta = new ML.Boleta();
                            boleta.IdMateria = item.IdMateria;
                            boleta.Nombre = item.Nombre;
                            boleta.Calificacion = item.Calificacion.Value;
                            result.Objects.Add(boleta);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetByNumeroBoleta(int numeroBoleta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    var query = context.BoletaGetMaterias(numeroBoleta);
                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var item in query)
                        {
                            ML.Boleta calificacion = new ML.Boleta();
                            calificacion.IdMateria = item.IdMateria;
                            calificacion.Nombre = item.Nombre;
                            calificacion.Calificacion = item.Calificacion.Value;
                            result.Objects.Add(calificacion);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al consultar el numero de materia.";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result AsignarCalificacion(decimal calificación, int idMateria, int numeroBoleta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    int rowsAffected = context.BoletaUpdateCalificacion(calificación,idMateria,numeroBoleta);
                    if(rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al asignar calificaciones";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
