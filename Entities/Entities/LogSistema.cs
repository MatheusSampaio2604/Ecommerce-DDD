using Entities.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{

    [Table("TB_LOG_SISTEMA")]
    public class LogSistema : Base
    {
        [Column("LOG_JSONINFORMACAO")]
        [Display(Name = "Json Informação")]
        public string JsonInformacao { get; set; }

        [Column("LOG_TIPOLOG")]
        [Display(Name = "Tipo Log")]
        public EnumTipoLog TipoLog { get; set; }

        [Column("LOG_CONTROLLER")]
        [Display(Name = "Nome Controller")]
        public string NomeController { get; set; }

        [Column("LOG_ACTION")]
        [Display(Name = "Nome Action")]
        public string NomeAction { get; set; }

        [Column(Order = 1)]
        [Display(Name = "Usuário")]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


    }
}
