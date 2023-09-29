using twtxt.Models;

namespace twtxt;

public class Twtxt
{
    private readonly string _textFile = "twtxt.txt";

    public void MakeTweet(string text)
    {
        Tweet tw = new Tweet(text);
        FileInfo fi = new FileInfo(_textFile);

        if(fi.Exists)
        {
            using StreamWriter sw = fi.AppendText();
            sw.WriteLine($"{tw.AbsoluteDateTime}\t{tw.Text}");
        }
        else
        {
            fi.Create();
            using StreamWriter sw = fi.CreateText();
            sw.WriteLine($"{tw.AbsoluteDateTime}\t{tw.Text}");
        }

    }

    public void ShowTimeline()
    {
    }

    public void ShowHelp()
    {
        Console.WriteLine(
            """
            Usage:
                    ./twtxtc [COMMAND]

            Commands:
                    tweet <text>            Adds <text> to your twtxt timeline.
                    timeline                Displays your twtxt timeline.
                    following               Gives you a list of all people you follow.
                    follow <user> <URL>     Adds the twtxt file from <URL> to your timeline.
                                            <user> defines the user name to display.
                    unfollow <user>         Removes the user with the display name <user> from your timeline.
                    help                    Displays this help screen.
            """);
    }
}
