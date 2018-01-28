using System;
using Toggler.Domain;
using System.Collections.Generic;

namespace Usecases.Factories
{
  public class ToggleFactory
  {
   
    public Toggle CreateToggle(Dictionary<string, Object> data)
    {

        var rToggle = new Toggle((string)data["name"]);

      return rToggle;
    }
  }
}