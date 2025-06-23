using BACKEND02.Models;
using Microsoft.EntityFrameworkCore;

namespace BACKEND02.Repository
{
    public class MarcaRepository : IRepository<Marca>
    {
        private StoreContext _context;

        public MarcaRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Marca>> GetAll()
        {
            var autos = await _context.Marcas.ToListAsync();
            return autos;
        }


        public async Task<Marca> GetById(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            return marca;
        }

        public async Task Add(Marca marca) => await _context.Marcas.AddAsync(marca);


        public void Update(Marca marca)
        {
            _context.Marcas.Attach(marca); // se posiciona
            _context.Entry(marca).State = EntityState.Modified; // marca el estado como modificado
        }

        public void Delete(Marca marca)
        {
            _context.Marcas.Remove(marca);
        }

        public async Task Save() => await _context.SaveChangesAsync();

        public IEnumerable<Marca> Search(Func<Marca, bool> filter) => 
            _context.Marcas.Where(filter).ToList(); //busqueda por filtro

    }
}
