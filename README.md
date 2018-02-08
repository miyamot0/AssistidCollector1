# AssistidCollector1
AssistidCollector1 is a native extension of work to establish a free and open source application for use with Android, iOS, Windows Mobile, and Blackberry.  AssistidCollector1 is fully supported on all platforms, though only Android and iOS are actively maintained and under evaluation at this point.

Features include:
  - Native views in both iOS and Android
  - Consumes Dropbox API, with support for token-based auth
  - Built-in messaging support, linked to cloud-based storage
  - Administrative mode/Pinning, limiting access to other treatment-unrelated intervention apps
  - Information is saved locally, with cloud-based syncing as available (but not required)

### Version
1.0.0.0

### Changelog
 * 1.0.0.0 - Initial push

### Referenced Works (Packages)
AssistidCollector1 uses a number of open source projects to work properly:
* [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) - MIT Licensed. Copyright (c) 2007 James Newton-King 
* [sqlite-net-pcl](https://github.com/praeclarum/sqlite-net) - MIT Licensed. Copyright (c) 2009-2016 Krueger Systems, Inc.
* [Xamarin Forms](https://github.com/xamarin/Xamarin.Forms) - MIT Licensed. Copyright (c) 2016 Microsoft
* [ConnectivityPlugin](https://github.com/jamesmontemagno/ConnectivityPlugin) - MIT Licensed. Copyright (c) 2018 James Montemagno
* [SettingsPlugin](https://github.com/jamesmontemagno/SettingsPlugin) - MIT Licensed. Copyright (c) 2018 James Montemagno
* [ACR.Dialogs](https://github.com/aritchie/userdialogs) - MIT Licensed. Copyright (c) 2018 Allan Ritchie
* [Xamarin-Forms-In-Anger Card Styles](https://github.com/awolf/Xamarin-Forms-InAnger/tree/master/src/Cards)- MIT Licensed. Copyright (c) 2015 Adam Wolf

### Referenced Works (Images)
* [Late onset Sleep](https://www.123rf.com/photo_47783209_thin-focus-on-hand-of-sleepy-young-asian-boy-using-cellphone-on-white-bed.html?term=boy%2Busing%2Bmoble%2Bphone%2Bbed&vti=of8gubz5v8u7l82bpv-1-5
) - ?? Paid
* [Co-sleeping](https://www.flickr.com/photos/revjim/2310315467/in/photolist-4w9Yii-aXkPt-4MKw56-c4s3C-bFpWHc-hLUqDs-cNsr2-a7uxri-665LR3-4FfN6q-kWm1LX-oePagv-5RR5g1-YWg7U-22aVsk-6TkUsF-4iBkLY-89PHe9-4EuqKr-e5WZaR-F5N5D-9ZJqvp-i55K2u-8E9iop-7a4LHX-hHpr7g-7qagPs-6dQ6ec-bjwwPz-6DVeyw-7GmStt-ccZqX-9ZEzfQ-ip6y8-FGt25-9xmaFN-94YrCv-4ZVjSs-eMJB9-b2gYg2-9kxX1n-agLqkc-2w3e8-e7dcSk-2rQGzC-fJ1yyE-fCtqQL-KVkHd-Ax7nc-scsvG
) - CC-BY-SA NC Licensed. Copyright (c) 2008 Daniel James
* [Bedtime Resistance](http://www.simplymodernmom.com/2010/10/adventures-of-modern-mom-breaking-rules/) - ??
* [Night Awakening](https://www.flickr.com/photos/djvirus/3186012992/) - CC-BY-2.0 Licensed. Copyright (c) 2009 Anton Pinchuk.

### Acknowledgements and Credits
* Bernadette Kirkpatrick, National University of Ireland, Galway
* Brian McGinley, Galway-Mayo Institute of Technology
* Geraldine Leader, National University of Ireland, Galway

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
AssistidCollector1 - Copyright February 2, 2018 Shawn Gilroy, Shawn P. Gilroy. GPL-Version 3
