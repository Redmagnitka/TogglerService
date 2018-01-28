using Toggler.Domain;
using System.Collections.Generic;
using Xunit;
using System;

namespace Toggler.Tests.Unit
{
  public class ToggleTests
  {
    [Fact]
    public void TestAddOneAppToBlackList()
    {
      //Given
      var app_a = new App("app1", "v1");
      var sut = new Toggle("feature-alpha");
      //When
      sut.AddToList(BWList.ListColor.BLACK, app_a);
      //Then
      var current = sut.isAppOnBL(app_a);
      Assert.True(current);
    }

    [Fact]
    public void TestAddTwoAppToBlackList()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v2");
      var sut = new Toggle("feature-alpha");
      //When
      sut.AddToList(BWList.ListColor.BLACK, app_a);
      sut.AddToList(BWList.ListColor.BLACK, app_b);
      //Then
      var current = sut.isAppOnBL(app_a);
      Assert.True(current);
      current = sut.isAppOnBL(app_b);
      Assert.True(current);
    }

    [Fact]
    public void TestToggleNameIsSet()
    {
      //Given
      var sut = new Toggle("feature-alpha");
      //When
      // no-op
      //Then
      var expected = "feature-alpha";
      var current = sut.Name;
      Assert.Equal(current, expected);
    }

    [Fact]
    public void TestIsONIsFalse()
    {
      //Given
      var sut = new Toggle("feature-alpha");
      //When
      // no-op
      //Then
      var expected = false;
      var current = sut.isON;
      Assert.Equal(current, expected);
    }

    [Fact]
    public void TestEqualitySuccess()
    {
      //Given
      var toggle_a = new Toggle("feature-1");
      var toogle_b = new Toggle("feature-1");

      //When
      // no-op

      //Then
      var current = toggle_a.Equals(toogle_b);
      Assert.True(current);
    }

    [Fact]
    public void TestEqualityFailure()
    {
      //Given
      var toggle_a = new Toggle("feature-1");
      var toggle_b = new Toggle("feature-2");

      //When
      // no-op

      //Then
      var current = toggle_a.Equals(toggle_b);
      Assert.False(current);
    }


    [Fact]
    public void TestIsAppOnBLNotSameRef()
    {
      //Given
      App app_a = new App("app1", "v1");
      Toggle sut = new Toggle("feature-alpha");

      //When
      sut.AddToList(BWList.ListColor.BLACK, app_a);

      //different ref but same ID and version
      App similar_app = new App("app1", "v1");

      //Then
      Assert.True(sut.isAppOnBL(similar_app));

      //When
      App other_ref_but_diff_version = new App("app1", "version z");

      //Then
      Assert.False(sut.isAppOnBL(other_ref_but_diff_version));
    }

    [Fact]
    public void TestIsAppOnBL()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v2");
      var sut = new Toggle("feature-alpha");

      //When
      sut.AddToList(BWList.ListColor.BLACK, app_a);

      //Then
      Assert.True(sut.isAppOnBL(app_a));
      Assert.False(sut.isAppOnWL(app_a));

      Assert.False(sut.isAppOnBL(app_b));
      Assert.False(sut.isAppOnWL(app_b));
    }

    [Fact]
    public void TestIsAppOnWL()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v2");
      var sut = new Toggle("feature-alpha");

      //When
      sut.AddToList(BWList.ListColor.WHITE, app_a);

      //Then
      Assert.True(sut.isAppOnWL(app_a));
      Assert.False(sut.isAppOnBL(app_a));

      Assert.False(sut.isAppOnWL(app_b));
      Assert.False(sut.isAppOnBL(app_b));
    }

    [Fact]
    public void TestHasBLTrue()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v2");
      var sut = new Toggle("feature-alpha");

      //When
      sut.AddToList(BWList.ListColor.BLACK, app_a);

      //Then
      Assert.True(sut.HasBL());
      Assert.False(sut.HasWL());
    }

    [Fact]
    public void TestHasWLTrue()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v2");
      var sut = new Toggle("feature-alpha");

      //When
      sut.AddToList(BWList.ListColor.WHITE, app_a);

      //Then
      Assert.True(sut.HasWL());
      Assert.False(sut.HasBL());
    }

    [Fact]
    public void TestAddToBL()
    {
      //Given
      var app_a = new App("app1", "v1");
      var sut = new Toggle("feature-alpha");

      //When
      sut.AddToList(BWList.ListColor.BLACK, app_a);

      //Then
      Assert.True(sut.HasBL());
      Assert.True(sut.isAppOnBL(app_a));
    }

    [Fact]
    public void TestAddToBLWhenHasWL()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v1");
      var sut = new Toggle("feature-alpha");
      var x = sut.AddToList(BWList.ListColor.WHITE, app_a);

      //When
      var current = sut.AddToList(BWList.ListColor.BLACK, app_b);

      //Then
      Assert.False(current);
    }

    [Fact]
    public void TestAddToWLWhenHasBL()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v1");
      var sut = new Toggle("feature-alpha");
      var x = sut.AddToList(BWList.ListColor.BLACK, app_a);

      //When
      var current = sut.AddToList(BWList.ListColor.WHITE, app_b);

      //Then
      Assert.False(current);
    }

    [Fact]
    public void TestBLCount()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v1");
      var sut = new Toggle("feature-alpha");
      sut.AddToList(BWList.ListColor.BLACK, app_a);
      sut.AddToList(BWList.ListColor.BLACK, app_b);

      //When
      var expected = 2;
      var current = sut.BwCount(BWList.ListColor.BLACK);

      //Then
      Assert.Equal(expected, current);
    }

    [Fact]
    public void TestRemoveFromBL()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v1");
      var sut = new Toggle("feature-alpha");
      sut.AddToList(BWList.ListColor.BLACK, app_a);
      sut.AddToList(BWList.ListColor.BLACK, app_b);

      //When
      sut.RemoveFromBW(BWList.ListColor.BLACK, app_a);
      var current = sut.BwCount(BWList.ListColor.BLACK);

      //Then
      var expected = 1;
      Assert.Equal(expected, current);
    }

    [Fact]
    public void TestRemoveAllFromBL()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v1");
      var sut = new Toggle("feature-alpha");
      sut.AddToList(BWList.ListColor.BLACK, app_a);
      sut.AddToList(BWList.ListColor.BLACK, app_b);

      //When
      sut.RemoveAllFromBL();
      var current = sut.BwCount(BWList.ListColor.BLACK);

      //Then
      var expected = 0;
      Assert.Equal(expected, current);
    }

    [Fact]
    public void TestRewriteBL()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v1");
      var sut = new Toggle("feature-alpha");
      sut.AddToList(BWList.ListColor.BLACK, app_a);
      sut.AddToList(BWList.ListColor.BLACK, app_b);

      //When
      var app_x = new App("appX", "v1");
      var app_y = new App("appY", "v1");
      var app_z = new App("appZ", "v1");
      var otherBL = new HashSet<App>();
      otherBL.Add(app_x);
      otherBL.Add(app_y);
      otherBL.Add(app_z);

      sut.RewriteBW(BWList.ListColor.BLACK, otherBL);

      Assert.True(sut.GetBlackList().Count > 0);
      Assert.Equal(otherBL, sut.GetBlackList());
    }

    [Fact]
    public void TestRewriteWL()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v1");
      var sut = new Toggle("feature-alpha");
      sut.AddToList(BWList.ListColor.WHITE, app_a);
      sut.AddToList(BWList.ListColor.WHITE, app_b);

      //When
      var app_x = new App("appX", "v1");
      var app_y = new App("appY", "v1");
      var app_z = new App("appZ", "v1");
      var otherWL = new HashSet<App>();
      otherWL.Add(app_x);
      otherWL.Add(app_y);
      otherWL.Add(app_z);

      sut.RewriteBW(BWList.ListColor.WHITE, otherWL);

      Assert.True(sut.GetWhiteList().Count > 0);
      Assert.Equal(otherWL, sut.GetWhiteList());
    }

    [Fact]
    public void TestRewriteBLSameREF()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v1");
      var sut = new Toggle("feature-alpha");
      sut.AddToList(BWList.ListColor.BLACK, app_a);
      sut.AddToList(BWList.ListColor.BLACK, app_b);

      //When
      var sameBlackList = sut.GetBlackList();
      sut.RewriteBW(BWList.ListColor.BLACK, sameBlackList);

      //Then
      Assert.True(sut.GetBlackList().Count > 0);
      Assert.Equal(sameBlackList, sut.GetBlackList());
    }
    
    [Fact]
    public void TestRewriteWLSameREF()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v1");
      var sut = new Toggle("feature-alpha");
      sut.AddToList(BWList.ListColor.WHITE, app_a);
      sut.AddToList(BWList.ListColor.WHITE, app_b);

      //When
      var sameWhiteList = sut.GetWhiteList();
      sut.RewriteBW(BWList.ListColor.WHITE, sameWhiteList);

      //Then
      Assert.True(sut.GetWhiteList().Count > 0);
      Assert.Equal(sameWhiteList, sut.GetWhiteList());
    }

    [Fact]
    public void TestRewriteBLSimilarList()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v1");
      var sut = new Toggle("feature-alpha");
      sut.AddToList(BWList.ListColor.BLACK, app_a);
      sut.AddToList(BWList.ListColor.BLACK, app_b);

      //When
      var otherBL = new HashSet<App>();
      otherBL.Add(app_a);
      otherBL.Add(app_b);

      //different list
      sut.RewriteBW(BWList.ListColor.BLACK, otherBL);

      Assert.True(sut.GetBlackList().Count > 0);
      Assert.Equal(otherBL, sut.GetBlackList());
    }

    [Fact]
    public void TestRewriteWLSimilarList()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v1");
      var sut = new Toggle("feature-alpha");
      sut.AddToList(BWList.ListColor.WHITE, app_a);
      sut.AddToList(BWList.ListColor.WHITE, app_b);

      //When
      var otherWL = new HashSet<App>();
      otherWL.Add(app_a);
      otherWL.Add(app_b);

      //different list
      sut.RewriteBW(BWList.ListColor.WHITE, otherWL);

      Assert.True(sut.GetWhiteList().Count > 0);
      Assert.Equal(otherWL, sut.GetWhiteList());
    }

    [Fact]
    public void TestBlackListEqualityFails()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v1");
      var app_x = new App("appX", "1");
      var app_y = new App("appY", "1");
      var tog1 = new Toggle("feature-alpha");
      var tog2 = new Toggle("feature-bravo");

      //When
      tog1.AddToList(BWList.ListColor.BLACK, app_a);
      tog1.AddToList(BWList.ListColor.BLACK, app_b);
      tog2.AddToList(BWList.ListColor.BLACK, app_x);
      tog2.AddToList(BWList.ListColor.BLACK, app_y);

      //Then
      bool current = tog1.GetBlackList().Equals(tog2);
      Assert.False(current);
    }

    [Fact]
    public void TestBLEqualitySuccess()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v1"); ;
      var tog1 = new Toggle("feature-alpha");
      var tog2 = new Toggle("feature-bravo");

      //When
      tog1.AddToList(BWList.ListColor.BLACK, app_a);
      tog1.AddToList(BWList.ListColor.BLACK, app_b);

      tog2.AddToList(BWList.ListColor.BLACK, app_a);
      tog2.AddToList(BWList.ListColor.BLACK, app_b);

      //Then
      bool current = tog1.GetBlackList().Equals(tog2.GetBlackList());
      Assert.True(current);
    }

    [Fact]
    public void TestWLEqualitySuccess()
    {
      //Given
      var app_a = new App("app1", "v1");
      var app_b = new App("app2", "v1"); ;
      var tog1 = new Toggle("feature-alpha");
      var tog2 = new Toggle("feature-bravo");

      //When
      tog1.AddToList(BWList.ListColor.WHITE, app_a);
      tog1.AddToList(BWList.ListColor.WHITE, app_b);

      tog2.AddToList(BWList.ListColor.WHITE, app_a);
      tog2.AddToList(BWList.ListColor.WHITE, app_b);

      //Then
      bool current = tog1.GetWhiteList().Equals(tog2.GetWhiteList());
      Assert.True(current);
    }

  }
}