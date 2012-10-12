using HyvesExpirationType = Hyves.Service.Core.HyvesExpirationType;
using HyvesMethod = Hyves.Service.Core.HyvesMethod;
using HyvesRequest = Hyves.Service.Core.HyvesRequest;
using HyvesService = Hyves.Service.HyvesService;
using HyvesSession = Hyves.Service.Core.HyvesSession;
using HyvesVisibility = Hyves.Service.HyvesVisibility;
using User = Hyves.Service.User;

namespace Sitecore.Social.Hyves.Providers
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Linq;
  using System.Web;
  using Newtonsoft.Json.Linq;
  using Sitecore.Globalization;
  using Sitecore.Social.Core.Auth;
  using Sitecore.Social.Core.Connector;
  using Sitecore.Social.Core.Connector.Paths;
  using Sitecore.Social.Core.NetworkFields;
  using Sitecore.Social.Core.Networks;
  using Sitecore.Social.Core.Networks.Accounts;
  using Sitecore.Social.Core.Networks.Args;
  using Sitecore.Social.Core.Networks.Messages;
  using Sitecore.Social.Core.Networks.Providers;
  using Sitecore.Social.Core.Networks.Providers.Interfaces;

  public class HyvesProvider : NetworkProvider, IAuth, IGetAccountInfo, IPublishOnTheWall, IMessageStatistics
  {
    /// <summary>
    /// The "likes" counter key.
    /// </summary>
    public const string RespectsCounterKey = "respects";

    /// <summary>
    /// The "comments" counter key.
    /// </summary>
    public const string CommentsCounterKey = "comments";

    public HyvesProvider(Application application)
      : base(application)
    {
    }

    public void AuthGetCode(AuthArgs args)
    {
      var hyvesMethods = new List<HyvesMethod>
      {
        HyvesMethod.All
      };
      var hyvesSession = new HyvesServerSession(args.Application.ApplicationKey, args.Application.ApplicationSecret, hyvesMethods);

      var hyvesRequest = new HyvesRequest(hyvesSession);
      string tokenSecret;
      var requestToken = hyvesRequest.CreateRequestToken(out tokenSecret, HyvesExpirationType.Infinite);

      args.InternalData["requestToken"] = requestToken;
      args.InternalData["requestTokenSecret"] = tokenSecret;

      this.SaveHyvesSession(args, hyvesSession);

      var request = HttpContext.Current.Request;
      var oauthCallback = string.Format("{0}://{1}{2}?type=access&state={3}", request.Url.Scheme, request.Url.Host, Paths.SocialLoginHandlerPath, args.StateKey);

      var hyvesAuthUrl = string.Format("http://www.hyves.nl/api/authorize/?oauth_token={0}&oauth_callback={1}", requestToken, HttpUtility.UrlEncode(oauthCallback));
      HttpContext.Current.Response.Redirect(hyvesAuthUrl);
    }

    private HyvesServerSession LoadHyvesSession(AuthArgs authArgs)
    {
      var hyvesSessionState = (HyvesSessionState)authArgs.InternalData["hyvesSessionState"];
      var hyvesServerSession = new HyvesServerSession(authArgs.Application.ApplicationKey, authArgs.Application.ApplicationSecret, new List<HyvesMethod>());
      hyvesServerSession.LoadState(hyvesSessionState);
      return hyvesServerSession;
    }

    private void SaveHyvesSession(AuthArgs authArgs, HyvesServerSession hyvesServerSession)
    {
      var hyvesSessionState = hyvesServerSession.SaveState();
      authArgs.InternalData["hyvesSessionState"] = hyvesSessionState;
      new AuthManager().UpdateAuthArgs(authArgs);
    }

    public void AuthGetAccessToken(AuthArgs args)
    {
      var requestToken = args.InternalData["requestToken"] as string;
      var requestTokenSecret = args.InternalData["requestTokenSecret"] as string;

      var hyvesSession = this.LoadHyvesSession(args);

      var hyvesRequest = new HyvesRequest(hyvesSession);
      string userId;
      string accessTokenSecret;
      DateTime expireDate;
      var accessToken = hyvesRequest.CreateAccessToken(requestToken, requestTokenSecret, out accessTokenSecret, out userId, out expireDate);

      var authCompletedArgs = new AuthCompletedArgs
      {
        Application = args.Application,
        AccessToken = accessToken,
        AccessTokenSecret = accessTokenSecret,
        CallbackPage = args.CallbackPage,
        ExternalData = args.ExternalData,
        AttachAccountToLoggedInUser = args.AttachAccountToLoggedInUser,
        IsAsyncProfileUpdate = args.IsAsyncProfileUpdate
      };
      if (!string.IsNullOrEmpty(args.CallbackType))
      {
        this.InvokeAuthCompleted(args.CallbackType, authCompletedArgs);
      }
    }

    public string GetDisplayName(Account account)
    {
      var user = this.GetHyvesUser(account);
      return user != null ? string.Format("{0} {1}", user.Firstname, user.Lastname) : null;
    }

    public string GetAccountId(Account account)
    {
      var user = this.GetHyvesUser(account);
      return user != null ? user.UserId : null;
    }

    public AccountBasicData GetAccountBasicData(Account account)
    {
      var user = this.GetHyvesUser(account);
      return new AccountBasicData
      {
        Account = account,
        Email = null,
        FullName = string.Format("{0} {1}", user.Firstname, user.Lastname),
        Id = user.UserId
      };
    }

    private HyvesService GetHyvesService(Account account)
    {
      var hyvesService = new HyvesService(this.GetHyvesSession(account));

      return hyvesService;
    }


    private HyvesSession GetHyvesSession(Account account)
    {
      var hyvesSession = new HyvesSession(account.Application.ApplicationKey, account.Application.ApplicationSecret, new List<HyvesMethod>
      {
        HyvesMethod.All
      });

      hyvesSession.InitializeToken(account.AccessToken, account.AccessTokenSecret, DateTime.MinValue);

      return hyvesSession;
    }

    private User GetHyvesUser(Account account)
    {
      var hyvesService = this.GetHyvesService(account);
      return hyvesService.Users.GetLoggedinUser(true);
    }

    public IEnumerable<Field> GetAccountInfo(Account account, IEnumerable<FieldInfo> acceptedFields)
    {
      var hyvesSession = this.GetHyvesSession(account);

      // receives the method dictionatyt where key: method name (described in config), value: method enum value
      Dictionary<string, HyvesMethod> methodDescriptionDictionary = this.GetMethodDescriptionDictionary();

      var fieldsGroupedByAccess = acceptedFields.Where(field => field["method"] != null)
            .GroupBy(field => field["method"])
            .Select(fg => new { Method = fg.Key, Fields = fg.ToList() });

      foreach (var groupFields in fieldsGroupedByAccess)
      {
        var request = new HyvesRequest(hyvesSession);
        var hyvesResponse = request.InvokeMethod(methodDescriptionDictionary[groupFields.Method], false);

        // You'll find documentation here:
        // http://james.newtonking.com/projects/json/help/
        var jObject = JObject.Parse(hyvesResponse.RawResponse);

        foreach (var field in groupFields.Fields)
        {
          var token = jObject.SelectToken(field["selectToken"]);
          var value = (token != null) ? token.ToString() : null;
          value = this.TryToRemoveBrackets(value);
          if (!string.IsNullOrEmpty(value))
          {
            yield return new Field { Name = field.SitecoreKey, Value = value };
          }
        }
      }
    }

    private string TryToRemoveBrackets(string expresstion)
    {
      if (!string.IsNullOrEmpty(expresstion)
        && expresstion.StartsWith("\"")
        && expresstion.EndsWith("\""))
      {
        var filter = new[]
        {
          '"'
        };
        return expresstion.TrimStart(filter).TrimEnd(filter);

      }

      return expresstion;
    }

    private Dictionary<string, HyvesMethod> GetMethodDescriptionDictionary()
    {
      var result = new Dictionary<string, HyvesMethod>();
      var methods = Enum.GetValues(typeof(HyvesMethod));
      foreach (HyvesMethod method in methods)
      {
        var fieldInfo = method.GetType().GetField(method.ToString());
        var attributes =
              (DescriptionAttribute[])fieldInfo.GetCustomAttributes(
              typeof(DescriptionAttribute), false);

        foreach (var descriptionAttribute in attributes)
        {
          result.Add(descriptionAttribute.Description, method);
        }
      }

      return result;
    }

    public string PublishOnTheWall(Account account, Message message)
    {
      var accountId = this.GetAccountId(account);

      var hyvesService = this.GetHyvesService(account);
      hyvesService.Session.InitializeUserId(accountId);
      var www = hyvesService.Wwws.CreateWww(message.Text, HyvesVisibility.SuperPublic, null, null, null, null, null);
      return www.WwwId;
    }

    public List<string> StatisticNames
    {
      get
      {
        return new List<string> { RespectsCounterKey, CommentsCounterKey };
      }
    }

    public Dictionary<string, double> GetMessageStatistics(Account account, string messageId)
    {
      var accountId = this.GetAccountId(account);

      var hyvesService = this.GetHyvesService(account);
      hyvesService.Session.InitializeUserId(accountId);

      var respects = hyvesService.Wwws.GetRespects(messageId);
      var comments = hyvesService.Wwws.GetComments(messageId);

      var result = new Dictionary<string, double>();

      if (respects != null)
      {
        result.Add(RespectsCounterKey, respects.Count);
      }

      if (comments != null)
      {
        result.Add(CommentsCounterKey, comments.Count);
      }

      return result;
    }

    public string GetStatisticsCounterDisplayName(string statisticsCounterName)
    {
      if (string.Compare(RespectsCounterKey, statisticsCounterName, StringComparison.CurrentCultureIgnoreCase) == 0)
      {
        return Translate.Text(Common.Texts.Respects);
      }

      if (string.Compare(CommentsCounterKey, statisticsCounterName, StringComparison.CurrentCultureIgnoreCase) == 0)
      {
        return Translate.Text(Common.Texts.Comments);
      }

      return statisticsCounterName;
    }
  }
}