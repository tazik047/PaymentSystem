using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PaymentSystem.Util
{
    public static class TableHelper
    {
        public static MvcHtmlString CreateBootstrapTable(this HtmlHelper helper, string jsonUrl, List<ColumnItem> columns, string click = "", string attributeFunction = "")
        {
            var tableTemplate = @"<table id='bootstrap-table' data-url='{0}'
                                           data-pagination='true' data-search='true'
                                           data-show-refresh='true' data-show-toggle='true'
                                           data-row-attributes='{1}'
                                           click-href='{2}'>
                                        <thead>
                                            <tr>
                                                {3}
                                                <th data-field='Id' data-visible='false'></th>
                                            </tr>
                                        </thead>
                                </table>";
            StringBuilder th = new StringBuilder();
            foreach (var column in columns)
            {
                string dateSort = "";
                if (column.Name.ToLower().Contains("date"))
                    dateSort = "data-sorter=\"sortDate\"";
                th.AppendFormat("<th data-field='{0}' data-sortable='true' {2}>{1}</th>", column.Name, column.Title, dateSort);
            }
            return new MvcHtmlString(string.Format(tableTemplate, jsonUrl, attributeFunction, click, th));
        }
    }
}
/*
<table id="bootstrap-table" data-url="@Url.Action("Accounts", new { Controller = "Json", id = ViewContext.RouteData.Values["id"] })"
           data-pagination="true" data-search="true"
           data-show-refresh="true" data-show-toggle="true"
           data-row-attributes="accountIsBlocked"
           click-href="@Url.Action("Details", "Card")">
        <thead>
            <tr>
                <th data-field="Name" data-sortable="true">@Html.DisplayNameFor(model => model.Card.Name)</th>
                <th data-field="Balance" data-sortable="true">@Html.DisplayNameFor(model => model.Balance)</th>
                <th data-field="CreationDate" data-sortable="true">@Html.DisplayNameFor(model => model.CreationDate)</th>
                <th data-field="Id" data-visible="false"></th>
            </tr>
        </thead>
</table>
*/