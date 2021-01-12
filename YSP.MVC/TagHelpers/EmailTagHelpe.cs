using Microsoft.AspNetCore.Razor.TagHelpers;

namespace YSP.MVC.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public string EmailName { get; set; }
        public string EmailDomain { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";  // Заменяет <email> дескриптором <a>
            var address = EmailName + "@" + EmailDomain;
            output.Attributes.SetAttribute("href ", "mailto:" + address);
            output.Content.SetContent(address);
        }
    }
}
