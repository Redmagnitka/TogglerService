using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Toggler.Domain
{
  [DataContract]
  public class Toggle : IComparable<Toggle>
  {
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public bool isON { get; set; }
    [DataMember]
    private BWList whiteList;
    [DataMember]
    private BWList blackList;
    public Toggle(string name)
    {
      whiteList = new BWList(BWList.ListColor.WHITE);
      blackList = new BWList(BWList.ListColor.BLACK);

      isON = false;
      Name = name;
    }

    public override int GetHashCode()
    {
      return (Name).GetHashCode();
    }

    public override bool Equals(object other)
    {
      var res = false;

      if (other == null || this.GetType() != other.GetType()) return res;

      var otherToggle = (Toggle)other;

      if (Name == otherToggle.Name) res = true;

      return res;
    }

    public static bool operator ==(Toggle left, Toggle right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Toggle left, Toggle right)
    {
      return !Equals(left, right);
    }

    public static bool operator >(Toggle left, Toggle right)
    {
      return (left.Name.CompareTo(right.Name) > 0);
    }

    public static bool operator <(Toggle left, Toggle right)
    {
      return (left.Name.CompareTo(right.Name) < 0);
    }

    public int CompareTo(Toggle other)
    {
      return Name.CompareTo(other.Name);
    }

    public BWList GetBlackList()
    {
      return blackList;
    }

    public BWList GetWhiteList()
    {
      return whiteList;
    }

    public bool isAppOnBL(App app)
    {
      return GetBlackList().Contains(app) ? true : false;
    }

    public bool isAppOnWL(App app)
    {
      return GetWhiteList().Contains(app) ? true : false;
    }

    public bool HasBL()
    {
      return GetBlackList().Count > 0 ? true : false;
    }

    public bool HasWL()
    {
      return GetWhiteList().Count > 0 ? true : false;
    }

    public bool AddToList(BWList.ListColor color, App app)
    {
      bool res = false;

      if (color == BWList.ListColor.BLACK)
      {
        
        if (HasWL()) return res;
        res = GetBlackList().AddToList(app);
      }
      else if (color == BWList.ListColor.WHITE)
      {
        if (HasBL()) return res;
        res = GetWhiteList().AddToList(app);
      }

      return res;
    }

    public int BwCount(BWList.ListColor color)
    {
      int res = 0;

      if (color == BWList.ListColor.BLACK) res = GetBlackList().Count;
      if (color == BWList.ListColor.WHITE) res = GetWhiteList().Count;

      return res;
    }

    public bool RemoveFromBW(BWList.ListColor color, App app)
    {
      bool res = false;

      if (color == BWList.ListColor.BLACK)
      {
        if (!isAppOnBL(app)) return res;
        res = GetBlackList().Remove(app);
      }
      else if (color == BWList.ListColor.WHITE)
      {
        if (!isAppOnWL(app)) return res;
        res = GetWhiteList().Remove(app);
      }

      return res;
    }

    public void RemoveAllFromWL()
    {
      this.GetWhiteList().Clear();
    }

    public void RemoveAllFromBL()
    {
      this.GetBlackList().Clear();
    }

    public void RewriteBW(BWList.ListColor color, HashSet<App> otherBWL)
    {
      if (color == BWList.ListColor.BLACK)
      {
  
        HashSet<App> otherBLCopy = new HashSet<App>();
        foreach (var app in otherBWL)
        {
          otherBLCopy.Add(app);
        }

        RemoveAllFromBL();

        foreach (var app in otherBLCopy)
        {
          GetBlackList().Add(app);
        }

      }
      else if (color == BWList.ListColor.WHITE)
      {
        HashSet<App> otherWLCopy = new HashSet<App>();
        foreach (var app in otherBWL)
        {
          otherWLCopy.Add(app);
        }

        RemoveAllFromWL();

        foreach (var app in otherWLCopy)
        {
          GetWhiteList().Add(app);
        }

      }

    }

  }
}