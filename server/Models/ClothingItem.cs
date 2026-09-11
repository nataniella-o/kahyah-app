using System.ComponentModel.DataAnnotations; //for table
using System.ComponentModel.DataAnnotations.Schema;  //for particular database schema

namespace server.Models;
[Table("clothing_items")]
public class ClothingItem{

    [Key]
    public Guid Id {get; set;}

    [Column("user_id")]
    public Guid User_id {get; set;} //guid is used to convert the uuid in supabase.

    [Column("photo_url")]
    public string?  PhotoUrl {get; set;}

    [Column("category_id")]
    public Guid CategoryId {get; set;}

    //we dont use the Column ("") for these fields because the names directly match the table names e.g. Color=color in supabase.cd
    public string? Color {get; set;}

    public string? Style {get; set;}

    public string? Season {get; set;}

    [Column("created_at")]
    public DateTime CreatedAt {get; set;}

}