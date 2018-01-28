using Toggler.Domain;

namespace Usecases.Interfaces
{
  interface IToggleRepository
  {
    Toggle getByName(string s);
    Toggle put(Toggle t);
    Toggle update(Toggle toggle);
    Toggle remove(Toggle togglet);

    int cardinality();
  }
}