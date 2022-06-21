using System.Collections.Generic;
using System.Linq;
using Bogus;
using MyApiWithDoc.Entities;
using static Bogus.DataSets.Name;

namespace MyApiWithDoc.MockData
{
    public class SorteioMock
    {
        private List<Sorteio> _data;

        public IEnumerable<Sorteio> Data
        {
            get { return _data; }
        }

        public SorteioMock()
        {
            _data = new List<Sorteio>();

            var userId = 0;

            for (int i = 0; i < 100; i++)
            {
                _data.Add(
                    new Faker<Sorteio>("en")
                        .CustomInstantiator(f => new Sorteio(++userId))
                        .RuleFor(u => u.Name, (f, u) => f.Name.FullName(0))
                        .RuleFor(u => u.CreatedAt, (f, u) => f.Date.Past(2))
                        .RuleFor(u => u.UpdatedAt, (f, u) => f.Date.Past(1, u.CreatedAt))
                );
            }
        }

        public void Add(Sorteio sorteio)
        {
            _data.Add(sorteio);
        }

        public Sorteio GetById(int id)
        {
            return (from c in _data
                    where c.Id.Equals(id)
                    select c).FirstOrDefault();
        }

        public void Remove(int id)
        {
            _data = (from c in _data
                     where !c.Id.Equals(id)
                     select c).ToList();
        }

        public void Update(Sorteio sorteio)
        {
            _data = (from c in _data
                     select c.Id == sorteio.Id ? sorteio : c).ToList();
        }
    }
}
