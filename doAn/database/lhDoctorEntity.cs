using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doAn.database
{
    [Table("lhDoctor")]
    public class lhDoctorEntity
    {
        [Key]
        public int idDoctor { get; set; }
        public string imgDoctor { get; set; }
        public string nameDoctor { get; set; }

        public string expertise { get; set; }

        public string infoContact { get; set; }

    }
}
