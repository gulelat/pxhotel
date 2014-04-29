using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class TestimonialRepository : Repository<Testimonial>
    {
        public TestimonialRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
