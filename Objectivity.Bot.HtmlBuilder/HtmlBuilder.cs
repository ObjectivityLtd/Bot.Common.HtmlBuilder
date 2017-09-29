namespace Objectivity.Bot.HtmlBuild
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class HtmlBuilder : IHtmlBuilder
    {
        public const string ColorBlue = "#0066CC";

        public const string ColorGrey = "#808080";

        public const string ColorOrange = "#ff7f32";

        private const string Space = "&nbsp;";

        private static readonly Dictionary<Decoration, Func<string, string>> Decorations =
            new Dictionary<Decoration, Func<string, string>>()
                {
                    { Decoration.None, s => s },
                    { Decoration.Bold, s => $"<b>{s}</b>" },
                    { Decoration.Italic, s => $"<i>{s}</i>" },
                    {
                        Decoration.SegoeUI,
                        s =>
                            $"<span style=\"font-family: Segoe UI;font-size: 13px;\">{s}</span>"
                    }
                };

        private readonly StringBuilder html;

        public HtmlBuilder()
        {
            this.html = new StringBuilder();
        }

        public HtmlBuilder(string text)
        {
            this.html = new StringBuilder(text);
        }

        public static string BoldsText(string text) => Decorations[Decoration.Bold](text);

        public static string BoldsTextIf(string text, Func<bool> condition)
        {
            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition() ? Decorations[Decoration.Bold](text) : string.Empty;
        }

        public static string ColorText(string text, string color)
        {
            return $"<span style=\"font-family:Segoe UI;font-size: 13px;color: {color};\">{text}</span>";
        }

        public static string GenerateLink(string source, string text, Decoration decoration = Decoration.None)
        {
            return $"<a style=\"color: {ColorBlue}\" href=\"{source}\">{Decorations[decoration](text)}</a>";
        }

        public IHtmlBuilder Append(string text)
        {
            this.html.Append(text);
            return this;
        }

        public IHtmlBuilder AppendIf(string text, Func<bool> condition)
        {
            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (condition())
            {
                return this.Append(text);
            }

            return this;
        }

        public IHtmlBuilder AppendIf(Func<string> text, Func<bool> condition)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (condition())
            {
                return this.Append(text());
            }

            return this;
        }

        public IHtmlBuilder AppendLine()
        {
            this.AppendLine(string.Empty);
            return this;
        }

        public IHtmlBuilder AppendLine(string text, Decoration decoration = Decoration.None)
        {
            this.html.Append($"<br />{Decorations[decoration](text)}");
            return this;
        }

        public IHtmlBuilder AppendLineIf(string text, Func<bool> condition, Decoration decoration = Decoration.None)
        {
            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (condition())
            {
                return this.AppendLine(text, decoration);
            }

            return this;
        }

        public IHtmlBuilder AppendLineIf(
            Func<string> text,
            Func<bool> condition,
            Decoration decoration = Decoration.None)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition() ? this.AppendLine(text(), decoration) : this;
        }

        public IHtmlBuilder AppendSpace()
        {
            this.html.Append(Space);
            return this;
        }

        public IHtmlBuilder Bold(string text)
        {
            this.html.Append(BoldsText(text));
            return this;
        }

        public string Build() => this.html.ToString();

        public IHtmlBuilder Color(string text, string colorType)
        {
            this.html.Append(ColorText(text, colorType));
            return this;
        }

        public IHtmlBuilder Link(string source, string text)
        {
            this.Link(source, text, Decoration.None);
            return this;
        }

        public IHtmlBuilder Link(string source, string text, Decoration decoration)
        {
            this.html.Append(GenerateLink(source, text, decoration));
            return this;
        }

        public IHtmlBuilder LinkIf(Func<string> source, Func<string> text, Func<bool> condition)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (condition())
            {
                this.Link(source(), text(), Decoration.None);
            }

            return this;
        }

        public IHtmlBuilder List(IEnumerable<string> messages)
        {
            var tuples = messages.Select(s => new Tuple<string, Decoration>(s, Decoration.None));
            this.List(tuples);
            return this;
        }

        public IHtmlBuilder List(IEnumerable<Tuple<string, Decoration>> tuples, string bulletColor = ColorBlue)
        {
            if (tuples == null)
            {
                throw new ArgumentNullException(nameof(tuples));
            }

            this.html.Append("<ul>");
            foreach (var tuple in tuples)
            {
                this.html.Append($"<li style=\"color: {bulletColor}\"><span style=\"color: black\">");
                this.html.Append(Decorations[tuple.Item2](tuple.Item1));
                this.html.Append("</span></li>");
            }

            this.html.Append("</ul>");

            return this;
        }
    }
}