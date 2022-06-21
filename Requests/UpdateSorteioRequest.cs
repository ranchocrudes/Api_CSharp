using System.ComponentModel.DataAnnotations;
using static Bogus.DataSets.Name;

namespace MyApiWithDoc.Requests
{
    public class UpdateSorteioRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Descricao { get; set; }

        [Required]
        public string Data { get; set; }
    }
}
