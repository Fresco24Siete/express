using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalogo.Api.Models.Entities;

[Table("categoria_producto")]
public class CategoriaProductoEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_categoria_producto")]
    public long IdCategoriaProducto { get; set; }

    [Column("nombre")]
    [MaxLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion", TypeName = "text")]
    public string? Descripcion { get; set; }

    [Column("activa")]
    public bool Activa { get; set; } = true;


}
