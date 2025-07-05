using ML;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetAll();
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }

            return View(usuario);
        }

        [HttpGet]
        public ActionResult Formulario(int? IdUsuario) 
        {
            ML.Usuario usuario = new ML.Usuario();
            if(IdUsuario != null && IdUsuario > 0)
            {
                ML.Result result = BL.Usuario.GetById(IdUsuario.Value);
                if(result.Correct == true)
                {
                    usuario = (ML.Usuario)result.Object;
                }
            }
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Formulario(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            if(usuario.IdUsuario == 0)
            {
                result = BL.Usuario.Add(usuario);

                if(result.Correct == true)
                {
                    TempData["Success"] = "Usuario Agregado Correctamente";
                    return RedirectToAction("GetAll");
                }
                else
                {
                    TempData["Error"] = "Error al agregar el Usuario" + result.ErrorMessage;
                }
            }
            else
            {
                result = BL.Usuario.Update(usuario);
                if(result.Correct == true)
                {
                    TempData["Success"] = "Usuario Actualizado Correctamente";
                    return RedirectToAction("GetAll");
                }
                else
                {
                    TempData["Error"] = "Error al actualizar el Usuario" + result.ErrorMessage;
                }
            }
            return View(usuario);
        }
        [HttpDelete]
        public ActionResult Delete(int IdUsuario) 
        {
            ML.Result result = BL.Usuario.Delete(IdUsuario);
            if(result.Correct == true)
            {
                TempData["Success"] = "Usuario Eliminado Correctamente";
            }
            else
            {
                TempData["Error"] = "Error al eliminar el Usuario" + result.ErrorMessage;
            }
            return RedirectToAction("GetAll");
        }
    }
}