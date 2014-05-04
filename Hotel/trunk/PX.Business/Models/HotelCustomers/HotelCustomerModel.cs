using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.HotelCustomers
{
    public class HotelCustomerModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string IndentityNumber { get; set; }

        [Required]
        public string Address { get; set; }

        public string Country { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Note { get; set; }
    }
}
