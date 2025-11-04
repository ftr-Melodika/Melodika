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



static public List<Cancion> seleccionarCancion()
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
            string storedProcedure = "loginCuenta";
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
            cuentaBuscada = connection.QueryFirstOrDefault<Cuenta>(
                storedProcedure,
                new{ idCuenta = id },
                commandType: CommandType.StoredProcedure);
        }

        return cuentaBuscada;
    }

}


