namespace PX.Business.Models.RotatingImages
{
    public class RotatingImageModel : BaseModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }
    }
}
