using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.models
{
    public class FbComment
    {
        [Key]
        public int CommentId { get; set; }
        public int FbId {  get; set; }
        [ForeignKey("FbId")]
        public int AdminId { get; set; }
        [ForeignKey("AdminId")]
        public string CommentText { get; set; } = string.Empty;

    }
}
