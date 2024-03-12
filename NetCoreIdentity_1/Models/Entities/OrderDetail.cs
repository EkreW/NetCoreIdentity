namespace NetCoreIdentity_1.Models.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }

        //Relational Props
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
