using BACKEND02.DTOs;
using BACKEND02.Models;
using Microsoft.EntityFrameworkCore;

namespace BACKEND02.Repository
{
    public class AutoRepository : IRepository<Auto>
    {

        // Aquí se implementaría la lógica para interactuar con la base de datos
        private StoreContext _context;

        public AutoRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Auto>> GetAll()
        {
            var autos = await _context.Autos.ToListAsync();
            return autos;
        }

        public async Task<Auto> GetById(int id)
        {
            var auto = await _context.Autos.FindAsync(id);
           
            return auto;
        }


        public async Task Add(Auto auto) => await _context.Autos.AddAsync(auto);

        public void Update(Auto AutoUpdate)
        {
            //no es async porque no existe un metodo updateAsync
            _context.Autos.Attach(AutoUpdate); // se posiciona
            _context.Entry(AutoUpdate).State = EntityState.Modified; // marca el estado como modificado
        }

        public void Delete(Auto auto)
        {
            _context.Autos.Remove(auto);
            
        }

        
        public Task Save() => _context.SaveChangesAsync();


    }
}
