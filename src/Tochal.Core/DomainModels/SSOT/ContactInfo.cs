using System.Collections.Generic;

namespace Tochal.Core.DomainModels.SSOT
{
    public class ContactInfo
    {
        public string Title { get; set; }
        public string PhoneNumber { get; set; }
        public string Logo { get; set; }
        public string EmailLogo { get; set; }
        public string LoginLogo { get; set; }
        public string SiteTitle { get; set; }
        public string SiteTitleLink { get; set; }
        public string SiteSlogan { get; set; }
        public string SiteDescription { get; set; }
        public string CvWebsiteUrl { get; set; }
        public bool HideSiteTitleOnLogin  { get; set; }
        public string FontFa { get; set; }
        public List<string> LoginImages { get; set; }
        public string TechnologicallyPhoneNumber { get; set; }
        public string SupportPhoneNumber { get; set; }
        public string InventionNumber { get; set; }
    }
}