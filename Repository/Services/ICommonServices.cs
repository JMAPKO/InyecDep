using BACKEND02.DTOs;

namespace BACKEND02.Services
{
    public interface ICommonServices<T, TI, TU> //LOS DTOS, Dtos insert, Dtos Update
    {

         Task<IEnumerable<T>> GetAll();//Traer todos
         Task<T> GetById(int id); //traer por ID
         Task<T> Add(TI autoInsertDto); //agregar
         Task<T> Update(int id, TU autoUpdateDto); //update
         Task<T> Delete(int id); //borrar
        
    }
}
