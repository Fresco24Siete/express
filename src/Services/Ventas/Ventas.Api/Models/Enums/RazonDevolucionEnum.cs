using System.Text.Json.Serialization;

namespace Ventas.Api.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RazonDevolucionEnum
{
    defectuoso,
    producto_equivocado,
    no_deseado,
    talla_incorrecta,
    danado_en_envio,
    otro
}
