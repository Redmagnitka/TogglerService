using System;
using Toggler.Domain;
using System.Collections.Generic;
using Usecases.Interfaces;
using Usecases.Repositories;
using Usecases.Factories;

namespace Usecases.Interactor
{
  public class UCIToggle : IEventEmiter
  {

    public List<IEventReceiver> observers = new List<IEventReceiver>();

    private IToggleRepository repoToggles;
    private ToggleFactory factToggles;

    public UCIToggle()
    {
            // NOTE change to another implementation, 
            repoToggles = new ToggleRepositoryInMemory();

            factToggles = new ToggleFactory();
    }

    public Toggle RegistToggle(Dictionary<string, Object> data)
    {
      var toggle = this.factToggles.CreateToggle(data);

      Toggle res = this.repoToggles.put(toggle);

      string msg = res is null ? "Toggle not created!" : $"Created the toggle: [{res.Name}]";
            NotifyObservers(msg, toggle);

      return res;
    }

    public Toggle returnToggle(string toggleName)
    {
      Toggle rToggle = this.repoToggles.getByName(toggleName);

      return rToggle;
    }

    public Toggle UpdateToggle(Toggle otherToggle, string msg)
    {
      // serialization
      if (otherToggle.GetType().GetProperty("name").GetValue(otherToggle) == null) return null;

      Toggle toggle = this.repoToggles.getByName(otherToggle.Name);

      if (toggle is null) return null;

      toggle.isON = otherToggle.isON;

            NotifyObservers(msg, toggle);

      return toggle;
    }

    public void NotifyObservers(string msg, Toggle pToggle)
    {
      foreach (var obs in this.observers)
      {
        obs.roger(msg, pToggle);
      }
    }
  }

}