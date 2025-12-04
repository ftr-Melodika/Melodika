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

        public IActionResult Login(string mensaje = "")
    {
        Cuenta cuenta = Objeto.StringToObject<Cuenta>(HttpContext.Session.GetString("cuenta"));//Saca de sesion
        if (cuenta == null){
            if (mensaje != "")
            {
                ViewBag.mensaje = mensaje;
            }
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
                    
                case -2:
                    ViewBag.mensaje = "El nombre de usuario ya está en uso";
                    return View("CrearCuenta");
                   
                case -3:
                    ViewBag.mensaje = "Hubo un error al crear la cuenta";
                    return View("CrearCuenta");
                  
                default:
                    return RedirectToAction("Login", "Account", new { mensaje = "Cuenta creada correctamente" }); 
                   
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
                return RedirectToAction("MostrarUsuario", "Account"); //PONER MENSAJE ACA

        }

        
    }



    public IActionResult MostrarUsuario(string mensaje="")
    {
        Cuenta cuenta = Objeto.StringToObject<Cuenta>(HttpContext.Session.GetString("cuenta"));
        int idCuenta = cuenta.IdCuenta;
        List<Usuario> usuarios = BD.GetUsuariosCuentaSimple(idCuenta);
        ViewBag.usuarios = usuarios;
        if (mensaje!="")
        {
            ViewBag.mensaje = mensaje;
        }
        return View("MostrarUsuario");
    }

    public IActionResult SeleccionarUsuario(int idUsuario)
    {
        Usuario usuario = BD.GetUsuarioSimple(idUsuario);
        
 
        if (usuario != null)
        {
            HttpContext.Session.Remove("usuario");
            ViewBag.usuario = usuario;
            string usuarioString = Objeto.ObjectToString(usuario); 
            HttpContext.Session.SetString("usuario", usuarioString); 
            
            return RedirectToAction("Cursos", "Home");
        }
        else
        {

            string mensaje = "El usuario seleccionado no está disponible.";
            return RedirectToAction("MostrarUsuario", "Account", new { mensaje = mensaje });
        }
    }

    public IActionResult SeleccionarInstrumento(string mensaje="")
    {
        if (mensaje!="")
        {
            ViewBag.mensaje = mensaje;
        }
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

            case -3:
                ViewBag.mensaje = "Para seleccionar un nuevo instrumento primero debe quitar el anterior.";
                return View("SeleccionarInstrumento");
            
            case -4:
            ViewBag.mensaje = "Ya tenés seleccionado este instrumento.";
            return View("SeleccionarInstrumento");

            default:
                ViewBag.mensaje = "Instrumento seleccionado correctamente";
                return View("SeleccionarInstrumento");
        }
    }

    public IActionResult logout()
    {
        HttpContext.Session.Remove("cuenta");
        HttpContext.Session.Remove("usuario");
        return RedirectToAction("Index", "Home", new { mensaje = "Usted salió correctamente de la sesión." });
    }

    public IActionResult InformacionUsuario(string mensaje)
    {
        Usuario usuario = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("usuario"));
        Usuario usuarioComplejo = BD.GetUsuarioComplejo(usuario.IdUsuario);
        ViewBag.usuario = usuarioComplejo;
        List<Instrumento> instrumentosUsuario = BD.GetInstrumentos(usuario.IdUsuario);
        ViewBag.instrumentos = instrumentosUsuario;
        if (mensaje!="")
        {
            ViewBag.mensaje = mensaje;
        }
        return View("InformacionUsuario");
    }

    public IActionResult quitarInstrumento(){
        Usuario usuario = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("usuario"));
        int idInstrumento = BD.GetIdInstrumento(usuario.IdUsuario);
        int reporte = BD.QuitarInstrumentoUsuario(usuario.IdUsuario, idInstrumento);
        switch(reporte){
            case -1:
                return RedirectToAction("SeleccionarInstrumento", "Account", new {mensaje = "Ocurrio un error"});

            case -2:
                return RedirectToAction("SeleccionarInstrumento", "Account", new {mensaje = "Se intento borrar un instrumento que no existe"});

            case 0:
                return RedirectToAction("SeleccionarInstrumento", "Account", new {mensaje = "Se borro exitosamente el instrumento"});
            
            default:
            
            return RedirectToAction("SeleccionarInstrumento", "Account", new {mensaje = "Error desconocido"});
        }
        
    }
}
