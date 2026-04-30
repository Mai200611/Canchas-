using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cancha.Shared.Entities
{
    public enum TipoCancha
    {
        Futbol = 1,
        [Display(Name = "Futbol Sala")] //Agrega espacio en ui
        FutbolSala = 2,
        Tennis = 3,
        Baloncesto = 4,
        Voleibol = 5,
        Pádel = 6
    }

    public class Cancha
    {
        [Display(Name = "Id de la Cancha")]
        public int Id { get; set; }

        [Display(Name = "Nombre de la Cancha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        [MinLength(3, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúñÑÁÉÍÓÚ0-9\s]+$",
            ErrorMessage = "El campo {0} solo puede contener letras, números y espacios.")]
        public string Nombre { get; set; }

        [Display(Name = "Tipo de Cancha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EnumDataType(typeof(TipoCancha), ErrorMessage = "El campo {0} no es válido.")]
        public TipoCancha Tipo { get; set; }

        [Display(Name = "Precio por Hora")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El campo {0} debe ser mayor que {1}.")]
        [Column(TypeName = "decimal(8,2)")]
        [DataType(DataType.Currency)] //ui
        public decimal PrecioHora { get; set; }

        [Display(Name = "Cancha Activa")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool Activa { get; set; } = true;


        //Relaciones

        [JsonIgnore]
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}