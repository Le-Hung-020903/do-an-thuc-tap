namespace doAn.Models
{
    public class UserModel
    {
        public int idUser { get; set; }
        public string nameUser { get; set; }
        public string passwordUser { get; set; }
        public string confirmPassword { get; set; }
        public string emailUser { get; set; }
        public string phoneNumber { get; set; }
        public string statusJob { get; set; }
        public bool isAdmin { get; set; }
    }
}
