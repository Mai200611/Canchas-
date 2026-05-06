using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Cancha.Shared.Entities
{
    public class Cliente
    {
        [Key]
        public int Cedula { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
    }
}
