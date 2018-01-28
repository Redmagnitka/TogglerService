using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Toggler.Domain
{
  [DataContract]
  public class App : IComparable<App>
  {
    [DataMember]
    public string ID { get; }
    [DataMember]
    public string Version { get; }
    public string Callback { get; set; }

    public App(string pId, string pVersion)
    {
      ID = pId;
      Version = pVersion;
    }

    public override int GetHashCode()
    {
      return (this.ID + this.Version).GetHashCode();
    }

    public override bool Equals(object other)
    {
      if (other == null || this.GetType() != other.GetType()) return false;

      var otherApp = (App)other;

      var is_equal = false;
      if (ID == otherApp.ID && Version == otherApp.Version)
      {
        is_equal = true;
      }

      return is_equal;
    }

    public static bool operator ==(App left, App right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(App left, App right)
    {
      return !Equals(left, right);
    }

    public static bool operator >(App left, App right)
    {
      return (left.ID.CompareTo(right.ID) > 0);
    }

    public static bool operator <(App left, App right)
    {
      return (left.ID.CompareTo(right.ID) < 0);
    }

    public int CompareTo(App other){
      return ID.CompareTo(other.ID);
    }

  }
}