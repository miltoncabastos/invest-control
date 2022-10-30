using System.Collections.Generic;
using InvestControl.Domain.Entity;

namespace InvestControl.Domain.Repository
{
    public interface IEventoRepository : IBaseRepository<Evento>
    {
        public IEnumerable<Evento> ObterAteAno(int ano);
    }
}