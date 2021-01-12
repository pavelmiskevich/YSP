using System;
using System.Collections.Generic;
using System.Text;
using YSP.Search.Core.Interfaces;

namespace YSP.Search.Core.Structures
{
    /// <summary>
    /// Struct result searching
    /// </summary>
    public struct SearchResult
    {
        public int Position;
        //url
        public string DisplayUrl,
        //saved-copy-url
        CacheUrl,
        //title
        Title,
        //headline
        Description,
        //modtime
        IndexedTime,
        Domain;

        public SearchResult(int position, string url,
                   string cacheUrl,
                   string title,
                   string description,
                   string indexedTime,
            string domain)
        {
            this.Position = position;
            this.DisplayUrl = url;
            this.CacheUrl = cacheUrl;
            this.Title = title;
            this.Description = description;
            this.IndexedTime = indexedTime;
            this.Domain = domain;
        }
    }
}
