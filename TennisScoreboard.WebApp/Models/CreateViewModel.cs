using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TennisScoreboard.WebApp.Models
{
    public class CreateViewModel
    {
        [Required]
        [DisplayName("Имя первого игрока")]
        public string NamePlayer1 { get; set; }

        [Required]
        [DisplayName("Имя второго игрока")]
        public string NamePlayer2 { get; set; }
    }
}
