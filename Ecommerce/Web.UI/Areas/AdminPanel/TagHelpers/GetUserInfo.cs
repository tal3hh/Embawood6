using DomainLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.UI.Areas.AdminPanel.TagHelpers
{
    [HtmlTargetElement("getUserInfo")]
    public class GetUserInfo : TagHelper
    {
        public int UserId { get; set; }
        private readonly UserManager<AppUser> userManager;

        public GetUserInfo(UserManager<AppUser> userManager )
        {
            this.userManager = userManager;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var html = "";

            var user = await userManager.Users.SingleOrDefaultAsync(x=> x.Id == UserId);
            var roles = await userManager.GetRolesAsync(user);

            foreach (var item in roles)
            {
                html += item + " ";
            }

            output.Content.SetHtmlContent(html);
        }
    }
}
