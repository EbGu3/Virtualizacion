using System;
using System.Collections.Generic;

namespace MDChatPlus.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Usuario1 { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Edad { get; set; }

    public string Email { get; set; } = null!;
}
