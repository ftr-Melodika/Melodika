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
        List<Curso> cursos = new List<Curso>();
        cursos = BD.getCursos();
        ViewBag.cursos = cursos;
        return View("Cursos");
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

}
