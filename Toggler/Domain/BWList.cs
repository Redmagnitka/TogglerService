using System.Collections.Generic;
using System;

namespace Toggler.Domain
{
  public class BWList : HashSet<App>
  {
    public enum ListColor
    {
      BLACK = 0,
      WHITE = 1
    }

    private ListColor _color;

    public BWList(ListColor color)
    {
      _color = color;
    }

    public bool AddToList(App appItem)
    {
      return Add(appItem);
    }

    public override bool Equals(object other)
    {
      if (other == null || GetType() != other.GetType()) return false;

      var otherBWList = (BWList)other;
      var res = true;
      foreach (var a in this)
      {
        if (!otherBWList.Contains(a))
        {
          res = false;
          break;
        }
      }

      return res;
    }

    public override int GetHashCode()
    {
      App[] apps = new App[Count];
      CopyTo(apps);
      Array.Sort(apps);
      
      string x = "";
      foreach (var elem in apps) x += elem.ID + elem.Version;

      return x.GetHashCode();
    }

  }

}