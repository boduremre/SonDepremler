using SonDepremler.Utils;
using System.Text.RegularExpressions;

namespace SonDepremler.Models.ViewModels
{
    public class DepremViewModel
    {
        public DateTime Tarih { get; set; }
        public string? Enlem { get; set; }
        public string? Boylam { get; set; }
        public string? Derinlik { get; set; }
        public string? Siddet { get; set; }
        public string? Yer { get; set; }
    }
}