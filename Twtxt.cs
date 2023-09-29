namespace twtxt;

public class Twtxt
{
    public static string Help()
    {
        return
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
            """;
    }
}
