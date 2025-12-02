namespace melodika.Models;
using Newtonsoft.Json;

public class Usuario
{


    [JsonProperty("IdUsuario")]
    public int IdUsuario { get; set; }

    [JsonProperty("Nombre")]
    public string Nombre { get; set; }


    [JsonProperty("FechaNacimiento")]
    public DateTime FechaNacimiento { get; set; }

    [JsonProperty("FechaRegistro")]
    public DateTime FechaRegistro { get; set; }

    [JsonProperty("FotoPerfil")]
    public string FotoPerfil { get; set; }

    [JsonProperty("Genero")]
    public string Genero { get; set; }

    [JsonProperty("Rol")]
    public string Rol { get; set; }

    [JsonProperty("IdCuenta")]
    public int IdCuenta { get; set; }

    [JsonProperty("Racha")]
    public int Racha { get; set; }

    public Usuario()
    {

    }
}

