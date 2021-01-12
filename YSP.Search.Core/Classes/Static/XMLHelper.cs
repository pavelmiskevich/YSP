using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace YSP.Search.Core.Classes.Static
{
    public static class XMLHelper
    {
        /// <summary>
        /// Get value of xml element by name
        /// </summary>
        /// <param name="group">XElement</param>
        /// <param name="name">Name</param>
        /// <returns>Value if exists</returns>
        public static string GetValue(XElement group, string name)
        {
            return group.Element("doc").Element(name) == null ? string.Empty : group.Element("doc").Element(name).Value;
            //try
            //{
            //    return group.Element("doc").Element(name).Value;
            //}
            ////это если в результате нету элемента с каким то именем,
            ////то будет вместо значащей строчки возвращаться пустая.
            //catch
            //{
            //    return string.Empty;
            //}
        }
    }
}
