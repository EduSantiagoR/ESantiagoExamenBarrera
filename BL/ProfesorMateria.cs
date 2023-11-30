using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ProfesorMateria
    {
        public static ML.Result GetMateriasAsignadas(int idProfesor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    var query = context.ProfesorMateriaGetMateriasAsignadas(idProfesor);
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
                        result.Message = "No se han podido recuperar las materias asignadas.";
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
        public static ML.Result GetMateriasNoAsignadas(int idProfesor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    var query = context.ProfesorMateriaGetMateriasNoAsignadas(idProfesor);
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
                        result.Message = "No se han podido recuperar las materias no asignadas.";
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
        public static ML.Result Delete(int idProfesor, int idMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    int rowsAffected = context.ProfesorMateriaDelete(idProfesor, idMateria);
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al eliminar el registro.";
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
        public static ML.Result Add(int idProfesor, int idMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    int rowsAffected = context.ProfesorMateriaAdd(idProfesor,idMateria);
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se ha podido agregar la materia.";
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
