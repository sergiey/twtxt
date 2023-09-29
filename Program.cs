using twtxt;
using twtxt.Models;
using static twtxt.Manual;

internal class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 1) {
            Console.WriteLine(Help());
            return;
        }
        args[0].ToLower() switch
        {
            "tweet"     => MakeTweet(args[1]),
            "timeline"  => ShowTimeline(),
            "following" => ShowFollowing(),
            "follow"    => Follow(args[1], args[2]),
            "unfollow"  => Unfollow(args[1]),
            "help"      => Help(),
            "man"       => Help(),
            _           => Help()
        };
    }
}
