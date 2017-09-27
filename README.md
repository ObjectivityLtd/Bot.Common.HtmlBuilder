# Bot.Common.HtmlBuilder

This is a library wchich allows user to add HTML tags from C# code in order to make Skype for Business channel for [Bot framework](https://dev.botframework.com/)

### implemented HTML Tags:
- Bold
- Italic
- New line
- Link
- List (unordered) 

### Example
```cs   
IHtmlBuilder builder = new HtmlBuilder("sample text");
builder.AppendLine("new line goes here ").Bold("something important");
```
