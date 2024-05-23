using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.models
{
    public class Feedback
    {
        public int FbId { get; set; }
        public string Comment {  get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public int FId {  get; set; }
        [ForeignKey("FId")]
        public int Rating {  get; set; }
        public int CommentId { get; set; }
        [ForeignKey("CommentId")]

        public Menu Menu { get; set; }
        public Customer Customer { get; set; }
        public FbComment FbComment { get; set; }
    }
}
