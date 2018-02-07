# AssistidCollector1
AssistidCollector1 is a native extension of work to establish a free and open source application for use with Android, iOS, Windows Mobile, and Blackberry.  AssistidCollector1 is fully supported on all platforms, though only Android and iOS are actively maintained and under evaluation at this point.

Features include:
  - Native views in both iOS and Android
  - Use as home screen, limit access to non-intervention apps
  - Save all information automatically, with cloud-based syncing

### Version
1.0.0.0

### Changelog
 * 1.0.0.0 - Initial push

### Referenced Works (Packages)
AssistidCollector1 uses a number of open source projects to work properly:
* [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) - MIT Licensed. Copyright (c) 2007 James Newton-King 
* [sqlite-net-pcl](https://github.com/praeclarum/sqlite-net) - MIT Licensed. Copyright (c) 2009-2016 Krueger Systems, Inc.
* [Xamarin Forms](https://github.com/xamarin/Xamarin.Forms) - MIT Licensed. Copyright (c) 2016 Microsoft

add in cross connectivity
add in card views

### Acknowledgements and Credits
* Geraldine Leader, National University of Ireland, Galway

add in bernie and brian

### Installation
AssistidCollector1 can be installed as either an Android or iOS application.  

### Device Owner Mode (Android)
AssistidCollector1 can be set to be a dedicated, intervention-only device by having the administrator run the following command from ADB:

<i>adb shell dpm set-device-owner com.smallnstats.AssistidCollector1/com.smallnstats.AssistidCollector1.Base.DeviceAdminReceiverClass</i>

Optionally, administators can disable the user warnings displayed on the screen by running the following command from ADB:

<i>adb shell appops set android TOAST_WINDOW deny</i>

Issuing this demand will perform indefinite screen pinning, much as single-use devices (e.g., inventory counters, touch screen cash registers) function.

### Download
Compiled binaries are not provided at this time.

### Development
This is currently under active development and evaluation.

### License
----
AssistidCollector1 - Copyright July 2, 2018 Shawn Gilroy, Shawn P. Gilroy. GPL-Version 3
