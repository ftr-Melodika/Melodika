namespace melodika.Models;
using Newtonsoft.Json;

public class Cuenta
{
    [JsonProperty("IdCuenta")]
    public int IdCuenta { get; set; }

    [JsonProperty("Username")]
    public string Username { get; set; }

    [JsonProperty("CorreoElectronico")]
    public string CorreoElectronico { get; set; }

    [JsonProperty("Contraseña")]
    public string Contraseña { get; set; }

    [JsonProperty("FechaRegistro")]
    public DateTime FechaRegistro { get; set; }

    public Cuenta(){}
}   