namespace melodika.Models;
using Newtonsoft.Json;

public class Cancion
{
    [JsonProperty("IdCancion")]
    public int IdCancion { get; set; }

    [JsonProperty("Titulo")]
    public string Titulo { get; set; }

    [JsonProperty("Autor")]
    public string Autor { get; set; }

    [JsonProperty("Portada")]
    public string Portada { get; set; }

    [JsonProperty("Dificultad")]
    public string Dificultad { get; set; }

    [JsonProperty("NombreGenero")]
    public string NombreGenero { get; set; }

    [JsonProperty("Valoracion")]
    public int Valoracion { get; set; }

    [JsonProperty("IdInstrumento")]
    public int IdInstrumento { get; set; }
}
