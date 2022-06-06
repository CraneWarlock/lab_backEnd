using System.Runtime.CompilerServices;


namespace WarehouseManager.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }

    }
}
