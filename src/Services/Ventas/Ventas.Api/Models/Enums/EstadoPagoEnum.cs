using System.Text.Json.Serialization;

namespace Ventas.Api.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EstadoPagoEnum
{
    pendiente,
    completado,
    fallido,
    reembolsado,
    cancelado
}
