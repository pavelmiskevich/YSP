using System;
using System.Collections.Generic;
using System.Text;
using YSP.Search.Core.Interfaces;

namespace YSP.Search.Core.Structures
{
    /// <summary>
    /// Struct result searching Google
    /// </summary>
    public class GoogleSearchResult : ISearchResult
    {
        public string Kind { get; set; }
        public string Title { get; set; }
        public string HtmlTitle { get; set; }
        //public string link { get; set; }
        public string Domain { get; set; }
        public string DisplayLink { get; set; }
        public string Snippet { get; set; }
        public string HtmlSnippet { get; set; }
        public string CacheId { get; set; }
        public string FormattedUrl { get; set; }
        public string HtmlFormattedUrl { get; set; }
        public int Position { get; set; }

        public GoogleSearchResult() { }
        public GoogleSearchResult(string title, string domain, int position)
        {
            Title = title;
            Domain = domain;
            Position = position;
        }

        //public int kind { get; set; }
        //public int url { get; set; }
        //public int queries { get; set; }
        //public int context { get; set; }
        //public int searchInformation { get; set; }
        //public List<Item> items { get; set; }
    }

    public class Item
    {
        public string kind { get; set; }
        public string title { get; set; }
        public string htmlTitle { get; set; }
        public string link { get; set; }
        public string displayLink { get; set; }
        public string snippet { get; set; }
        public string htmlSnippet { get; set; }
        public string cacheId { get; set; }
        public string formattedUrl { get; set; }
        public string htmlFormattedUrl { get; set; }
    }
}
