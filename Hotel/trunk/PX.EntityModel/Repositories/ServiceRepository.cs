﻿using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class ServiceRepository : Repository<Service>
    {
        public ServiceRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
