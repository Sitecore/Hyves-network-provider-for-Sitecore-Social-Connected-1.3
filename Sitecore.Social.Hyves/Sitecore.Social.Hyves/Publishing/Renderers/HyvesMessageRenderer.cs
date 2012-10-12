namespace Sitecore.Social.Hyves.Publishing.Renderers
{
  using System.Globalization;
  using System.Text;
  using System.Web;
  using Sitecore.Social.Core.Publishing.Items;
  using Sitecore.Social.Core.Publishing.Renderers;
  using Sitecore.Social.Core.Publishing.Renderers.Strategies;
  using Sitecore.Social.Hyves.Publishing.Items;

  public class HyvesMessageRenderer : IMessageRenderer
  {
    /// <summary>
    /// Renders the message.
    /// </summary>
    /// <param name="socialMessageBase">The social message base.</param>
    /// <param name="sourceName">Name of the source.</param>
    /// <param name="messageTextRenderStrategy">The message text render strategy.</param>
    /// <returns>
    /// The rendered html.
    /// </returns>
    public string RenderMessage(SocialMessageBase socialMessageBase, string sourceName, IMessageTextRenderStrategy messageTextRenderStrategy)
    {
      var hyvesMessage = new HyvesMessage(socialMessageBase.MessageItem);

      var stringBuilder = new StringBuilder();

      var anchorTag = string.Format(CultureInfo.CurrentCulture, @"<a href=""{0}"" target=""_blank"">{0}</a>", Core.Publishing.Managers.LinkManager.GenerateLink(hyvesMessage.Link, hyvesMessage.CampaignId));

      var messageText = messageTextRenderStrategy.Render(socialMessageBase.MessageItem.ID, sourceName, HttpUtility.HtmlEncode(hyvesMessage.Message));

      stringBuilder.Append(messageText);

      if (messageText.Contains("$link"))
      {
        stringBuilder = stringBuilder.Replace("$link", anchorTag);
      }
      else
      {
        stringBuilder.Append("</br>");
        stringBuilder.Append(anchorTag);
      }

      return stringBuilder.ToString();
    }

    /// <summary>
    /// Gets the message content CSS class.
    /// </summary>
    /// <returns>
    /// The css class name.
    /// </returns>
    public string GetMessageContentCssClass()
    {
      return string.Empty;
    }
  }
}