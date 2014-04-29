﻿using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class BannerRepository : Repository<Banner>
    {
        public BannerRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
