using System.Collections.Generic;

namespace PX.Business.Models.ClientMenus
{
    public class ClientMenuRenderingModel
    {
        public string Url { get; set; }
        public string Name { get; set; }

        public List<ClientMenuRenderingModel> ChildMenus { get; set; } 
    }
}
