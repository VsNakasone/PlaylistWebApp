using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistWebApp.Models
{
    [Table("SONG")]
    public class Song
    {
        [Key]
        [Column("IDSONG")]
        public int IdSong { get; set; }

        [Column("NAMESONG")]
        public string NameSong { get; set; }

        [Column("ARTIST")]
        public string Artist { get; set; }
    }
}
