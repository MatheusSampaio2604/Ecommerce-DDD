using Entities.Entities.Enums;
using Entities.Notifications;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    [Table("TB_COMPRA")]

    public class Compra : Notifies
    {
        [Column("COM_ID")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("COM_ESTADO")]
        [Display(Name = "Estado")]
        public EnumEstadoCompra Estado { get; set; }

        [Column("COM_DATA_COMPRA")]
        [Display(Name = "Data Compra")]
        public DateTime? DataCompra { get; set; }

        [Column(Order = 1)]
        [Display(Name = "Usuário")]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
