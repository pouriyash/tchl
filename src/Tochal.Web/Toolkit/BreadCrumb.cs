﻿using System.Collections.Generic;

namespace Research.City.Admin.Toolkit
{
    public class BreadCrumb
    {
        public List<BreadCrumbLink> Links { get; set; }
    }
    
    public class BreadCrumbLink
    {
        public string Title { get; set; }
        public string Link { get; set; }
    }
}