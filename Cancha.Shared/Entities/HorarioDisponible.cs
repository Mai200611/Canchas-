using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cancha.Shared.Entities
{
    public enum DiaSemana
    {
        [Display(Name = "Domingo")]
        Domingo = 0,

        [Display(Name = "Lunes")]
        Lunes = 1,

        [Display(Name = "Martes")]
        Martes = 2,

        [Display(Name = "Miércoles")]
        Miercoles = 3,

        [Display(Name = "Jueves")]
        Jueves = 4,

        [Display(Name = "Viernes")]
        Viernes = 5,

        [Display(Name = "Sábado")]
        Sabado = 6
    }

    public class HorarioDisponible
    {
        [Display(Name = "Id del Horario")]
        public int Id { get; set; }

        [Display(Name = "Día de la Semana")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EnumDataType(typeof(DiaSemana), ErrorMessage = "El campo {0} no es válido.")]
        public int DiaSemana { get; set; }

        [Display(Name = "Hora de Inicio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Time)]
        [Column(TypeName = "time")]
        public TimeSpan HoraInicio { get; set; }

        [Display(Name = "Hora de Fin")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Time)]
        [Column(TypeName = "time")]
        [CustomValidation(typeof(HorarioDisponible), nameof(ValidarHoraFin))] //Validacion de que las horas sean coherentes
        public TimeSpan HoraFin { get; set; }

        [Display(Name = "Horario Activo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool Activo { get; set; } = true;


        //Relaciones

        [JsonIgnore]
        public CanchaEntidad? Cancha { get; set; }

        [Display(Name = "Cancha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int CanchaId { get; set; }


        //Validacion HoraFin > HoraInicio
        public static ValidationResult? ValidarHoraFin(TimeSpan horaFin, ValidationContext context)
        {
            var instance = context.ObjectInstance as HorarioDisponible;
            if (instance != null && horaFin <= instance.HoraInicio)
            {
                return new ValidationResult("La hora de finalización debe ser mayor que la hora de inicio.");
            }
            return ValidationResult.Success;
        }
    }
}