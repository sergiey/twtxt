using twtxt;

internal class Program
{
    public static void Main(string[] args)
    {
        Twtxt twtxt = new Twtxt();
        if (args.Length < 1) {
            twtxt.ShowHelp();
            return;
        }

        try
        {
            switch (args[0].ToLower())
            {
                case "tweet":
                    if (args.Length < 1)
                        throw new ArgumentOutOfRangeException();
                    twtxt.MakeTweet(args[1]);
                    break;
                case "timeline":
                    twtxt.ShowTimeline();
                    break;
                case "help":
                    twtxt.ShowHelp();
                    break;
                case "following":
                    twtxt.ShowFollowing();
                    break;
                case "follow":
                    if (args.Length < 2)
                        throw new ArgumentOutOfRangeException();
                    twtxt.Follow(args[1], args[2]);
                    break;
                case "unfollow":
                    if (args.Length < 1)
                        throw new ArgumentOutOfRangeException();
                    twtxt.Unfollow(args[1]);
                    break;
                default:
                    twtxt.ShowHelp();
                    break;
            }
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine("Invalid number of parameters.");
            twtxt.ShowHelp();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            twtxt.ShowHelp();
        }
    }
}
