using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Servicio.Api.Models.Entities;

[Table("categoria_servicio")]
public class CategoriaServicioEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_categoria_servicio")]
    public long IdCategoriaServicio { get; set; }

    [Column("nombre")]
    [MaxLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion", TypeName = "text")]
    public string? Descripcion { get; set; }

    [Column("duracion_promedio_horas")]
    public int? DuracionPromedioHoras { get; set; }

    [Column("requiere_ubicacion")]
    public bool RequiereUbicacion { get; set; } = true;

    [Column("precio_base_sugerido", TypeName = "decimal(12,2)")]
    public decimal PrecioBaseSugerido { get; set; } = 0.00m;

    [Column("activa")]
    public bool Activa { get; set; } = true;

    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    [Column("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}
