using AssistidCollector1.Interfaces;
using AssistidCollector1.Droid.Implementations;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImplementationContentLocation))]
namespace AssistidCollector1.Droid.Implementations
{
    public class ImplementationContentLocation : InterfaceContentLocation
    {
        public string GetBaseLocation()
        {
            return "file:///android_asset/";
        }

        public string GetPersonalLocation()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        }
    }
}