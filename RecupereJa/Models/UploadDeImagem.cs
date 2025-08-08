namespace RecupereJa.Models
{
    public class UploadDeImagem
    {
        public string ImageId { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile ImageFile { get; set; }
        public bool? HasImage { get; set; }
    }
}
