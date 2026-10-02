using System.Text.Json.Serialization;

namespace Servicio.Api.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EstadoServicioEnum
{
    solicitado,
    aceptado,
    en_proceso,
    completado,
    cancelado,
    rechazado
}
