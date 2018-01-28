using Xunit;
using Toggler.Control;
using Toggler.Domain;
using Toggler.Control.Exceptions;

namespace Toggler.Tests.Acceptance
{
  public class CtlToggleTests
  {
    [Fact]
    public void TestInsertToggle()
    {
      //Given
      CTLToggle ctl = CTLToggle.Instance;
      string featID = "f1";


      //When
      Toggle expected = null;
      expected = ctl.InsertToggle(featID, "admin_token");

      //Then
      var current = ctl.retrieveToggle(featID, "app_alpha_token", "app_alpha", "1");
      Assert.Equal(expected, current);
    }

    [Fact]
    public void TestRetrieveToggle()
    {
      //Given
      CTLToggle ctl = CTLToggle.Instance;
      string featID = "f2";

      //When
      Toggle toggle = ctl.InsertToggle(featID, "admin_token");
      var current = ctl.retrieveToggle(featID, "app_alpha_token", "app_alpha", "1");

      //Then
      var expected = new Toggle(featID);

      Assert.Equal(expected, current);
    }

    [Fact]
    public void TestUpdateToggleNONAdmin()
    {
      object current = null;

      //Given
      CTLToggle ctl = CTLToggle.Instance;
      var featID = "f3";
      var token = "app_alpha_token";
      ctl.InsertToggle(featID, "admin_token"); // need to be admin to create toggles
      var app_id = "app_alpha";
      var app_version = "1";

      //When
      Toggle toggle = ctl.retrieveToggle(featID, "app_alpha_token", app_id, app_version);

      //Then
      toggle.isON = true;
      current = Assert.Throws<TogglerExceptionNotAdmin>(() => ctl.UpdateToggle(toggle, token,$"Switched toggle {toggle.Name} isON to {toggle.isON}"));
    }

    [Fact]
    public void TestAddAppToBLFor()
    {
      //Given
      var ctl = CTLToggle.Instance;
      string toggle_ID = "f4";
      string app_a_ID = "app_alpha";
      string app_a_version = "1";
      string admin_token = "admin_token";
      App app_a = new App(app_a_ID, app_a_version);

      Toggle toggle = ctl.InsertToggle(toggle_ID, admin_token);

      //When
      ctl.AddAppToToggle(BWList.ListColor.BLACK, toggle_ID, app_a, admin_token);
      Toggle rt = ctl.retrieveToggle(toggle_ID, admin_token, app_a_ID, app_a_version);
      bool current = rt.GetBlackList().Contains(app_a);

      //Then
      Assert.True(current);
    }

    [Fact]
    public void TestUpdateToggleNonAdmin()
    {
      //Given
      CTLToggle ctl = CTLToggle.Instance;
      var featID = "f5";
      ctl.InsertToggle(featID, "admin_token"); // only admins can insert new toggles!
      var app_id = "app_alpha";
      var app_version = "1";
      var app_token = "app_alpha_token";
      var app_alpha = new App(app_id,app_version);

      //When
      Toggle toggle = ctl.retrieveToggle(featID, "app_alpha_token", app_id, app_version);

      //Then
      
      // NOTE app_token cannot create a toggle's blacklist
      Assert.Throws<TogglerExceptionNotAdmin>(() => ctl.AddAppToToggle(BWList.ListColor.BLACK,featID, app_alpha, app_token));

      bool current = toggle.HasBL();
      Assert.False(current);
    }
  }

}