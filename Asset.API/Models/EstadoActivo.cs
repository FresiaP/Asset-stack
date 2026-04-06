using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class EstadoActivo
{
    public int IdEstadoActivo { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Activo> Activos { get; set; } = new List<Activo>();
}
