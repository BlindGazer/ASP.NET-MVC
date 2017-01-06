using System;
using System.Text;
using System.Web.Mvc;
using FastBus.DAL.Objects;

namespace FastBus.Web.Helpers
{
    public static class PagingHelper
    {
        private static TagBuilder GetTag(int page, int curPage, string text = null)
        {
            var tag = new TagBuilder("a");

            tag.AddCssClass("item");
            tag.MergeAttribute("page", page.ToString());
            tag.InnerHtml = text ?? page.ToString();
            if (page == curPage && text == null)
            {
                tag.AddCssClass("current");
            }
            return tag;
        }
        public static MvcHtmlString PageLinks(this HtmlHelper html, Paging paging, int total)
        {
            var result = new StringBuilder();
            var tag = new TagBuilder("div");
            string firstText = "<span class='fa fa-angle-double-left'></span>",
                lastText = "<span class='fa fa-angle-double-right'></span>";
            tag.AddCssClass("pagination");
            var totalPages = (int)Math.Ceiling((double)total/paging.Length);
            if (totalPages <= 9)
            {
                for (var i = 1; i <= totalPages; i++)
                {
                    result.Append(GetTag(i, paging.Page));
                }
            }
            else if (totalPages - paging.Page <= 4)
            {
                result.Append(GetTag(1, paging.Page, firstText));
                for (var i = totalPages - 8; i <= totalPages; i++)
                {
                    result.Append(GetTag(i, paging.Page));
                }
            }
            else if (totalPages - paging.Page > 4)
            {
                if (paging.Page > 5)
                {
                    result.Append(GetTag(1, paging.Page, firstText));
                    for (var i = paging.Page - 4; i <= paging.Page + 4; i++)
                    {
                        result.Append(GetTag(i, paging.Page));
                    }
                    result.Append(GetTag(totalPages, paging.Page, lastText));
                }
                else
                {
                    for (var i = 1; i <= 9; i++)
                    {
                        result.Append(GetTag(i, paging.Page));
                    }
                    result.Append(GetTag(totalPages, paging.Page, lastText));
                }
            }
            tag.InnerHtml = result.ToString();
            return MvcHtmlString.Create(tag.ToString());
        }
    }
}