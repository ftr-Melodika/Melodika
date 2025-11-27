namespace melodika.Models;
using System;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Data;
public static class BD
{
    private static string _connectionString = @"Server=localhost; DataBase = Melodika_DB; Trusted_Connection = true; TrustServerCertificate = true";

/*
static public List<Cancion> seleccionarCancion()
{
    List<Cancion> canciones = new List<Cancion>();
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "EXEC seleccionarCancion;";
        canciones = connection.Query<Cancion>(query).ToList();
    }
    return canciones;
}
*/



static public List<Cancion> GetCanciones()
{
    List<Cancion> canciones = new List<Cancion>();
    
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string storedProcedure = "seleccionarCancion";
        
        canciones = connection.Query<Cancion>(
            storedProcedure,
            commandType: CommandType.StoredProcedure)
            .ToList();
    }
    
    return canciones;
}
  /*
    static public int logIn(string correo, string contraseña)
    {
        int idUser;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "EXEC loginCuenta @CorreoElectronico = @pCorreo, @Contraseña = @pContraseña ;";
            idUser = connection.QueryFirstOrDefault<int>(query, new { pCorreo = correo, pContraseña = contraseña });
        }
        return idUser;
    }

    */
    static public int logIn(string correo, string contraseña)
    {
        int idUser = -1;
        
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string storedProcedure = "LoginCuenta";
            idUser = connection.QueryFirstOrDefault<int>(
                storedProcedure,
                new{ CorreoElectronico = correo, Contraseña = contraseña},
                commandType: CommandType.StoredProcedure);
        }
        return idUser; 
    }

    static public Cuenta getCuenta(int id){
        
        Cuenta cuentaBuscada = new Cuenta();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string storedProcedure = "GetCuenta";
            cuentaBuscada = connection.QueryFirstOrDefault<Cuenta>(storedProcedure,new{ idCuenta = id },commandType: CommandType.StoredProcedure);
        }

        return cuentaBuscada;
    }

        static public List<Curso> getCursos(){
        
        List<Curso> cursos = new List<Curso>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string storedProcedure = "GetCursos";
            cursos = connection.Query<Curso>(storedProcedure, commandType: CommandType.StoredProcedure).ToList();
        }

        return cursos;
    }

        static public List<Usuario> GetUsuariosCuentaSimple(int idCuenta){
            List<Usuario> usuarios = new List<Usuario>();
            
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string storedProcedure = "GetUsuariosCuentaSimple";
            usuarios = connection.Query<Usuario>(storedProcedure, new{idCuenta = idCuenta}, commandType: CommandType.StoredProcedure).ToList();
        }

            return usuarios;
        }

        static public Usuario GetUsuarioSimple(int idUsuario){
            Usuario usuarioBuscado = new Usuario();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string storedProcedure = "GetUsuarioSimple";
                usuarioBuscado = connection.QueryFirstOrDefault<Usuario>(storedProcedure,new{ idUsuario = idUsuario },commandType: CommandType.StoredProcedure);
            }

            return usuarioBuscado;
        }
    static public int crearCuenta(string correo, string contraseña, string username, bool terminos, bool actualizaciones){
        int idCuenta = -1;
        DateTime fecha = DateTime.Now;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string storedProcedure = "CrearCuenta";
            idCuenta = connection.QueryFirstOrDefault<int>(
                storedProcedure,
                new{ CorreoElectronico = correo, Contrasenia = contraseña, Username = username, FechaRegistro = fecha, Terminos = terminos, Actualizaciones = actualizaciones},
                commandType: CommandType.StoredProcedure);
        }
        return idCuenta; 
    }

    static public int crearUsuario(string nombre, DateTime fechaNacimiento, string genero, int idCuenta){
        int idUsuario = -1;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string storedProcedure = "CrearUsuario";
            idUsuario = connection.QueryFirstOrDefault<int>(
                storedProcedure,
                new{Nombre = nombre, Genero = genero, FechaNacimiento = fechaNacimiento, IdCuenta = idCuenta},
                commandType: CommandType.StoredProcedure);
        }
        return idUsuario;
    }

    static public int AgregarInstrumentoUsuario(int idUsuario, int idInstrumento){
        int idInstrumento = -1;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string storedProcedure = "AgregarInstrumentoUsuario";
            idInstrumento = connection.QueryFirstOrDefault<int>(
                storedProcedure,
                new{IdUsuario = idUsuario, IdInstrumento = idInstrumento},
                commandType: CommandType.StoredProcedure);
        }
        return idInstrumento;
    }

    static public List<Instrumento> GetInstrumetnos(int idUsuario){
        List<Instrumento> instrumentos = new List<Instrumento>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string storedProcedure = "GetInstrumetnos";
            instrumentos = connection.Query<Instrumento>(storedProcedure, new{idUsuario = idUsuario}, commandType: CommandType.StoredProcedure).ToList();
        }
        return instrumentos;
    }

}


