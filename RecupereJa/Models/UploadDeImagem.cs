using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecupereJa.Models
{
    [NotMapped]
    public class UploadDeImagem
    {
        public IFormFile? Arquivo { get; set; }

        public string ImageId { get; set; }

        public bool? HasImage { get; set; }
    }
}



    //[NotMapped]
    //public class UploadDeImagem
    //{
    //    public int UsuarioId { get; set; }

    //    public string ImageId { get; set; }

    //    public string? ImageUrl { get; set; }

    //    [NotMapped]
    //    public IFormFile ImageFile { get; set; }

    //    public bool? HasImage { get; set; }
    //}