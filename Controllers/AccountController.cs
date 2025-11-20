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
        Cuenta cuenta = Objeto.StringToObject<Cuenta>(HttpContext.Session.GetString("cuenta"));//Saca de sesion
        if (cuenta == null){
            return View("Login");
        }
        else{
            return RedirectToAction("MostrarUsuario", "Account");
        } 

    } //



        public IActionResult LoginGuardar(string correo, string contraseña)
    {
        int idCuenta = BD.logIn(correo, contraseña);

        if(HttpContext.Session.GetString("cuenta") != null)
        {
            ViewBag.mensaje = "Ya hay una cuenta logueada. Para ingresar denuevo primero salga de sesion";
            return View("Login");
        }
        //verificar cuenta
        else if(idCuenta > 0 ){
           Cuenta cuentaLogueada;
            cuentaLogueada = BD.getCuenta(idCuenta);
            if(cuentaLogueada != null){
                string cuenta = Objeto.ObjectToString(cuentaLogueada); //lo paso a string
                HttpContext.Session.SetString("cuenta", cuenta); //poner en sesion
                return RedirectToAction("MostrarUsuario", "Account");
            }
            else{
                ViewBag.mensaje = "No se pudo obtener la cuenta";
                return View("Login");
            }
        }
        else{
            ViewBag.mensaje = "La contraseña o el correo estan mal";
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

    public IActionResult MostrarUsuario()
    {
        Cuenta cuenta = Objeto.StringToObject<Cuenta>(HttpContext.Session.GetString("cuenta"));
        int idCuenta = cuenta.IdCuenta;
        List<Usuario> usuarios = BD.GetUsuariosCuentaSimple(idCuenta);
        ViewBag.usuarios = usuarios;
        return View("MostrarUsuario");
    }
    public IActionResult logout()
    {

        HttpContext.Session.Remove("cuenta");
        ViewBag.mensaje = "Usted salió correctamente de la sesión.";
        return RedirectToAction("Index", "Home"); 
    }

}