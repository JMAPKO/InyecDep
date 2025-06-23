namespace BACKEND02.Repository
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task Save();

        //ERRORES
        IEnumerable<TEntity> Search(Func<TEntity, bool> filter); //busqueda por filtro
    }
}
