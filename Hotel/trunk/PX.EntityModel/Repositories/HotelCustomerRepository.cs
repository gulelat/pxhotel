using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class HotelCustomerRepository : Repository<HotelCustomer>
    {
        public HotelCustomerRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
