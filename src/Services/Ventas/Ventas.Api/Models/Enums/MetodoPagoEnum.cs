using System.Text.Json.Serialization;

namespace Ventas.Api.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MetodoPagoEnum
{
    tarjeta_credito,
    tarjeta_debito,
    pse,
    efectivo,
    billetera_digital,
    transferencia
}
