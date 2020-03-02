using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tochal.Core.DomainModels.Entity
{
    public class FooterInfo
    {
        public int Id { get; set; }

        public string Logo { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string TelegramLink { get; set; }
        public string InstagramLink { get; set; }
        public string PintrestLink { get; set; }
        public string WhatsAppLink { get; set; }

    }

    public class FooterColleague
    {
        public int Id { get; set; }

        public int FooterId { get; set; }

        public string Link { get; set; }

        public string Image { get; set; }

        public string Title { get; set; }
    }

    public class FooterColleagueViewModel
    {
        public int Id { get; set; }

        public int FooterId { get; set; }

        public string Link { get; set; }

        public string Image { get; set; }

        public string Title { get; set; }
    }
}
