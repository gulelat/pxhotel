﻿using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class TemplateRepository : Repository<Template>
    {
        public TemplateRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
