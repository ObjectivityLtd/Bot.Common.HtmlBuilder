namespace Objectivity.Bot.HtmlBuilder
{
    public interface IHtmlListBuilder
    {
        IHtmlListBuilder Append(string text);

        IHtmlListBuilder AppendLine();

        IHtmlListBuilder AppendListItem(string li);

        string Build();
    }
}