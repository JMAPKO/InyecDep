namespace BACKEND02.Services
{
    public class RandomServices : IRandomServices
    {
        private readonly int _value;

        public int Value => _value;

        // El método getValue() también devolverá el mismo _value.
        public int getValue()
        {
            return _value;
        }

        public RandomServices()
        {
            // Aquí se genera el número aleatorio y se asigna a _value.
            // Esto solo ocurre cuando se crea una nueva instancia de RandomServices.
            _value = new Random().Next(1, 1000);
        }

        

    }
}
