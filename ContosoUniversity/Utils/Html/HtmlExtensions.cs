using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Utils.Html
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString DisplayNameFor<TModel, TValue>(this HtmlHelper<IPagedList<TModel>> html, Expression<Func<TModel, TValue>> expression)
        {
            return DisplayNameForInternal(html, expression);
        }

        internal static MvcHtmlString DisplayNameForInternal<TModel, TValue>(this HtmlHelper<IPagedList<TModel>> html, Expression<Func<TModel, TValue>> expression)
        {
            return DisplayNameHelper(ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>()),
                                     ExpressionHelper.GetExpressionText(expression));
        }

        internal static MvcHtmlString DisplayNameHelper(ModelMetadata metadata, string htmlFieldName)
        {
            string resolvedDisplayName = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            return new MvcHtmlString(HttpUtility.HtmlEncode(resolvedDisplayName));
        }
    }
}