using Toggler.Domain;
using System.Collections.Generic;
using Xunit;
using System;

namespace Toggler.Tests.Unit
{
  public class AppTests
  {
    [Fact]
    public void TestAppVersionIsSet()
    {
      //Given
      var app_a = new App("app1", "v1");

      //When
   
      //Then
      var expected = "v1";
      var current = app_a.Version;

      Assert.Equal(current, expected);
    }

    [Fact]
    public void TestEqualitySuccess()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app1", "v1");

      //When
      // no-op

      //Then
      var current = app_a.Equals(app_b);
      Assert.True(current);
    }

    [Fact]
    public void TestEqualitySucessUsingSetLength()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app1", "v1"); // NOTE is equal
      var y = new HashSet<App>(new App[] { }); // NOTE empty set

      //When
      y.Add(app_a);
      y.Add(app_a);
      y.Add(app_b);
      y.Add(app_b);

      //Then
      var expected = 1;
      var current = y.Count;
      Assert.True(current == expected);
    }

    [Fact]
    public void TestEqualitySucessUsingSetContains()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app1", "v1");
      var y = new HashSet<App>();
      //When
      y.Add(app_a);
      // NOTE that app_b instance is not added on purpose

      //Then
      var current = y.Contains(app_b); // NOTE testing against app_b
      Assert.True(current);
    }

    [Fact]
    public void TestEqualityFailureDiffObject()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new Toggle("not even the same type");
      
      //When
      // no-op

      //Then
      Assert.False(app_a.Equals(app_b));
    }

    [Fact]
    public void TestGreatherThanOp()
    {
      //Given
      var app_a = new App("alpha", "v1");
      var app_z = new App("zulu", "v1");

      //When
      //var current = "zulu".CompareTo("alpha");
      var current = app_z > app_a;

      //Then
      Assert.True(current);
    }

    [Fact]
    public void TestArraySort()
    {
      //Given
      var app_a = new App("alpha", "v1");
      var app_z = new App("zulu", "v1");
      var app_c = new App("charlie", "v1");
      var app_b = new App("bravo", "v1");
      App[] apps = new App[4];
      apps[0] = app_a;
      apps[1] = app_z;
      apps[2] = app_c;
      apps[3] = app_b;

      //When
      Array.Sort(apps);

      //Then
      Assert.Equal(app_a, apps[0]);
      Assert.Equal(app_b, apps[1]);
      Assert.Equal(app_c, apps[2]);
      Assert.Equal(app_z, apps[3]);
    }

    [Fact]
    public void TestLessThanOp()
    {
      //Given
      var app_a = new App("alpha", "v1");
      var app_z = new App("zulu", "v1");

      //When
      //var current = "zulu".CompareTo("alpha");
      var current = app_a < app_z;

      //Then
      Assert.True(current);
    }

    [Fact]
    public void TestEqualityFailureDiffVersion()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app1", "v2"); // diff version

      //When
      // no-op

      //Then
      var current = app_a.Equals(app_b);
      Assert.False(current);
    }

    [Fact]
    public void TestEqualityFailureDiffName()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v1"); // diff name

      //When
      // no-op

      //Then
      var current = app_a.Equals(app_b);
      Assert.False(current);
    }
  }
}
