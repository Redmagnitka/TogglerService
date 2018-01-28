using Toggler.Domain;
using System;
using System.Collections.Generic;
using Usecases.Interfaces;

namespace Usecases.Repositories
{

  public class ToggleRepositoryInMemory : IToggleRepository
  {
    private List<Toggle> toggles; 

    public ToggleRepositoryInMemory()
    {
      toggles = new List<Toggle>();
    }

    public Toggle put(Toggle toggle)
    {
      // validation to check if toggle already exists
      if (toggles.Exists(t => t.Equals(toggle))) {
        Console.WriteLine($"The Toggle: [{toggle.Name}] already exists!");
        return null;
      }

      toggles.Add(toggle);

      return toggle;
    }

    // NOTE to be used only by admin, because 
    // a client will need to do getTooglsForApp
    // and then filter the desired toggle.
    public Toggle getByName(string name)
    {
      var input = new Toggle(name);

      var res = toggles.Find(t => t.Equals(input));

      return res;
    }

    public int cardinality()
    {
      return toggles.Count;
    }

    public List<Toggle> getTogglesForApp(App app)
    {
      var res = new List<Toggle>();

      foreach (var t in toggles)
      {

        if (t.HasBL() && t.isAppOnBL(app))
        {
          // NOTE next
          continue;
        }

        if (!t.HasWL())
        {
          // NOTE all apps can use
          res.Add(t);
          continue;
        }

        // NOTE can be WL or is not BL
        res.Add(t);
      }

      return res;
    }

    public Toggle remove(Toggle other)
    {
      Toggle res = null;

      if (!this.toggles.Exists(x => x.Equals(other))) return null;

      var t = this.getByName(other.Name);
      
      res = this.toggles.Remove(t) ? other : null;

      return res;
    }

    public Toggle update(Toggle other)
    {
      Toggle res = null;

      if (!toggles.Exists(x => x.Equals(other))) return res;

      var t = getByName(other.Name);

      Toggle t_removed = remove(t);

      t_removed.isON = other.isON;

      t_removed.RewriteBW(BWList.ListColor.BLACK, other.GetBlackList());
      t_removed.RewriteBW(BWList.ListColor.WHITE, other.GetWhiteList());

      res = put(t_removed);

      return res;
    }

  }
}