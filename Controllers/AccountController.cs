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

    } 



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

    public IActionResult CrearCuentaGuardar(string correo, string contra1, string contra2, string username, bool terminos, bool actualizaciones)
    {
        int idCuenta = -1;
        

        if(contra1 != contra2){
            ViewBag.mensaje = "Las contraseñas no coinciden";
            return View("CrearCuenta");
        }
        else{
            string contraseña = contra1;
            idCuenta = BD.crearCuenta(correo, contraseña, username, terminos, actualizaciones);
            switch (idCuenta)
            {
                case -1:
                    ViewBag.mensaje = "El correo ya está en uso";
                    return View("CrearCuenta");
                    break;
                case -2:
                    ViewBag.mensaje = "El nombre de usuario ya está en uso";
                    return View("CrearCuenta");
                    break;
                case -3:
                    ViewBag.mensaje = "Hubo un error al crear la cuenta";
                    return View("CrearCuenta");
                    break;
                default:
                    ViewBag.mensaje = "Cuenta creada correctamente";
                    return RedirectToAction("Login", "Account");
                    break;
            }
            

        }


    }


    public IActionResult CrearUsuario()
    {
        return View("CrearUsuario");
    }

    public IActionResult CrearUsuarioGuardar(string nombre, DateTime fechaNacimiento, string genero)
    {
        Cuenta cuenta = Objeto.StringToObject<Cuenta>(HttpContext.Session.GetString("cuenta"));
        int idCuenta = cuenta.IdCuenta;
        int idUsuario = BD.crearUsuario(nombre, fechaNacimiento, genero, idCuenta);
        switch(idUsuario){
            case -1:
                ViewBag.mensaje = "Ya hay un usuario con ese nombre";
                return View("CrearUsuario");


            case -2:
                ViewBag.mensaje = "Algo salio mal.";
                return View("CrearUsuario");


            default:
                ViewBag.mensaje = "Usuario creado correctamente";
                return RedirectToAction("MostrarUsuario", "Account");

        }

        
    }



    public IActionResult MostrarUsuario()
    {
        Cuenta cuenta = Objeto.StringToObject<Cuenta>(HttpContext.Session.GetString("cuenta"));
        int idCuenta = cuenta.IdCuenta;
        List<Usuario> usuarios = BD.GetUsuariosCuentaSimple(idCuenta);
        ViewBag.usuarios = usuarios;
        return View("MostrarUsuario");
    }

    public IActionResult SeleccionarUsuario(int idUsuario)
    {
        Usuario usuario = BD.GetUsuarioSimple(idUsuario);
        if(usuario != null)
        {
            ViewBag.usuario = usuario;
            string usuarioString = Objeto.ObjectToString(usuario); 
            HttpContext.Session.SetString("usuario", usuarioString); 
            return RedirectToAction("Cursos", "Home");
        }
        else
        {
            ViewBag.mensaje = "No hay usuarios disponibles para esta cuenta";
            return RedirectToAction("MostrarUsuario", "Account");
        }
        //SI HAY UNO LOGUADO, BORRAR AL LOGUADO Y PONER AL OTRO
    }

    public IActionResult SeleccionarInstrumento()
    {
        return View("SeleccionarInstrumento");
    }

    public IActionResult SeleccionarInstrumentoGuardar(int idInstrumento)
    {
        Usuario usuario = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("usuario"));
        int idUsuario = usuario.IdUsuario;
        int reporte = BD.AgregarInstrumentoUsuario(idUsuario, idInstrumento);
        switch(reporte){
            case -1:
                ViewBag.mensaje = "No hay un usuario seleccionado";
                return View("SeleccionarInstrumento");

            case -2:
                ViewBag.mensaje = "Algo salio mal.";
                return View("SeleccionarInstrumento");

            default:
                ViewBag.mensaje = "Instrumento seleccionado correctamente";
                return View("SeleccionarInstrumento");
        }
    }

    public IActionResult logout()
    {
        HttpContext.Session.Remove("cuenta");
        ViewBag.mensaje = "Usted salió correctamente de la sesión.";
        return RedirectToAction("Index", "Home"); 
    }

    public IActionResult InformacionUsuario()
    {
        try
        {
            // Get user from session
            var usuarioString = HttpContext.Session.GetString("usuario");
            if (string.IsNullOrEmpty(usuarioString))
            {
                TempData["ErrorMessage"] = "No se encontró la sesión del usuario. Por favor, inicie sesión nuevamente.";
                return RedirectToAction("Login", "Account");
            }

            // Deserialize user object
            var usuario = Objeto.StringToObject<Usuario>(usuarioString);
            if (usuario == null || usuario.IdUsuario <= 0)
            {
                TempData["ErrorMessage"] = "La información del usuario no es válida. Por favor, inicie sesión nuevamente.";
                return RedirectToAction("Login", "Account");
            }

            // Get complete user information
            var usuarioComplejo = BD.GetUsuarioComplejo(usuario.IdUsuario);
            if (usuarioComplejo == null)
            {
                TempData["ErrorMessage"] = "No se pudo cargar la información del usuario. Por favor, intente nuevamente.";
                return RedirectToAction("Index", "Home");
            }

            // Get user's instruments
            var instrumentosUsuario = BD.GetInstrumentos(usuario.IdUsuario) ?? new List<Instrumento>();
            
            ViewBag.usuario = usuarioComplejo;
            ViewBag.instrumentos = instrumentosUsuario;
            
            return View("InformacionUsuario");
        }
        catch (Exception ex)
        {
            // Log the error (you should implement proper logging)
            Console.WriteLine($"Error en InformacionUsuario: {ex.Message}");
            
            TempData["ErrorMessage"] = "Ocurrió un error al cargar la información del usuario. Por favor, intente nuevamente.";
            return RedirectToAction("Index", "Home");
        }
    }


}