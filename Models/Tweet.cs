using System.Globalization;

namespace twtxt.Models;

public class Tweet
{
    private readonly DateTime _createdAt;
    public string Text { get; }
    public Source? Src { get; }
    public string AbsoluteDateTime => $"{_createdAt:s}{_createdAt:zzz}";
    public string RelativeDateTime => ToRelativeDateTime(_createdAt);

    public Tweet(string text, DateTime? createdAt = null, Source? src = null)
    {
        Text = text;
        if (createdAt == null)
            _createdAt = DateTime.Now;
        if(src != null)
            Src = src;
    }

    private string ToRelativeDateTime(DateTime dateTime)
    {
        TimeSpan timeSpan = DateTime.Now.Subtract(dateTime);
        return timeSpan.TotalSeconds switch {
            <= 60 => $"about {timeSpan.Seconds} seconds ago",
            _ => timeSpan.TotalMinutes switch
            {
                <= 1 =>"about a minute ago",
                < 60 => $"{timeSpan.Minutes} mimutes ago",
                _ => timeSpan.TotalHours switch
                {
                    <= 1 => "an hour ago",
                    < 24 => $"{timeSpan.Hours} hours ago",
                    _ => timeSpan.TotalDays switch
                    {
                        <= 1 => "yesterday",
                        < 30 => $"{timeSpan.Days} days ago",
                        <= 60  => "about a  month ago",
                        < 365 => $"{timeSpan.Days / 30} months ago",
                        < 730 => "about a year ago",
                        _ => $"about {timeSpan.Days / 365} years ago"
                    }
                }
            }
        };
    }
}
