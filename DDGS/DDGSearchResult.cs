using System;
using System.Collections.Generic;
using System.Text;

namespace DDGS
{
    public class DDGSearchResult
    {
        public string Title { get; set; }
        public string Href { get; set; }
        public string Body { get; set; }
    }

    public class DDGSearchPayload
    {
        public string q { get; set; }
    }
}
