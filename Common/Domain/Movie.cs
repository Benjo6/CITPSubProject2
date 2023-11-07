using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain;

[Table("movie")]
public class Movie
{




    [Column("is_adult")]



    [Column("runtime")]



    [Column("votes")]
}