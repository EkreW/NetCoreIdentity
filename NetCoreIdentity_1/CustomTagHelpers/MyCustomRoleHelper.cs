using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using NetCoreIdentity_1.Models.Entities;

namespace NetCoreIdentity_1.CustomTagHelpers
{
    [HtmlTargetElement("getUserInfo")] //Bu Helper'in amacı aldıgı kullanıcı nesnesinin rollerini tespit etmek ve rol isimlerini bir htmlstring'te cıkarmak olacaktır..
    public class MyCustomRoleHelper : TagHelper
    {
        readonly UserManager<AppUser> _userManager;

        public MyCustomRoleHelper(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public int UserID { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;

            IList<string> userRoles = await _userManager.GetRolesAsync(await _userManager.Users.FirstOrDefaultAsync(x => x.Id == UserID));

            foreach (string role in userRoles)
            {
                html += $"{role},";
            }
            html = html.TrimEnd(',');

            output.Content.SetHtmlContent(html);
        }
    }
}
