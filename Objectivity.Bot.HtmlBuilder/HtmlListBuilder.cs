namespace Objectivity.Bot.HtmlBuilder
{
    using System.Text;

    public class HtmlListBuilder : IHtmlListBuilder
    {
        private StringBuilder html = new StringBuilder();
        private string listType;

        public HtmlListBuilder() : this("ul")
        {
            
        }

        public HtmlListBuilder(string listType)
        {
            this.listType = listType;
            this.html.Append($"<{listType}>");
        }

        public IHtmlListBuilder Append(string text)
        {
            this.html.Append(text);
            return this;
        }

        public IHtmlListBuilder AppendLine()
        {
            this.html.Append($"<br />");
            return this;
        }

        public IHtmlListBuilder AppendListItem(string li)
        {
            this.html.Append($"<li style=\"color: {HtmlBuilder.ColorBlue}\"><span style=\"color: black\">");
            this.html.Append(li);
            this.html.Append("</span></li>");
            return this;
        }

        public string Build()
        {
            this.html.Append($"</{this.listType}>");
            return this.html.ToString();
        }
    }
}