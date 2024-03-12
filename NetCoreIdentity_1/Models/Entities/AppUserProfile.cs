namespace NetCoreIdentity_1.Models.Entities
{
    public class AppUserProfile : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //Relational Props
        public virtual AppUser AppUser { get; set; }
    }
}
