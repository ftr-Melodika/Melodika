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
        Usuario user = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("usuario"));
        if (user == null){
            return View("Login");
        }
        else{
            return RedirectToAction("ElegirUsuario", "Account");
        } 

    }



        public IActionResult LoginGuardar(string correo, string contraseña)
    {
        int idUser = BD.logIn(correo, contraseña);

        if(HttpContext.Session.GetString("usuario") != null)
        {
            ViewBag.mensaje = "Ya hay un usuario logueado. Para ingresar denuevo primero salga de sesion";
            return View("Login");
        }

        if(idUser > 0 ){
           Cuenta usuarioLogueado;
            usuarioLogueado = BD.getCuenta(idUser);
            if(usuarioLogueado != null){
                string usuario = Objeto.ObjectToString(usuarioLogueado);
                HttpContext.Session.SetString("usuario", usuario);
                return RedirectToAction("ElegirUsuario", "Account");
            }
            else{
                ViewBag.mensaje = "No se pudo obtener el usuario";
                return View("Login");
            }
        }
        else{
            ViewBag.mensaje = "La contraseña o el usuario estan mal";
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
    public IActionResult logout()
    {

        HttpContext.Session.Remove("usuario");
        ViewBag.mensaje = "Usted salió correctamente de la sesión.";
        return RedirectToAction("Index", "Home"); 
    }

}