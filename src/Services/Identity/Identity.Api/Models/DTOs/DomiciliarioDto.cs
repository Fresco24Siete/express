
using System.Text.Json.Serialization;
using Identity.Api.Models.Enums;

namespace Identity.Api.Models.Dtos;

public class DomiciliarioDto
{
   
    [JsonPropertyName("placa_vehiculo")]
    public string PlacaVehiculo { get; set; } = null!;

    [JsonPropertyName("tipo_vehiculo")]
    public TipoVehiculoEnum TipoVehiculo { get; set; }

    [JsonPropertyName("capacidad_carga")]
    public decimal CapacidadCarga { get; set; }


}
