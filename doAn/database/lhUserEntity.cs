using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doAn.database
{
    [Table("lhUser")]
    public class lhUserEntity
    {
        [Key]
        public int idUser { get; set; }
        public string nameUser { get; set; }
        public string passwordUser { get; set; }
        public string emailUser { get; set; }
        public string phoneNumber { get; set; }

        public string statusJob { get; set; }
        public bool isAdmin { get; set; }
    }
}
