# Trip Tracker

GPS + Motion Activity - Track walks, runs, biking, and drives.  Tracks speed, time taken, total distance, average speed, and the path your user took!

|Area|Value|
|----|-----|
|NuGet|[!NugetShield(Shiny.TripTracker)]|
|Service|Shiny.TripTracker.ITripTrackerManager|
|Auto-Register|YES|
|Delegate|Shiny.TripTracker.ITripTrackerDelegate|
|Static Class|ShinyTripTracker|


## Shiny Startup

```csharp
using Microsoft.Extensions.DependencyInjection;
using Shiny;

namespace YourNamespace
{
    public class YourShinyStartup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
            services.UseTripTracker<MyTripTrackerDelegate>();
        }
    }
}
```


## Features
* Track your runs, walks, biking, and drives
* Tracks average speed, total distance, time taken, and the path your user took

## Samples
* <https://github.com/shinyorg/samples/tree/main/Sponsors-TripTracker>

## iOS

# [Info Plist](#iostab/info)

```xml
<?xml version="1.0" encoding="utf-8" ?>
<!DOCTYPE plist PUBLIC " -//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>NSLocationAlwaysUsageDescription</key>
       <string>Say something useful here that your users will understand</string>
    <key>NSLocationAlwaysAndWhenInUseUsageDescription</key>
       <string>Say something useful here that your users will understand</string>
    <key>NSLocationWhenInUseUsageDescription</key>
       <string>Say something useful here that your users will understand</string>
</dict>
</plist>
```


***


## Android

Minimum Version: 8.0
Target Version: 11
```xml
<?xml version="1.0" encoding="utf-8" ?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android" android:versionCode="1" android:versionName="1.0" package="com.org.yourapp" android:installLocation="preferExternal">
    <uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />
    <uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />
    <uses-permission android:name="android.permission.ACCESS_BACKGROUND_LOCATION" />
    <uses-permission android:name="android.permission.FOREGROUND_SERVICE" />
    <uses-feature android:name="android.hardware.location.gps" />
    <uses-feature android:name="android.hardware.location.network" />
</manifest>
```


