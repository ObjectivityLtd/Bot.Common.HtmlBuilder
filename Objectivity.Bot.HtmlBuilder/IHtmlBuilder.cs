namespace Objectivity.Bot.HtmlBuilder
{
    using System;
    using System.Collections.Generic;

    public interface IHtmlBuilder
    {
        IHtmlBuilder Append(string text);
        IHtmlBuilder AppendIf(string text, Func<bool> condition);
        IHtmlBuilder AppendIf(Func<string> text, Func<bool> condition);
        IHtmlBuilder AppendLine();
        IHtmlBuilder AppendLine(string text, Decoration decoration = Decoration.None);
        IHtmlBuilder AppendLineIf(string text, Func<bool> condition, Decoration decoration = Decoration.None);
        IHtmlBuilder AppendLineIf(Func<string> text, Func<bool> condition, Decoration decoration = Decoration.None);
        IHtmlBuilder AppendSpace();
        IHtmlBuilder Bold(string text);

        IHtmlBuilder Link(string source, string text);
        IHtmlBuilder LinkIf(Func<string> source, Func<string> text, Func<bool> condition);
        IHtmlBuilder Link(string source, string text, Decoration decoration);

        IHtmlBuilder List(IEnumerable<string> messages);

        IHtmlBuilder List(IEnumerable<Tuple<string, Decoration>> tuples, string bulletColor);

        IHtmlBuilder Color(string text, string color);

        string Build();
    }
}