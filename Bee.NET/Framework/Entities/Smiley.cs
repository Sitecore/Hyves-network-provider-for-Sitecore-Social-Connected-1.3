// Copyright (c) 2010, Beemway. All Rights Reserved.

namespace Hyves.Service
{
  /// <summary>
  /// Represents a smiley.
  /// </summary>
  public sealed class Smiley : HyvesEntity
  {
    public Smiley()
    {
    }
    
    /// <summary>
    /// The code of the smiley.
    /// </summary>
    public string SmileyCode
    {
      get
      {
        return GetState<string>("smileycode");
      }
    }

    /// <summary>
    /// The category of the smiley.
    /// </summary>
    public string SmileyCategory
    {
      get
      {
        return GetState<string>("smileycategory");
      }
    }

    /// <summary>
    /// The url of the smiley.
    /// </summary>
    public string Url
    {
      get
      {
        return GetState<string>("url");
      }
    }
  }
}
