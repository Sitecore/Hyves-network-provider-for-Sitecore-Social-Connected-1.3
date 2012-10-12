// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Login with Hyves.ascx.cs" company="Sitecore A/S">
//   Copyright (C) 2011 by Sitecore A/S
// </copyright>
// <summary>
//   Defines the Login_with_Hyves type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Sitecore.Social.Hyves.Connector.Controls
{
  using System;
  using System.Web.UI;
  using Sitecore.Globalization;
  using Sitecore.Social.Core.Connector;

  /// <summary>
  /// Organizes the work of the LoginWithHyves control.
  /// </summary>
  public partial class LoginWithHyves : UserControl
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!Sitecore.Context.User.IsAuthenticated)
      {
        this.hyvesLoginButton.ToolTip = Translate.Text(Common.Texts.LoginWithHyves);
      }
      else
      {
        this.hyvesLoginButton.ToolTip = Translate.Text(Common.Texts.AttachHyvesAccount);
      }
    }

    /// <summary>
    /// Processing pressing of googleLoginButton button.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.
    /// </param>
    protected void HyvesLoginButtonClick(object sender, ImageClickEventArgs e)
    {
      var connectUserManager = new ConnectUserManager();
      const bool IsAsyncProfileUpdate = true;

      if (!Sitecore.Context.User.IsAuthenticated)
      {
        connectUserManager.LoginUser("Hyves", IsAsyncProfileUpdate);
      }
      else
      {
        connectUserManager.AttachUser("Hyves", IsAsyncProfileUpdate);
      }
    }
  }
}