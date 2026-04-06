using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class TipoActivo
{
    public int IdTipoActivo { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Activo> Activos { get; set; } = new List<Activo>();

    public virtual ICollection<Modelo> Modelos { get; set; } = new List<Modelo>();
}
