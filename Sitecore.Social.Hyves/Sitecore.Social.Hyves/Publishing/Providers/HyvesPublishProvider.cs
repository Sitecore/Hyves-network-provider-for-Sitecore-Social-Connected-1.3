namespace Sitecore.Social.Hyves.Publishing.Providers
{
  using System;
  using System.Collections.Generic;
  using System.Text;
  using Sitecore.Social.Core.Networks;
  using Sitecore.Social.Core.Networks.Accounts;
  using Sitecore.Social.Core.Publishing.Messages;
  using Sitecore.Social.Core.Publishing.Providers;
  using Sitecore.Social.Core.Publishing.Utils;
  using Sitecore.Social.Hyves.Providers;

  /// <summary>
  /// The class prepares message for posting and posts in on the Hyves WWW (Who What Where)
  /// </summary>
  public class HyvesPublishProvider : PublishProviderBase
  {
    private const string AccessTokenFieldName = "AccessToken";
    private const string AccessTokenSecretFieldName = "AccessTokenSecret";
    private const string MessageFieldName = "Message";
    private const string LinkFieldName = "Link";

    /// <summary>
    /// Initializes a new instance of the <see cref="HyvesPublishProvider"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    public HyvesPublishProvider(Message message) : base(message)
    {
    }

    /// <summary>
    /// Posts the message on the Hyves WWW.
    /// </summary>
    public override void PublishAll()
    {
      this.Publish(account =>
      {
        var applicationKey = (string)null;
        var applicationSecret = (string)null;
        var accessToken = account[AccessTokenFieldName];
        var accessTokenSecret = account[AccessTokenSecretFieldName];

        if (account.Application != null)
        {
          applicationKey = account.Application.ApplicationKey;
          applicationSecret = account.Application.ApplicationSecret;
        }

        var networkApplication = new Application
        {
          ApplicationKey = applicationKey,
          ApplicationSecret = applicationSecret
        };

        var networkAccount = new Account
        {
          AccessToken = accessToken,
          AccessTokenSecret = accessTokenSecret,
          Application = networkApplication
        };

        var hyvesProvider = new HyvesProvider(networkApplication);

        var hyvesMessage = this.BuildHyvesMessage(this.Message);

        var messageId = hyvesProvider.PublishOnTheWall(networkAccount, new Core.Networks.Messages.Message
        {
          Text = hyvesMessage
        });

        return new PostResult { MessageId = messageId, Responce = new Dictionary<string, string>() };
      });
    }

    /// <summary>
    /// Builds the hyves message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns></returns>
    protected virtual string BuildHyvesMessage(Message message)
    {
      var messageText = new StringBuilder();

      if (!string.IsNullOrEmpty(message[LinkFieldName]))
      {
        var shortner = new GoogleUrlShortener();
        var link = message[LinkFieldName];
        var uri = shortner.ShortenUrl(new Uri(link));

        link = uri.AbsoluteUri;

        if (message[MessageFieldName].Contains("$link"))
        {
          messageText.Append(message[MessageFieldName].Replace("$link", link));
        }
        else
        {
          messageText.Append(message[MessageFieldName]);
          messageText.Append(" ");
          messageText.Append(link);
        }
      }
      else
      {
        messageText.Append(message[MessageFieldName].Replace("$link", string.Empty));
      }

      return messageText.ToString();
    }
  }
}