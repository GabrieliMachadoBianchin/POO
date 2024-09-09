using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstoqueSistema.Models;
using EstoqueSistema.DataBase;

namespace EstoqueSistema.Services
{
    public class Services<T> : IServices<T> where T : class
    {
        private readonly EstoqueLojinhaContext _context;
        private readonly DbSet<T> _dbSet;

        public Services(EstoqueLojinhaContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> ObterTodos()
        {
            return _dbSet.ToList();
        }

        public T ObterPorId(int id)
        {
            return _dbSet.Find(id);
        }
        public void Adicionar(T entidade)
        {
            _dbSet.Add(entidade);
            _context.SaveChanges();
        }
        public void Atualizar(T entidade)
        {
            _dbSet.Update(entidade);
            _context.SaveChanges();
        }
        public void Excluir(int id)
        {
            var entidade = _dbSet.Find(id);
            if (entidade != null)
            {
                _dbSet.Remove(entidade);
                _context.SaveChanges();
            }
        }

        public async Task<T> AdicionarAsync(T entidade)
        {
            _dbSet.Add(entidade);
            await _context.SaveChangesAsync();
            return entidade;
        }
    }
}
