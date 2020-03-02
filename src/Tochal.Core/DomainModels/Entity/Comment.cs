using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tochal.Core.DomainModels.Entity
{
    public class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text{ get; set; }

        public bool IsShow { get; set; } = false;

        public string Answer { get; set; }

        public int EntityId { get; set; }
    }

    public class CommentViewModel
    {

        public string Name { get; set; }
        public string Text { get; set; }

        public bool IsShow { get; set; } = false;

        public string Answer { get; set; }
         
        public int EntityId { get; set; }
    }
    
}
