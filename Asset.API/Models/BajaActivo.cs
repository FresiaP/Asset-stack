using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class BajaActivo
{
    public int IdBaja { get; set; }

    public int IdActivo { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Motivo { get; set; }

    public int IdUsuario { get; set; }

    public virtual Activo IdActivoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
