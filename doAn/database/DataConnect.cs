using Microsoft.EntityFrameworkCore;

namespace doAn.database
{
    public class DataConnect:DbContext
    {
        public DataConnect(DbContextOptions<DataConnect> options) : base(options){ }
        public DbSet<lhUserEntity> UserEntity { get; set; }
        public DbSet<lhServicesEntity> SevicesEntity { get; set; }
        public DbSet<lhDoctorEntity> DoctorEntity { get; set; }
        public DbSet<lhAppoimentEntity> AppoimentEntity { get; set; }
    }
}
