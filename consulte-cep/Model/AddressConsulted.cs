using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace consulte_cep.Model
{
    public class AddressConsulted
    {
        public AddressConsulted() { }

        public AddressConsulted(
            string postcode,
            string street,
            string? complement,
            string neighborhood,
            string city,
            string uf
        ) {
            Postcode = postcode;
            Street = street;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            UF = uf;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public string Postcode { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(60)")]
        public string Street { get; set; }

        [Column(TypeName = "nvarchar(60)")]
        public string? Complement { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(40)")]
        public string Neighborhood { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(40)")]
        public string City { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(2)")]
        public string UF { get; set; }
    }
}
