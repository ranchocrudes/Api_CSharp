using System.ComponentModel.DataAnnotations;
using static Bogus.DataSets.Name;

namespace MyApiWithDoc.Responses
{
    public class SorteioResponse
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Descricao { get; set; }

        [Required]
        public string Data { get; set; }

        public SorteioResponse(int id, string name, string data,  string descricao)
        {
            Id = id;
            Name = name;
            Descricao = descricao;
            Data = data;

        }
    }
}
