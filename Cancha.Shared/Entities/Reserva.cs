using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cancha.Shared.Entities
{
    public enum EstadoReserva
    {
        [Display(Name = "Pendiente")]
        Pendiente = 1,

        [Display(Name = "Confirmada")]
        Confirmada = 2,

        [Display(Name = "Completada")]
        Completada = 3,

        [Display(Name = "Cancelada")]
        Cancelada = 4
    }

    public class Reserva
    {
        [Display(Name = "Código de la Reserva")]
        public int Id { get; set; }

        [Display(Name = "Fecha de la Reserva")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Reserva), nameof(ValidarFechaNoMenorActual))]
        public DateTime Fecha { get; set; } = DateTime.Today;

        [Display(Name = "Hora de Inicio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Time)]
        [Column(TypeName = "time")]
        public TimeSpan HoraInicio { get; set; }

        [Display(Name = "Hora de Fin")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Time)]
        [Column(TypeName = "time")]
        [CustomValidation(typeof(Reserva), nameof(ValidarHoraFin))]
        public TimeSpan HoraFin { get; set; }

        [Display(Name = "Estado de la Reserva")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EnumDataType(typeof(EstadoReserva), ErrorMessage = "El campo {0} no es válido.")]
        public EstadoReserva Estado { get; set; } = EstadoReserva.Pendiente;

        [Display(Name = "Precio Total")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El campo {0} debe ser mayor que {1}.")]
        [Column(TypeName = "decimal(10,2)")]
        [DataType(DataType.Currency)]
        public decimal PrecioTotal { get; set; }

        //Relaciones
        [JsonIgnore]
        public CanchaEntidad? Cancha { get; set; }

        [Display(Name = "Cancha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int CanchaId { get; set; }


        [JsonIgnore]
        public Cliente? Cliente { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ClienteId { get; set; }

        [JsonIgnore]
        public Pago? Pago { get; set; }

        //Validacion Fecha no menor a la actual
        public static ValidationResult? ValidarFechaNoMenorActual(DateTime fecha, ValidationContext context)
        {
            if (fecha.Date < DateTime.Today)
            {
                return new ValidationResult("La fecha de la reserva no puede ser anterior a la fecha actual.");
            }
            return ValidationResult.Success;
        }

        //Validacion HoraFin > HoraInicio
        public static ValidationResult? ValidarHoraFin(TimeSpan horaFin, ValidationContext context)
        {
            var instance = context.ObjectInstance as Reserva;
            if (instance != null && horaFin <= instance.HoraInicio)
            {
                return new ValidationResult("La hora de finalización debe ser mayor que la hora de inicio.");
            }
            return ValidationResult.Success;
        }
    }
}