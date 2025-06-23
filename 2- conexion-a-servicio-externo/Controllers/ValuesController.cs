using BACKEND02.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {

        //prueba: creamos variables privadas para guardar al crear al constructor
        private IRandomServices _randomServicesSingleton;
        private IRandomServices _randomServicesScoped;
        private IRandomServices _randomServicesTransient;

        private IRandomServices _randomServicesSingleton2;
        private IRandomServices _randomServicesScoped2;
        private IRandomServices _randomServicesTransient2;

        public RandomController(
            [FromKeyedServices("RandomSingleton")] IRandomServices randomServicesSingleton,
            [FromKeyedServices("RandomScoped")] IRandomServices randomServicesScoped,
            [FromKeyedServices("RandomTransient")] IRandomServices randomServicesTransient,
            [FromKeyedServices("RandomSingleton")] IRandomServices randomServicesSingleton2,
            [FromKeyedServices("RandomScoped")] IRandomServices randomServicesScoped2,
            [FromKeyedServices("RandomTransient")] IRandomServices randomServicesTransient2)
        {
            _randomServicesSingleton = randomServicesSingleton;
            _randomServicesScoped = randomServicesScoped;
            _randomServicesTransient = randomServicesTransient;
            _randomServicesSingleton2 = randomServicesSingleton2;
            _randomServicesScoped2 = randomServicesScoped2;
            _randomServicesTransient2 = randomServicesTransient2;
        }


        [HttpGet("num")]
        public ActionResult<Dictionary<string, int>> getRandom()
        {
            var value = new Dictionary<string, int>();

            value.Add("Singleton", _randomServicesSingleton.Value);
            value.Add("Scoped", _randomServicesScoped.Value);
            value.Add("Transient", _randomServicesTransient.Value);
            
            value.Add("Singleton2", _randomServicesSingleton2.Value);
            value.Add("Scoped2", _randomServicesScoped2.Value);
            value.Add("Transient2", _randomServicesTransient2.Value);

            if (value != null ) 
            { 
                return value;
            }

            return NotFound("No random value found.");
        }


    }
}
