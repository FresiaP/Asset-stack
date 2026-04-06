using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class ActivoAtributo
{
    public int Id { get; set; }

    public int IdActivo { get; set; }

    public int IdAtributo { get; set; }

    public string? Valor { get; set; }

    public virtual Activo IdActivoNavigation { get; set; } = null!;

    public virtual Atributo IdAtributoNavigation { get; set; } = null!;
}
