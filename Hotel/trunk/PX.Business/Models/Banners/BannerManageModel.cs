namespace PX.Business.Models.Banners
{
    public class BannerManageModel
    {
        public int? Id { get; set; }

        public string ImageUrl { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public string GroupName { get; set; }

        public int RecordOrder { get; set; }
    }
}
