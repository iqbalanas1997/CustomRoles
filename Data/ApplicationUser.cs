using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations.Schema;


namespace CustomRoles.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }

        public int? BusinessId { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("BusinessId")]
        public virtual Business Business { get; set; }

    }
   
}

