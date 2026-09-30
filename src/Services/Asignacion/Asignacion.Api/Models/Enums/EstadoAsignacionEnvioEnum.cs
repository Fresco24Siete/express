using System.Text.Json.Serialization;

namespace Asignacion.Api.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EstadoAsignacionEnvioEnum
{
    Asignado,
    EnTransito,
    Entregado,
    Fallido,
    Cancelado
}
