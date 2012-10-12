namespace Sitecore.Social.Hyves.Publishing.Items
{
  using Sitecore.Data.Items;
  using Sitecore.Social.Core.Publishing.Items;

  /// <summary>
  /// The class represents the wrapper on the Hyves message item.
  /// </summary>
  public class HyvesMessage : SocialMessageBase
  {
    private const string MessageFieldName = "Message";

    private const string LinkFieldName = "Link";

    private string message;

    private string link;

    /// <summary>
    /// Initializes a new instance of the <see cref="HyvesMessage"/> class.
    /// </summary>
    public HyvesMessage()
    {
      
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HyvesMessage"/> class.
    /// </summary>
    /// <param name="item">The item.</param>
    public HyvesMessage(Item item) : base(item)
    {
      
    }

    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    /// <value>The message.</value>
    public string Message
    {
      get
      {
        return this.messageItem.Fields[MessageFieldName].Value;
      }

      set
      {
        this.message = value;
      }
    }

    /// <summary>
    /// Gets or sets the link.
    /// </summary>
    /// <value>The link.</value>
    public string Link
    {
      get
      {
        return this.messageItem.Fields[LinkFieldName].Value;
      }

      set
      {
        this.link = value;
      }
    }

    /// <summary>
    /// Saves the data to the item.
    /// </summary>
    public override void SaveData()
    {
      base.SaveData();
      this.messageItem.Editing.BeginEdit();
      this.messageItem.Fields[LinkFieldName].Value = this.link;
      this.messageItem.Fields[MessageFieldName].Value = this.message;
      this.messageItem.Editing.EndEdit();
    }
  }
}