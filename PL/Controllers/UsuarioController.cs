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
            return View();
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

                }
            }
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Formulario(ML.Usuario usuario)
        {
            
            return View(usuario);
        }
        [HttpDelete]
        public ActionResult Delete(int IdUsuario) 
        {

            return RedirectToAction("GetAll");
        }
    }
}