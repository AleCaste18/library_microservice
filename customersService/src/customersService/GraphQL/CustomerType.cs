using customersService.Entities;

namespace customersService.GraphQL
{
    public class CustomerType : ObjectType<Customer>
    {
        protected override void Configure(IObjectTypeDescriptor<Customer> descriptor)
        {
            descriptor.Field(_ => _.Id);
            descriptor.Field(_ => _.Name);
            descriptor.Field(_ => _.Surname);
            descriptor.Field(_ => _.Address);
            descriptor.Field(_ => _.Card);
        }
    }
}
