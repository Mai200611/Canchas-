using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cancha.Shared.Entities
{
    public enum MetodoPago
    {
        [Display(Name = "Efectivo")]
        Efectivo = 1,

        [Display(Name = "Transferencia")]
        Transferencia = 2,

        [Display(Name = "Tarjeta")]
        Tarjeta = 3
    }

    public class Pago
    {
        [Display(Name = "Código del Pago")]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Monto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El campo {0} debe ser mayor que {1}.")]
        [Column(TypeName = "decimal(10,2)")]
        [DataType(DataType.Currency)]
        public decimal Monto { get; set; }

        [Display(Name = "Fecha de Pago")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.DateTime)]
        public DateTime FechaPago { get; set; } = DateTime.Now;  // Default: fecha actual

        [Display(Name = "Método de Pago")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        [EnumDataType(typeof(MetodoPago), ErrorMessage = "El campo {0} no es válido.")]
        [Column(TypeName = "varchar(20)")]
        public string MetodoPago { get; set; }

        // Relaciones
        [JsonIgnore]
        [ForeignKey("ReservaId")]
        public Reserva? Reserva { get; set; }

        [Display(Name = "Reserva")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ReservaId { get; set; }
    }
}