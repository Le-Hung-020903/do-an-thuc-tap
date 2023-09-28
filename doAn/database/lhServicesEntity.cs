using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doAn.database
{
    [Table("lhServices")]
    public class lhServicesEntity
    {
        [Key]
        public int idService { get; set; }
        public string nameService { get; set; }
        public string descriptionServices { get; set; }
        public float price { get; set; }


    }
}
