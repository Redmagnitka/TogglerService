using Xunit;
using Usecases.Interactor;
using System.Collections.Generic;
using System;
using Toggler.Domain;

// NOTE why aren't we seeing events?
// All tests won't raise any event and becayse UCIToggle is instantiated. 
// The CtlToggle class has the responsability to instatiate UCIToggle instantiation
// that will regist itself as an observer of UCIToggle class


namespace Toggler.Tests.Acceptance
{
  public class UCIToggleTests
  {
    [Fact]
    public void TestRegistToggle()
    {
      //Given
      var uc = new UCIToggle();
      var data = new Dictionary<string, Object>();
      data.Add("name", (object)"feature 1");

      //When
      uc.RegistToggle(data);

      //Then
      var current = uc.returnToggle("feature 1");
      var expected = new Toggle("feature 1");
      Assert.Equal(expected, current);
    }

    [Fact]
    public void TestRegistToggleMoreThanOnce()
    {
      //Given
      var uc = new UCIToggle();
      var data = new Dictionary<string, Object>();
      data.Add("name", (object)"feature 1");

      //When
      uc.RegistToggle(data);
      var res = uc.RegistToggle(data);

      //Then
      Assert.Null(res);
    }

    [Fact]
    public void TestRetrieveToggleWhenNone()
    // TODO TestRetrieveToggleWhenNone
    {
      //Given
      var uc = new UCIToggle();

      //When
      var current = uc.returnToggle("feature 1");

      //Then
      Assert.Null(current);
    }

    [Fact]
    public void TestRegistToggleWithoutName()
    // TODO TestRegistToggleWithoutName
    {
      //Given
      var uc = new UCIToggle();

      //When
      var current = uc.returnToggle(null);

      //Then
      Assert.Null(current);
    }

    [Fact]
    public void TestRegistToggleNameIsNull() 
    // TODO TestRegistToggleNameIsNull
    {
      //Given
      var uc = new UCIToggle();

      //When
      var current = uc.returnToggle(null);

      //Then
      Assert.Null(current);
    }

    [Fact]
    public void TestUpdateToggle()
    {
      //Given
      var data = new Dictionary<string, Object>();
      data.Add("name", (object)"feature 1");
      var uct = new UCIToggle();

      // NOTE insert "feature 1" without BL
      uct.RegistToggle(data);

      //When
      Toggle rt1 = uct.returnToggle("feature 1");
      App aA = new App("a", "1");
      App aB = new App("b", "1");
      BWList.ListColor color = BWList.ListColor.BLACK;
      rt1.AddToList(color, aA);
      rt1.AddToList(color, aB);

      // NOTE updated "feature 1" with a BL
      uct.UpdateToggle(rt1, $"Added {aA.ID} and {aB.ID} to toggle {rt1.Name} to {color} list.");


      //Then
      Toggle rt2 = uct.returnToggle("feature 1");
      Assert.Equal(rt1.GetBlackList(), rt2.GetBlackList());
    }

    [Fact]
    public void TestIsAppOnBLForToggle()
    {
      //Given
      var data = new Dictionary<string, Object>();
      data.Add("name", (object)"feature 1");
      var uci_t = new UCIToggle();
      uci_t.RegistToggle(data);

      //When
      Toggle t = uci_t.returnToggle("feature 1");
      App app_a = new App("alpha app", "1");
      App app_b = new App("bravo app", "1");
      BWList.ListColor color = BWList.ListColor.BLACK;
      t.AddToList(color, app_a);
      t.AddToList(color, app_b);
      uci_t.UpdateToggle(t, $"Added {app_a.ID} and {app_b.ID} to toggle {t.Name} to {color} list.");

      //bool current = uci_t.isAppOnBLForToggle(app_a.ID, app_a.version, t);
      bool current = t.isAppOnBL(app_a);

      //Then
      Assert.True(current);
    }

    [Fact]
    public void TestIsAppOnBLForToggleNotSameRef()
    {
      //Given
      var data = new Dictionary<string, Object>();
      data.Add("name", (object)"feature 1");
      var uci_t = new UCIToggle();
      uci_t.RegistToggle(data);

      //When
      Toggle t = uci_t.returnToggle("feature 1");
      App app_a = new App("alpha app", "1");
      BWList.ListColor color = BWList.ListColor.BLACK;
      t.AddToList(color, app_a);
      uci_t.UpdateToggle(t, $"Added {app_a.ID} to toggle {t.Name} to {color} list.");

      //When 
      Toggle t1 = new Toggle("feature 1"); // NOTE this is a new instance
      bool current = t1.isAppOnBL(app_a);

      //Then
      Assert.True(t1.BwCount(BWList.ListColor.BLACK) == 0);
      Assert.False(current);
    }

  }
}