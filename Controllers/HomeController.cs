using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using melodika.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace Melodika.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult todasLasPaginas()
    {
        return View("allPages");
    }



    public IActionResult Configuracion()
    {
        return View("Configuracion");
    }

    

    public IActionResult CancionesPopulares()
    {
        List<Cancion> canciones = new List<Cancion>();
        canciones = BD.GetCanciones();
        ViewBag.canciones = canciones;
        return View("CancionesPopulares");
    }




    public IActionResult CancionesPopularesAdentroHard()
    {
        // Aquí puedes agregar lógica para cargar la canción específica si es necesario
        // Por ahora, simplemente devolvemos la vista
        return View("CancionesPopularesAdentroHard");
    }

    public IActionResult Cursos()
    {
        Cuenta cuenta = Objeto.StringToObject<Cuenta>(HttpContext.Session.GetString("cuenta"));//Saca de sesion
        Usuario usuario = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("usuario"));
        
        if (cuenta == null){
            return RedirectToAction("Login", "Account");
        }
        else{
<<<<<<< HEAD
            if(instrumentos.Count == 0){
                ViewBag.mensaje = "Primero debe agregar un instrumento en su perfil para ver los cursos disponibles";
                return View("~/Views/Account/SeleccionarInstrumento.cshtml");
=======
            if(usuario != null){
                List<Instrumento> instrumentos = BD.GetInstrumentos(usuario.IdUsuario);
                if(instrumentos.Count == 0){
                    ViewBag.mensaje = "Primero debe agregar un instrumento en su perfil para ver los cursos disponibles";
                    return RedirectToAction("SeleccionarInstrumento", "Account");
                }
                else{            
                    List<Curso> cursos = BD.getCursos();
                    ViewBag.cursos = cursos;
                    return View("Cursos");
                }
>>>>>>> bc8ab6d24f7b8f18a9fde7a3b91e0790afdb44ec
            }
            else{
                ViewBag.mensaje = "Primero, seleccione un usuario";
                return RedirectToAction("MostrarUsuario", "Account");
            }
       
        }
    }





        public IActionResult LogrosYRecompensas()
    {
        return View("LogrosYRecompensas");
    }
        public IActionResult PreguntasFrecuentes()
    {
        return View("PreguntasFrecuentes");
    }
       public IActionResult Contactos()
    {
        return View("Contactos");
    }

    public IActionResult CursoAdentro(int id)
    {
        Cuenta cuenta = Objeto.StringToObject<Cuenta>(HttpContext.Session.GetString("cuenta"));
        if (cuenta == null)
        {
            return RedirectToAction("Login", "Account");
        }

        Curso curso = BD.getCursoPorId(id);
        if (curso == null)
        {
            return RedirectToAction("Cursos");
        }

        return View("CursoAdentro", curso);
    }

    public IActionResult Feedback()
    {
        return View("Feedback");
    }

}
