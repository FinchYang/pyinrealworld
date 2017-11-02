using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace trafficpolice.Models
{
    public class uploadtemplate
    {
        public string templatetype { set; get; }
        [Required]
        [MaxLength(100)]
        public string name { set; get; }
        [Required]
        [MaxLength(300)]
        public string comment { set; get; }
        [Required]
        [Display(Name = "模板文件")]
        [FileExtensions(Extensions = ".doc,.docx", ErrorMessage = "word文档格式错误")]
        public IFormFile templatefile { get; set; }
    }
}
