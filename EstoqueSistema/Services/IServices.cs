using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueSistema.Services
{
    public interface IServices<T> where T : class
    {

        IEnumerable<T> ObterTodos();
        T ObterPorId(int id);
        void Atualizar(T entidade);
        void Excluir(int id);

    }
}
