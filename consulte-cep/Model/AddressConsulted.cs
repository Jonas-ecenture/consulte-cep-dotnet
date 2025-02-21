using System.ComponentModel.DataAnnotations;

namespace consulte_cep.Model
{
    public class AddressConsulted
    {
        public AddressConsulted(string postcode, 
            string street, 
            string complement, 
            string neighborhood, 
            string city, 
            string uf) {
            Postcode = postcode;
            Street = street;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            UF = uf;
        }

        [Key]
        public int Id { get; set; }
        public string Postcode { get; set; }
        public string Street { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string UF { get; set; }
    }
}
