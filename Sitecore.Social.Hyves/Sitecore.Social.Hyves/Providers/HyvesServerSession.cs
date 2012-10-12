using HyvesMethod = Hyves.Service.Core.HyvesMethod;
using HyvesSession = Hyves.Service.Core.HyvesSession;

namespace Sitecore.Social.Hyves.Providers
{
  using System.Collections.Generic;

  public class HyvesServerSession : HyvesSession
  {
    private string referrer;

    public HyvesSessionState SaveState()
    {
      return new HyvesSessionState
      {
        ExpireDate = this.ExpireDate,
        Referrer = this.referrer,
        Token = this.Token,
        TokenSecret = this.TokenSecret,
        UserId = this.UserId
      };
    }

    public void LoadState(HyvesSessionState hyvesSessionState)
    {
      this.referrer = hyvesSessionState.Referrer;

      this.StateLoaded = true;
    }

    protected bool StateLoaded { get; set; }

    public HyvesServerSession(string consumerKey, string consumerSecret, List<HyvesMethod> methods)
      : base(consumerKey, consumerSecret, methods)
    {

    }
  }
}