namespace Objectivity.Bot.HtmlBuilder.Tests
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;

    public class HtmlBuilderTests
    {
        [Fact]
        public void Whether_HtmlBuilder_BoldsText_On_Bold()
        {
            var unit = new HtmlBuilder();
            var text = "bold this!";

            unit.Bold(text);

            var result = unit.Build();

            result.Should().Be($"<b>{text}</b>");
        }

        [Fact]
        public void Whether_HtmlBuilder_AppendLine_On_AppendLine()
        {
            var text = "text";
            var unit = new HtmlBuilder(text);

            unit.AppendLine();

            var result = unit.Build();

            result.Should().Be($"{text}<br />");
        }

        [Fact]
        public void Whether_HtmlBuilder_CreatesHyperLink_On_Link()
        {
            var unit = new HtmlBuilder();

            unit.Link("http://google.com", "google");

            var result = unit.Build();

            result.Should().Be("<a href=\"http://google.com\">google</a>");
        }

        [Fact]
        public void Whether_HtmlBuilder_AppendsText_On_Append()
        {
            var text = "text";
            var unit = new HtmlBuilder(text);

            unit.Append(text);

            var result = unit.Build();

            result.Should().Be(text + text);
        }

        [Fact]
        public void Whether_HtmlBuilder_ProducesBoldLink_On_BoldInLink()
        {
            var unit = new HtmlBuilder();

            unit.Link("http://google.com", "google", Decoration.Bold);

            var result = unit.Build();

            result.Should().Be("<a href=\"http://google.com\"><b>google</b></a>");
        }

        [Fact]
        public void Whether_HtmlBuilder_ProducesList_On_List()
        {
            var unit = new HtmlBuilder();

            unit.List(new List<string>() {"a", "b"});

            var result = unit.Build();

            result.Should().Be("<ul><li>a</li><li>b</li></ul>");
        }

        [Fact]
        public void Whether_HtmlBuilder_BoldsSecondItem_On_List()
        {
            var unit = new HtmlBuilder();

            List<Tuple<string, Decoration>> list = new List<Tuple<string, Decoration>>();
            list.Add(new Tuple<string, Decoration>("a", Decoration.None));
            list.Add(new Tuple<string, Decoration>("b", Decoration.Bold));
            unit.List(list);

            var result = unit.Build();

            result.Should().Be("<ul><li>a</li><li><b>b</b></li></ul>");
        }

        [Fact]
        public void Whether_HtmlBuilder_ProducesSimpleSite_On_MulitipleCalls()
        {
            var unit = new HtmlBuilder();

            unit.Link("http://test.com", "test").
                AppendLine().
                Link("http://google.com", "google", Decoration.Bold).
                AppendLine().
                List(new List<string>() {"a", "b"}).
                AppendLine().
                Append("footer");

            var result = unit.Build();

            result.Should().Be("<a href=\"http://test.com\">test</a><br /><a href=\"http://google.com\"><b>google</b></a><br /><ul><li>a</li><li>b</li></ul><br />footer");
        }
    }
}
