using System.Text.Json.Serialization;

namespace Ventas.Api.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EstadoOrdenEnum
{
    pendiente_pago,
    pagada,
    preparando,
    en_camino,
    entregada,
    cancelada
}
