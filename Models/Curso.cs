namespace melodika.Models;
using Newtonsoft.Json;

public class Curso
{
    public Curso() { }

    [JsonProperty("IdCurso")]
    public int IdCurso { get; set; }

    [JsonProperty("TituloCurso")]
    public string TituloCurso { get; set; }

    [JsonProperty("Descripcion")]
    public string Descripcion { get; set; }

    [JsonProperty("IdInstrumento")]
    public int IdInstrumento { get; set; }

    [JsonProperty("Foto")]
    public string Foto { get; set; }

    [JsonProperty("NombreInstrumento")]
    public string nombreInstrumento { get; set; }
}
