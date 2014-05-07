using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class EmailAccountRepository : Repository<EmailAccount>
    {
        public EmailAccountRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
