namespace PX.Business.Models.Services.CurlyBrackets
{
    public class ServiceCurlyBracket : BaseModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string DetailsUrl { get; set; }

        public string ImageUrl { get; set; }
    }
}
