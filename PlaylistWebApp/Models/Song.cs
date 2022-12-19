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
        public string Name { get; set; }

        [Column("CATEGORIE")]
        public string Categorie { get; set; }
    }
}
