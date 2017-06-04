using OrdersCollector.Core.Exceptions;

namespace OrdersCollector.Core.Suppliers.Exceptions
{
    public class SupplierWithSpecifiedNameOrAliasAlreadyExistsException : DomainException
    {
        public SupplierWithSpecifiedNameOrAliasAlreadyExistsException(string name) 
            : base(
                ErrorCodes.SupplierWithSpecifiedNameOrAliasAlreadyExists,
                $"Supplier with name or alias '{name} already exists")
        {
            SupplierName = name;
        }

        public string SupplierName { get; }
    }
}