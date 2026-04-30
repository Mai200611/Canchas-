using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Runtime.InteropServices;

namespace Cancha.Shared.Entities
{
    public class Cliente
    {
        [Display(Name = "Id del Cliente")]
        public int Id { get; set; }

        [Display(Name = "Cédula")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        [MinLength(5, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El campo {0} solo debe contener números.")]
        public string Cedula { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(200, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        [MinLength(3, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúñÑÁÉÍÓÚ\s]+$", ErrorMessage = "El campo {0} solo debe contener letras y espacios.")]
        public string Nombre { get; set; }

        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        [EmailAddress(ErrorMessage = "El campo {0} no es una dirección de correo electrónico válida.")]
        [DataType(DataType.EmailAddress)] //ui
        public string Correo { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(15, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        [MinLength(7, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres.")]
        [Phone(ErrorMessage = "El campo {0} no es un número de teléfono válido.")]
        [DataType(DataType.PhoneNumber)] //ui
        public string Telefono { get; set; }

        //Relaciones
        [JsonIgnore]
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}