using System.Text;
using System.Text.Json;
using twtxt.Models;

namespace twtxt;

public class Twtxt
{
    private readonly string _textFile = "twtxt.txt";
    private readonly string _configFile = ".twtxtconfig";

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
            using StreamWriter sw = new StreamWriter(fi.Create(), Encoding.UTF8);
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

    public void ShowFollowing()
    {
        if(File.Exists(_configFile))
        {
            try
            {
                string jsonString = File.ReadAllText(_configFile);
                FollowList? followList = JsonSerializer.Deserialize<FollowList>(jsonString);
                Console.WriteLine("You are following:");
                if (followList?.Following != null)
                {
                    foreach (Source src in followList.Following)
                        Console.Write($"@{src.Nick}  ");
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        else
            Console.WriteLine($"The config file ({_configFile}) does not exist.");
    }

    public void Follow(string username, string url)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var followList = new FollowList();
        var newSource = new Source(username, url);

        try
        {
            followList = GetFollowListFromConfigFile();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        using(FileStream fs = new FileStream(_configFile, FileMode.Create))
        {
            followList?.Following.Add(newSource);
            JsonSerializer.Serialize(fs, followList, options);
        }

    }

    public void Unfollow(string username)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var followList = new FollowList();
        var removingSource = new Source(username);

        try
        {
            followList = GetFollowListFromConfigFile();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        if(followList.Following.Exists(src => src.Nick == removingSource.Nick))
        {
            using FileStream fs = new FileStream(_configFile, FileMode.Create);
            followList.Following.RemoveAll(src => src.Nick == removingSource.Nick);
            JsonSerializer.Serialize(fs, followList, options);
        }
        else
            Console.WriteLine("You do not follow this user.");
    }

    private FollowList GetFollowListFromConfigFile()
    {
        if(File.Exists(_configFile))
        {
            using FileStream fs = new FileStream(_configFile, FileMode.OpenOrCreate);
            return JsonSerializer.Deserialize<FollowList>(fs);
        }
        return new FollowList();
    }
}
