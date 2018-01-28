using System;
using System.Collections.Generic;
using Toggler.Domain;
using Usecases.Repositories;
using Xunit;

namespace Toggler.Tests.Unit
{
    public class RepositoryTests
  {

    [Fact]
    public void TestPutReturn()
    {
      Object current;
      Object expected;

      //Given
      var repo = new ToggleRepositoryInMemory();
      var t_a = new Toggle("alpha");

      //When
      current = repo.put(t_a);

      //Then
      expected = t_a;
      Assert.Equal(expected, current);
    }

    [Fact]
    public void TestPutAddingMoreThanOnceAndCardinality()
    {
      //Given
      var repo = new ToggleRepositoryInMemory();
      var t_a = new Toggle("alpha");

      //When
      repo.put(t_a);
      repo.put(t_a); 

      //Then
      Assert.Null(repo.put(t_a));
      Assert.True(1 == repo.cardinality()); // remains 1
    }

    [Fact]
    public void TestGetByName()
    {
      //Given
      var repo = new ToggleRepositoryInMemory();
      var t_a = new Toggle("alpha");

      //When
      repo.put(t_a);

      //Then
      var expected = t_a;
      var current = repo.getByName("alpha");

      Assert.Equal(expected, current);
    }

    [Fact]
    public void TestGetTooglesForAppWhenAppIsOnBL()
    {
      //Given
      var app1 = new App("app1", "v1");
      var tog1 = new Toggle("feature-alpha");
      var repo = new ToggleRepositoryInMemory();

      //When
      tog1.AddToList(BWList.ListColor.BLACK, app1);
      repo.put(tog1);

      var app1_toggles = repo.getTogglesForApp(app1);

      //Then
      var current = app1_toggles.Count;
      var expected = 0;

      Assert.True(current == expected);
    }

    [Fact]
    public void TestGetTooglesForAppWhenAppIsOnWL()
    {
      //Given
      var app1 = new App("app1", "v1");
      var tog1 = new Toggle("feature-alpha");
      var repo = new ToggleRepositoryInMemory();

      //When
      tog1.AddToList(BWList.ListColor.WHITE, app1);
      repo.put(tog1);

      var app1_toggles = repo.getTogglesForApp(app1);

      //Then
      var current = app1_toggles.Count;
      var expected = 1;

      Assert.True(current == expected);
    }

    [Fact]
    public void TestGetTooglesForAppWhenAppIsBlackListedOnAllToogles()
    {
      //Given
      var app1 = new App("app1", "v1");
      var tog1 = new Toggle("feature-alpha");
      var tog2 = new Toggle("feature-bravo");
      var repo = new ToggleRepositoryInMemory();

      //When
      tog1.AddToList(BWList.ListColor.BLACK, app1);
      repo.put(tog1);

      tog2.AddToList(BWList.ListColor.BLACK, app1);
      repo.put(tog2);

      var app1_toggles = repo.getTogglesForApp(app1);

      //Then
      var current = app1_toggles.Count;
      var expected = 0;

      Assert.True(current == expected);
    }

    [Fact]
    public void TestGetTooglesForAppWhenTogglesWithNoWLNorBL()
    {
      //Given
      var app1 = new App("app1", "v1");
      var tog1 = new Toggle("feature-alpha");
      var tog2 = new Toggle("feature-bravo");
      var repo = new ToggleRepositoryInMemory();

      //When
      repo.put(tog1); // NOTE no BL nor WL
      repo.put(tog2); // all apps can use these toggles

      var app1_toggles = repo.getTogglesForApp(app1);

      //Then
      var current = app1_toggles.Count;
      var expected = 2;

      Assert.True(current == expected);
    }

    [Fact]
    public void TestGetTooglesForAppWhenMultipleApps()
    {
      //Given
      var app1 = new App("app1", "v1");
      var app2 = new App("app2", "v1");
      var app3 = new App("app3", "v1");
      var tog1 = new Toggle("feature-alpha");
      var tog2 = new Toggle("feature-bravo");
      var repo = new ToggleRepositoryInMemory();

      //When
      repo.put(tog1); // NOTE no BL nor WL
      repo.put(tog2); // all apps can use these toggles

      //Then
      var dicApps = new Dictionary<int, App>();
      dicApps.Add(1, app1);
      dicApps.Add(2, app2);
      dicApps.Add(3, app3);

      foreach (KeyValuePair<int, App> pair in dicApps)
      {
        var current = repo.getTogglesForApp(pair.Value).Count;
        var expected = 2;
        Assert.True(current == expected);
      }
    }

    [Fact]
    public void TestRemove()
    {
      Object expected;
      Object current;

      //Given
      var tog1 = new Toggle("feature-alpha");
      var repo = new ToggleRepositoryInMemory();

      //When
      repo.put(tog1);

      //Then
      expected = 1;
      current = repo.cardinality();
      Assert.Equal(expected, current);

      //When
      repo.remove(tog1);

      //Then
      expected = 0;
      current = repo.cardinality();
      Assert.Equal(expected, current);

      //When
      repo.put(tog1);

      //Then
      expected = tog1;
      current = repo.remove(tog1);
      Assert.Equal(expected, current);
    }

    [Fact]
    public void TestUpdateIsON()
    {
      //Given
      Toggle tog1 = new Toggle("feature-alpha");
      ToggleRepositoryInMemory repo = new ToggleRepositoryInMemory();

      //When
      var toggle = repo.put(tog1);

      //Then
      Assert.False(toggle.isON);

      //When
      toggle.isON = true;
      toggle = repo.update(toggle);

      //Then
      Assert.True(toggle.isON);
    }

    [Fact]
    public void TestUpdateBL()
    {
      //Given
      Toggle tog1 = new Toggle("feature-alpha");
      App app1 = new App("alpha", "1");
      ToggleRepositoryInMemory repo = new ToggleRepositoryInMemory();

      //When
      tog1.AddToList(BWList.ListColor.BLACK, app1);
      var toggle = repo.put(tog1);

      //Then
      Assert.True(toggle.GetBlackList().Count == 1);

      //When
      tog1.RemoveAllFromBL();
      toggle = repo.update(tog1);

      //Then
      Assert.True(toggle.GetBlackList().Count == 0);
    }

    [Fact]
    public void TestUpdateWL()
    {
      //Given
      ToggleRepositoryInMemory repo = new ToggleRepositoryInMemory();

      Toggle tog1 = new Toggle("feature-alpha");
      App app1 = new App("alpha", "1");
      tog1.AddToList(BWList.ListColor.WHITE, app1);

      Toggle tog2 = new Toggle("feature-zulu");
      App app2 = new App("x-ray", "1");
      tog2.AddToList(BWList.ListColor.WHITE, app2);

      //When
      var toggle = repo.put(tog1);
      toggle.RewriteBW(BWList.ListColor.WHITE, tog2.GetWhiteList());
      Toggle current = repo.update(toggle);

      //Then
      Assert.Equal(tog2.GetWhiteList(), current.GetWhiteList());
    }
  }
}