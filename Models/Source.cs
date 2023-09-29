namespace twtxt.Models;

public class Source
{
    public string Nick { get; }
    public string? Url { get; }
    public string? File { get; }

    public Source(string nick, string? url = null, string? file = null)
    {
        Nick = nick;
        if(url != null)
            Url = url;
        if(file != null)
            File = file;
    }
}
