namespace Sitecore.Social.Hyves.Providers
{
  using System;

  [Serializable]
  public class HyvesSessionState
  {
    public string Token { get; set; }
    public string TokenSecret { get; set; }
    public DateTime ExpireDate { get; set; }
    public string UserId { get; set; }
    public string Referrer { get; set; }
  }
}