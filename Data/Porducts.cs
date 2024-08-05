using System.ComponentModel.DataAnnotations;

namespace SecureApiWithAuthentication.Data
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required , MaxLength(255)]
        public string Name { get; set; }
        [Required , MaxLength(255)]
        public string SKu { get; set; }
    }
}
