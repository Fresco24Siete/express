using System.Text.Json.Serialization;

namespace Ventas.Api.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EstadoDevolucionEnum
{
    solicitada,
    en_revision,
    aprobada,
    rechazada,
    completada
}
