namespace Melodika.Controllers;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using melodika.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
public class AccountController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public AccountController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

        public IActionResult Login()
    {
        return View("Login");
    }



        public IActionResult LoginGuardar(string correo, string contraseña)
    {
        int idUser = BD.logIn(correo, contraseña);

        if(idUser > 0){
            Cuenta cuentaLogueada;
            cuentaLogueada = BD.getCuenta(idUser);
            string usuario = Objeto.ObjectToString(cuentaLogueada);
            HttpContext.Session.SetString("usuario", Objeto.ObjectToString (usuario));
            return RedirectToAction("Cursos", "Home"); 
        }
        else if(HttpContext.Session.GetString("idUsuario") != null )
        {
            ViewBag.mensaje = "Ya hay un usuario logueado. Para ingresar denuevo primero salga de sesion";
            return View("Login");
        }
        else{
            ViewBag.mensaje("La contraseña o el usuario estan mal");
            return View("Login");
        }


    }

        public IActionResult CrearCuenta()
    {

        return View("CrearCuenta");
    }

        public IActionResult CrearCuentaGuardar(string correo, string contra1, string contra2, string username)
    {

        return View("CrearCuenta");
    }


    public IActionResult CrearUsuario()
    {
        return View("CrearUsuario");
    }

    public IActionResult ElegirUsuario()
    {
        return View("ElegirUsuario");
    }

}