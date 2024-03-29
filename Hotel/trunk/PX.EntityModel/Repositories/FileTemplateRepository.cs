﻿using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class FileTemplateRepository : HierarchyRepository<FileTemplate>
    {
        public FileTemplateRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
