using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doAn.database
{
    [Table("lhAppointments")]
    public class lhAppoimentEntity
    {

        [Key]
        public int idAppointments { get; set; }
        public string appointmentDate { get; set; }
        public string nameUser { get; set; }
        public string emailUser { get; set; }
        public string nameService { get; set; }
        public string nameDoctor { get; set; }
    }
}
