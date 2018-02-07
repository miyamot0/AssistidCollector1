using Android.App;
using Android.Content.PM;
using Android.Content;
using Android.OS;
using Acr.UserDialogs;

namespace AssistidCollector1.Droid
{
    [Activity(Label = "Sleep Data Collector",
        AlwaysRetainTaskState = true,
        Icon = "@drawable/icon",
        ScreenOrientation = ScreenOrientation.SensorPortrait,
        MainLauncher = true,
        Theme = "@style/MainTheme", 
        HardwareAccelerated = true,
        MultiProcess = true,
        ConfigurationChanges = ConfigChanges.Orientation |
                               ConfigChanges.ScreenSize |
                               ConfigChanges.Keyboard |
                               ConfigChanges.KeyboardHidden)]
    [IntentFilter(new[] { Intent.ActionMain },
        Categories = new[]
        {
            Intent.CategoryHome,
            Android.Content.Intent.CategoryDefault
        })]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static Activity MainApplicationActivity;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            UserDialogs.Init(this);

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            MainApplicationActivity = this;

            LoadApplication(new App());
        }
    }
}

