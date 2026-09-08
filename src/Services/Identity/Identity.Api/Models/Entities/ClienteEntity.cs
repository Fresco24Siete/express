using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Identity.Api.Models.Enums;

namespace Identity.Api.Models.Entities;



[Table("cliente")]
public class ClienteEntity : UsuarioEntity
{   
    [Column("id_carrito")]
    public long? IdCarrito {get; set;}
    
    [Column("cantidad_resenas")]
    public int CantidadResenas {get; set;}

    [Column("nivel_confianza")]
    [MaxLength(50)]
    public string NivelConfianza {get;set;} = null!;

    [Column("metodo_pago_prederminado")]
    public MetodoPagoEnum MetodoPago {get; set;}

    [Column("numero_pedidos")]
    public int NumeroPedidos {get; set;}

    [Column("estado_activo")]
    public bool EstadoActivo {get; set;}
}