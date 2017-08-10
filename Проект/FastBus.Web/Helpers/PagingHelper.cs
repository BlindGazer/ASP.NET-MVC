using System.Text;
using System.Web.Mvc;
using FastBus.Domain.Objects;

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

        public static MvcHtmlString PageLinks(this HtmlHelper html, QueryResult query)
        {
            var result = new StringBuilder();
            var tag = new TagBuilder("div");
            var lastPage = query.LastPage;
            const string firstText = "<span class='fa fa-angle-double-left'></span>",
                lastText = "<span class='fa fa-angle-double-right'></span>";

            tag.AddCssClass("pagination");
            if (lastPage <= 9)
            {
                for (var i = 1; i <= lastPage; i++)
                {
                    result.Append(GetTag(i, query.Paging.Page));
                }
            }
            else if (lastPage - query.Paging.Page <= 4)
            {
                result.Append(GetTag(1, query.Paging.Page, firstText));
                for (var i = lastPage - 8; i <= lastPage; i++)
                {
                    result.Append(GetTag(i, query.Paging.Page));
                }
            }
            else if (lastPage - query.Paging.Page > 4)
            {
                if (query.Paging.Page > 5)
                {
                    result.Append(GetTag(1, query.Paging.Page, firstText));
                    for (var i = query.Paging.Page - 4; i <= query.Paging.Page + 4; i++)
                    {
                        result.Append(GetTag(i, query.Paging.Page));
                    }
                    result.Append(GetTag(lastPage, query.Paging.Page, lastText));
                }
                else
                {
                    for (var i = 1; i <= 9; i++)
                    {
                        result.Append(GetTag(i, query.Paging.Page));
                    }
                    result.Append(GetTag(lastPage, query.Paging.Page, lastText));
                }
            }
            tag.InnerHtml = result.ToString();
            return MvcHtmlString.Create(tag.ToString());
        }
    }
}