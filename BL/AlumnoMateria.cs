using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AlumnoMateria
    {
        public static ML.Result GetMateriasAsignadas(int idAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    var query = context.GetMateriasAsignadas(idAlumno);
                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var obj in query)
                        {
                            ML.Materia materia = new ML.Materia();
                            materia.IdMateria = obj.IdMateria;
                            materia.Nombre = obj.Nombre;
                            result.Objects.Add(materia);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se ha podido recuperar las materias asignadas.";
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
        public static ML.Result GetMateriasNoAsignadas(int idAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    var query = context.GetMateriasNoAsignadas(idAlumno);
                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var obj in query)
                        {
                            ML.Materia materia = new ML.Materia();
                            materia.IdMateria = obj.IdMateria;
                            materia.Nombre = obj.Nombre;
                            result.Objects.Add(materia);
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se ha podido recuperar las materias no asignadas.";
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
        public static ML.Result Delete(int idAlumno, int idMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    int rowsAffected = context.AlumnoMateriaDelete(idAlumno,idMateria);
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se ha podido eliminar el registro.";
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
        public static ML.Result Add(int idAlumno, int idMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    int rowsAffected = context.AlumnoMateriaAdd(idAlumno,idMateria);
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se ha podido asignar la materia.";
                    }
                }
            }
            catch( Exception ex )
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
