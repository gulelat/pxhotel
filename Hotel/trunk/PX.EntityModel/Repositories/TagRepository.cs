﻿using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class TagRepository : HierarchyRepository<Tag>
    {
        public TagRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
