using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Profesor
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    var query = (from prof in context.Profesors
                                 select new
                                 {
                                     IdProfesor = prof.IdProfesor,
                                     Nombre = prof.Nombre,
                                     ApellidoPaterno = prof.ApellidoPaterno,
                                     ApellidoMaterno = prof.ApellidoMaterno
                                 });
                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var obj in query)
                        {
                            ML.Profesor profesor = new ML.Profesor();
                            profesor.IdProfesor = obj.IdProfesor;
                            profesor.Nombre = obj.Nombre;
                            profesor.ApellidoPaterno = obj.ApellidoPaterno;
                            profesor.ApellidoMaterno = obj.ApellidoMaterno;
                            result.Objects.Add(profesor);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se ha podido recuperar los registros de profesores.";
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
        public static ML.Result GetById(int idProfesor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    var query = (from prof in context.Profesors
                                 where prof.IdProfesor == idProfesor
                                 select new
                                 {
                                     IdProfesor = prof.IdProfesor,
                                     Nombre = prof.Nombre,
                                     ApellidoPaterno = prof.ApellidoPaterno,
                                     ApellidoMaterno = prof.ApellidoMaterno
                                 }).AsEnumerable().FirstOrDefault();
                    if(query != null)
                    {
                        result.Object = new object();
                        ML.Profesor profesor = new ML.Profesor();
                        profesor.IdProfesor = query.IdProfesor;
                        profesor.Nombre = query.Nombre;
                        profesor.ApellidoPaterno = query.ApellidoPaterno;
                        profesor.ApellidoMaterno = query.ApellidoMaterno;
                        result.Object = profesor;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se ha podido recuperar el registro.";
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
        public static ML.Result Delete(int idProfesor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    var query = (from prof in context.Profesors where prof.IdProfesor == idProfesor select prof).First();
                    context.Profesors.Remove(query);
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
        public static ML.Result Add(ML.Profesor profesor)
        {
            ML.Result result = new ML.Result();
            try
            {

                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    DL.Profesor nuevoProfesor = new DL.Profesor();
                    nuevoProfesor.Nombre = profesor.Nombre;
                    nuevoProfesor.ApellidoPaterno = profesor.ApellidoPaterno;
                    nuevoProfesor.ApellidoMaterno = profesor.ApellidoMaterno;
                    context.Profesors.Add(nuevoProfesor);
                    int rowsAffected = context.SaveChanges();
                    if(rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se ha podido agregar el nuevo profesor.";
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
        public static ML.Result Update(ML.Profesor profesor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoExamenBarreraEntities context = new DL.ESantiagoExamenBarreraEntities())
                {
                    var query = (from prof in context.Profesors where prof.IdProfesor == profesor.IdProfesor select prof).SingleOrDefault();
                    if( query != null )
                    {
                        query.Nombre = profesor.Nombre;
                        query.ApellidoPaterno = profesor.ApellidoPaterno;
                        query.ApellidoMaterno = profesor.ApellidoMaterno;
                        int rowsAffected = context.SaveChanges();
                        if( rowsAffected > 0 )
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = "No se ha podido actualizar el registro.";
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
