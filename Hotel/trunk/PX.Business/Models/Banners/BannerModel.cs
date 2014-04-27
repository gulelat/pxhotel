namespace PX.Business.Models.Banners
{
    public class BannerModel : BaseModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public string GroupName { get; set; }
    }
}
