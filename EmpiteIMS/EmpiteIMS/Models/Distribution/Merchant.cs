using System.ComponentModel.DataAnnotations;

namespace EmpiteIMS.Models.Distribution
{
    public class Merchant
    {
        [Key]
        public string MerchantId { get; set; }

        [DataType(DataType.EmailAddress)]
        public string MerchantEmail { get; set; }
    }
}
