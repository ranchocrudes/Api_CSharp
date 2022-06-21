using System;
using static Bogus.DataSets.Name;

namespace MyApiWithDoc.Entities
{
    public class Sorteio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descricao { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Data { get; set; }

        public Sorteio(int id, string name, string descricao, string data)
        {
            Id = id;
            Name = name;
            Descricao = descricao;
            Data = data;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public Sorteio(int id)
        {
            Id = id;
        }
    }
}