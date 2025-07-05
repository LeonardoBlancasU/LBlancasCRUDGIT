using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CRUDGITEntities context = new DL.CRUDGITEntities())
                {
                    var query = (from usuario in context.USUARIOs
                                 select new
                                 {
                                     IdUsuario = usuario.IdUsuario,
                                     Nombre = usuario.Nombre,
                                     Apellido = usuario.Apellido,
                                     FechaNacimiento = usuario.FechaNacimiento
                                 });
                    result.Objects = new List<object>();
                    if (query != null && query.ToList().Count > 0)
                    {
                        foreach (var item in query)
                        {
                            ML.Usuario usuarioBL = new ML.Usuario();
                            usuarioBL.IdUsuario = item.IdUsuario;
                            usuarioBL.Nombre = item.Nombre;
                            usuarioBL.Apellido = item.Apellido;
                            usuarioBL.FechaNacimiento = item.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            result.Objects.Add(usuarioBL);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron Usuarios";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CRUDGITEntities context = new DL.CRUDGITEntities())
                {
                    DL.USUARIO usuarioDB = new DL.USUARIO();
                    usuarioDB.Nombre = usuario.Nombre;
                    usuarioDB.Apellido = usuario.Apellido;
                    usuarioDB.FechaNacimiento = DateTime.ParseExact(usuario.FechaNacimiento, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
                    context.USUARIOs.Add(usuarioDB);
                    int rowsAffected = context.SaveChanges();
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CRUDGITEntities context = new DL.CRUDGITEntities())
                {
                    var query = (from usuarioDB in context.USUARIOs
                                 where usuarioDB.IdUsuario == usuario.IdUsuario
                                 select usuarioDB).SingleOrDefault();
                    if (query != null)
                    {
                        query.Nombre = usuario.Nombre;
                        query.Apellido = usuario.Apellido;
                        query.FechaNacimiento = DateTime.ParseExact(usuario.FechaNacimiento, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
                        int rowsAffected = context.SaveChanges();
                        if (rowsAffected > 0)
                        {
                            
                            context.USUARIOs.Remove(query);
                            context.SaveChanges();
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro al Usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CRUDGITEntities contex = new DL.CRUDGITEntities())
                {
                    var usuariobyid = (from usuarioDB in contex.USUARIOs
                                       where usuarioDB.IdUsuario == IdUsuario
                                       select new
                                       {
                                           IdUsuario = usuarioDB.IdUsuario,
                                           Nombre = usuarioDB.Nombre,
                                           Apellido = usuarioDB.Apellido,
                                           FechaNacimiento = usuarioDB.FechaNacimiento,
                                       }).SingleOrDefault();
                    if (usuariobyid != null)
                    {
                        result.Objects = new List<object>();


                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = usuariobyid.IdUsuario;
                        usuario.Nombre = usuariobyid.Nombre;
                        usuario.Apellido = usuariobyid.Apellido;
                        usuario.FechaNacimiento = (usuariobyid.FechaNacimiento).ToString();

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CRUDGITEntities contex = new DL.CRUDGITEntities())
                {
                    var query = (from a in contex.USUARIOs
                                 where a.IdUsuario == IdUsuario
                                 select a).First();
                    contex.USUARIOs.Remove(query);
                    contex.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }

}

