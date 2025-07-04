using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result=new ML.Result();
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

            }catch (Exception ex)
            {
                result.Correct=false;               
                result.ErrorMessage=ex.Message;
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
