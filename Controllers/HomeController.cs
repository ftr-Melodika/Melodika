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




    public IActionResult Cursos()
    {
        Cuenta cuenta = Objeto.StringToObject<Cuenta>(HttpContext.Session.GetString("cuenta"));//Saca de sesion
        if (cuenta == null){
            return RedirectToAction("Login", "Account");
        }
        else{
            List<Curso> cursos = new List<Curso>();
            cursos = BD.getCursos();
            ViewBag.cursos = cursos;
            return View("Cursos");
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
