using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Alumno
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    var query = (from alum in context.Alumnoes
                                 select new
                                 {
                                     IdAlumno = alum.IdAlumno,
                                     Nombre = alum.Nombre,
                                     Paterno = alum.ApellidoPaterno,
                                     Materno = alum.ApellidoMaterno
                                 });
                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var obj in query)
                        {
                            ML.Alumno alumno = new ML.Alumno();
                            alumno.IdAlumno = obj.IdAlumno;
                            alumno.Nombre = obj.Nombre;
                            alumno.ApellidoPaterno = obj.Paterno;
                            alumno.ApellidoMaterno = obj.Materno;
                            result.Objects.Add(alumno);
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
        public static ML.Result GetById(int idAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    var query = (from alumno in context.Alumnoes where alumno.IdAlumno == idAlumno 
                                 select new
                                 {
                                     IdAlumno = alumno.IdAlumno,
                                     Nombre = alumno.Nombre,
                                     Paterno = alumno.ApellidoPaterno,
                                     Materno = alumno.ApellidoMaterno
                                 }).AsEnumerable().FirstOrDefault();
                    if(query != null)
                    {
                        result.Object = new object();
                        ML.Alumno alumno = new ML.Alumno();
                        alumno.IdAlumno = query.IdAlumno;
                        alumno.Nombre = query.Nombre;
                        alumno.ApellidoPaterno = query.Paterno;
                        alumno.ApellidoMaterno = query.Materno;
                        result.Object = alumno;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se ha podido obtener el registro del alumno.";
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
        public static ML.Result Delete(int idAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    var query = (from alumno in context.Alumnoes where alumno.IdAlumno == idAlumno select alumno).First();
                    context.Alumnoes.Remove(query);
                    int rowsAffected = context.SaveChanges();
                    if(rowsAffected > 0)
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
        public static ML.Result Add(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    DL.Alumno nuevoAlumno = new DL.Alumno();
                    nuevoAlumno.Nombre = alumno.Nombre;
                    nuevoAlumno.ApellidoPaterno = alumno.ApellidoPaterno;
                    nuevoAlumno.ApellidoMaterno = alumno.ApellidoMaterno;
                    context.Alumnoes.Add(nuevoAlumno);
                    int rowsAffected = context.SaveChanges();
                    if(rowsAffected > 0 )
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se ha podido agregar el nuevo alumno.";
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
        public static ML.Result Update(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    var query = (from alu in context.Alumnoes where alu.IdAlumno == alumno.IdAlumno select alu).SingleOrDefault();
                    if(query != null)
                    {
                        query.Nombre = alumno.Nombre;
                        query.ApellidoPaterno = alumno.ApellidoPaterno;
                        query.ApellidoMaterno = alumno.ApellidoMaterno;
                        int rowsAffected = context.SaveChanges();
                        if(rowsAffected > 0 )
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = "Error al tratar de actualizar el alumno.";
                        }
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
