﻿using System.Collections.Generic;
using Newtonsoft.Json;


namespace Shiny
{
    public class Package
    {
        public string Component { get; set; }
        public bool SponsorshipRequired { get; set; }
        public string SaveLocation { get; set; }
        public string? Description { get; set; }
        public string? StaticClass { get; set; }
        public string? Service { get; set; }
        public string? Startup { get; set; }
        public bool CanAutoRegister { get; set; } = true;
        public string? Delegate { get; set; }
        public string? Nuget { get; set; }
        public string[]? Features { get; set; }
        public string[]? SampleLinks { get; set; }
        public SampleInclude[]? SampleIncludes { get; set; }
        public Faq[] Faqs { get; set; }

        public AndroidConfig Android { get; set; }
        public iOSConfig iOS { get; set; }
        public UwpConfig Uwp { get; set; }
    }

    public class SampleInclude
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
    }

    public class Faq
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }

    public class iOSConfig
    {
        public string Info { get; set; }
        public string MinVersion { get; set; } = "10";

        [JsonProperty("infoPlist")]
        public string[]? InfoPlistValues { get; set; }

        [JsonProperty("entitlements")]
        public Dictionary<string, string>? Entitlements { get; set; }
        public string[]? BackgroundModes { get; set; }
        public bool UsesJobs { get; set; } // will show BGTaskSchedulerPermittedIdentifiers in info.plist
        public bool UsesPush { get; set; } // will show entitlements, info.plist uibackgroundmodes, and appdelegate overrides
        public bool UsesBackgroundTransfers { get; set; } // AppDelegate HandleEventsForBackgroundUrl
    }


    public class AndroidConfig
    {
        public string Info { get; set; }
        public string MinVersion { get; set; } = "8.0";
        public string TargetVersion { get; set; } = "11";
        public string[] ManifestUsesPermissions { get; set; }
        public string[] ManifestUsesFeatures { get; set; }
    }


    public class UwpConfig
    {
        public string Info { get; set; }
        public string MinVersion = "17763";
        public string[] Capabilities { get; set; }
        public string[] DeviceCapabilities { get; set; }
        public string[] BackgroundTasks { get; set; }
    }
}
/* UWP Manifest
 //    < Extensions >
  //      < Extension Category = "windows.backgroundTasks" EntryPoint = "Shiny.Support.Uwp.ShinyBackgroundTask" >

  //           < BackgroundTasks >

  //             < Task Type = "bluetooth" />

  //              < Task Type = "deviceConnectionChange" />

  //               < Task Type = "deviceServicing" />

  //                < Task Type = "deviceUse" />

  //                 < Task Type = "general" />

  //                  < Task Type = "location" />

  //                   < Task Type = "systemEvent" />

  //                    < Task Type = "pushNotification" />

  //                     < Task Type = "timer" />

  //                    </ BackgroundTasks >

  //                  </ Extension >

  //                </ Extensions >

  //              </ Application >

  //            </ Applications >

  //            < Capabilities >

  //              < Capability Name = "internetClient" />

  //               < uap3:Capability Name = "backgroundMediaPlayback" />

  //                < uap3:Capability Name = "userNotificationListener" />

  //                 < DeviceCapability Name="bluetooth" />
  //  <DeviceCapability Name="microphone" />
  //  <DeviceCapability Name="location" />
  //  <DeviceCapability Name="proximity"/>
  //</Capabilities>
 */

// iOS - Entitlements
//< key > com.apple.developer.nfc.readersession.formats </ key >

//    < array >

//        < string > NDEF </ string >

//    </ array >

//    < key > aps - environment </ key >

//    < string > development </ string >



// iOS - info.plist
//< key > UIBackgroundModes </ key >
//< array >
//    < string > audio </ string >
//    < string > location </ string >
//    < string > external - accessory </ string >
//    < string > bluetooth - central </ string >
//    < string > bluetooth - peripheral </ string >
//    < string > fetch </ string >
//    < string > remote - notification </ string >
//    < string > processing </ string >
//</ array >
//< key > BGTaskSchedulerPermittedIdentifiers </ key >
//< array >
//    < string > com.shiny.job </ string >
//    < string > com.shiny.jobpower </ string >
//    < string > com.shiny.jobnet </ string >
//    < string > com.shiny.jobpowernet </ string >
//</ array >
//< key > NSSpeechRecognitionUsageDescription </ key >
//< string > Say something useful here</string>



/* Android Manifest
 <?xml version="1.0" encoding="utf-8"?>
<!--
    This code was generated by a tool.
    It was generated from C:\dev\shinylibs\shiny\samples\Samples.Android\Properties\AndroidManifest.xml
    Changes to this file may cause incorrect behavior and will be lost if
    the contents are regenerated.
-->
<manifest xmlns:android="http://schemas.android.com/apk/res/android" xmlns:tools="http://schemas.android.com/tools" package="com.shiny.sample" android:installLocation="preferExternal" android:versionCode="1" android:versionName="1.0">
  <uses-sdk android:minSdkVersion="21" android:targetSdkVersion="29" />
  <uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />
  <uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />
  <uses-permission android:name="android.permission.ACCESS_BACKGROUND_LOCATION" />

  <uses-permission android:name="android.permission.ACCESS_MOCK_LOCATION" />
  <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
  <uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />

  <uses-permission android:name="android.permission.BLUETOOTH_PRIVILEGED" />
  <uses-permission android:name="android.permission.BLUETOOTH_ADMIN" />
  <uses-permission android:name="android.permission.BLUETOOTH" />
  <uses-permission android:name="android.permission.BATTERY_STATS" />
  <uses-permission android:name="android.permission.RECEIVE_BOOT_COMPLETED" />
  <uses-permission android:name="android.permission.RECORD_AUDIO" />
  <uses-permission android:name="android.permission.BIND_MIDI_DEVICE_SERVICE" />
  <uses-permission android:name="android.permission.READ_CALENDAR" />
  <uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
  <uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
  <uses-permission android:name="android.permission.NFC" />
  <uses-permission android:name="android.permission.FOREGROUND_SERVICE" />
  <uses-permission android:name="android.permission.ACCESS_MEDIA_LOCATION" />
  <uses-permission android:name="android.permission.ACTIVITY_RECOGNITION" />
  <uses-permission android:name="android.permission.INTERNET" />
  <uses-feature android:name="android.hardware.bluetooth" />
  <uses-feature android:name="android.hardware.bluetooth_le" />
  <uses-feature android:name="android.hardware.location.gps" />
  <uses-feature android:name="android.hardware.location.network" />
  <uses-feature android:name="android.permission.NFC" />
  <uses-permission android:name="android.permission.WAKE_LOCK" />
  <!-- Required by older versions of Google Play services to create IID tokens -->
  <uses-permission android:name="com.google.android.c2dm.permission.RECEIVE" />
  <application android:name="crc6464984155be56d792.MainApplication" android:allowBackup="true" android:appComponentFactory="androidx.core.app.CoreComponentFactory" android:debuggable="true" android:extractNativeLibs="true" android:icon="@mipmap/icon" android:label="Shiny">
    <!-- <meta-data android:name="com.google.android.maps.v2.API_KEY" android:value="#{GoogleMapsApiKey}#" /> -->
    <uses-library android:name="org.apache.http.legacy" android:required="false" />
    <activity android:name="crc64b5364e0bc1f2d6a9.MainActivity" android:configChanges="orientation|screenSize" android:icon="@mipmap/icon" android:label="Shiny" android:theme="@style/MainTheme">
      <intent-filter>
        <action android:name="android.intent.action.MAIN" />
        <category android:name="android.intent.category.LAUNCHER" />
      </intent-filter>
    </activity>
    <service android:name="crc641134d8fea1f9be25.ShinyBeaconMonitoringService" android:enabled="true" android:exported="true" android:foregroundServiceType="location" />
    <receiver android:name="com.shiny.bluetoothle.ShinyBleAdapterStateBroadcastReceiver" android:enabled="true" android:exported="true">
      <intent-filter>
        <action android:name="android.bluetooth.adapter.action.STATE_CHANGED" />
      </intent-filter>
    </receiver>
    <receiver android:name="com.shiny.bluetoothle.ShinyBleCentralBroadcastReceiver" android:enabled="true" android:exported="true">
      <intent-filter>
        <action android:name="android.bluetooth.device.action.NAME_CHANGED" />
        <action android:name="android.bluetooth.device.action.BOND_STATE_CHANGED" />
        <action android:name="android.bluetooth.device.action.PAIRING_REQUEST" />
      </intent-filter>
    </receiver>
    <receiver android:name="com.shiny.locations.GeofenceBroadcastReceiver" android:enabled="true" android:exported="true">
      <intent-filter>
        <action android:name="com.shiny.locations.GeofenceBroadcastReceiver.INTENT_ACTION" />
        <action android:name="android.permission.RECEIVE_BOOT_COMPLETED" />
      </intent-filter>
    </receiver>
    <receiver android:name="com.shiny.locations.MotionActivityBroadcastReceiver" android:enabled="true" android:exported="true">
      <intent-filter>
        <action android:name="com.shiny.locations.MotionActivityBroadcastReceiver.INTENT_ACTION" />
      </intent-filter>
    </receiver>
    <service android:name="crc6461f3faed59cf0423.ShinyGpsService" android:enabled="true" android:exported="true" android:foregroundServiceType="location" />
    <receiver android:name="com.shiny.net.http.HttpTransferBroadcastReceiver" android:enabled="true" android:exported="true">
      <intent-filter>
        <action android:name="android.intent.action.DOWNLOAD_COMPLETE" />
      </intent-filter>
    </receiver>
    <receiver android:name="com.shiny.notifications.ShinyNotificationBroadcastReceiver" android:enabled="true" android:exported="false" />
    <service android:name="crc6457fe0494698a4c27.ShinyFirebaseService">
      <intent-filter>
        <action android:name="com.google.firebase.MESSAGING_EVENT" />
      </intent-filter>
    </service>
    <service android:name="crc64396a3fe5f8138e3f.KeepAliveService" />
    <receiver android:name="crc64a0e0a82d0db9a07d.BatteryBroadcastReceiver" android:enabled="true" android:exported="false" android:label="Essentials Battery Broadcast Receiver" />
    <receiver android:name="crc64a0e0a82d0db9a07d.EnergySaverBroadcastReceiver" android:enabled="true" android:exported="false" android:label="Essentials Energy Saver Broadcast Receiver" />
    <receiver android:name="crc64a0e0a82d0db9a07d.ConnectivityBroadcastReceiver" android:enabled="true" android:exported="false" android:label="Essentials Connectivity Broadcast Receiver" />
    <activity android:name="crc64a0e0a82d0db9a07d.IntermediateActivity" android:configChanges="orientation|screenSize" />
    <provider android:name="xamarin.essentials.fileProvider" android:authorities="com.shiny.sample.fileProvider" android:exported="false" android:grantUriPermissions="true">
      <meta-data android:name="android.support.FILE_PROVIDER_PATHS" android:resource="@xml/xamarin_essentials_fileprovider_file_paths" />
    </provider>
    <activity android:name="crc64a0e0a82d0db9a07d.WebAuthenticatorIntermediateActivity" android:configChanges="orientation|screenSize" />
    <receiver android:name="crc643f46942d9dd1fff9.PowerSaveModeBroadcastReceiver" android:enabled="true" android:exported="false" />
    <provider android:name="mono.android.MultiDexLoader" android:authorities="com.shiny.sample.mono.android.MultiDexLoader.__mono_init__" android:exported="false" android:initOrder="1999999999" />
    <provider android:name="mono.MonoRuntimeProvider" android:authorities="com.shiny.sample.mono.MonoRuntimeProvider.__mono_init__" android:exported="false" android:initOrder="1999999998" />
    <!-- suppress ExportedReceiver -->
    <receiver android:name="mono.android.Seppuku">
      <intent-filter>
        <action android:name="mono.android.intent.action.SEPPUKU" />
        <category android:name="mono.android.intent.category.SEPPUKU.com.shiny.sample" />
      </intent-filter>
    </receiver>
    <activity android:name="com.google.android.gms.common.api.GoogleApiActivity" android:exported="false" android:theme="@android:style/Theme.Translucent.NoTitleBar" />
    <meta-data android:name="com.google.android.gms.version" android:value="@integer/google_play_services_version" />
    <service android:name="androidx.room.MultiInstanceInvalidationService" android:directBootAware="true" android:exported="false" />
    <provider android:name="androidx.work.impl.WorkManagerInitializer" android:authorities="com.shiny.sample.workmanager-init" android:directBootAware="false" android:exported="false" android:multiprocess="true" tools:targetApi="n" />
    <service android:name="androidx.work.impl.background.systemalarm.SystemAlarmService" android:directBootAware="false" android:enabled="@bool/enable_system_alarm_service_default" android:exported="false" tools:targetApi="n" />
    <service android:name="androidx.work.impl.background.systemjob.SystemJobService" android:directBootAware="false" android:enabled="@bool/enable_system_job_service_default" android:exported="true" android:permission="android.permission.BIND_JOB_SERVICE" tools:targetApi="n" />
    <service android:name="androidx.work.impl.foreground.SystemForegroundService" android:directBootAware="false" android:enabled="@bool/enable_system_foreground_service_default" android:exported="false" tools:targetApi="n" />
    <receiver android:name="androidx.work.impl.utils.ForceStopRunnable$BroadcastReceiver" android:directBootAware="false" android:enabled="true" android:exported="false" tools:targetApi="n" />
    <receiver android:name="androidx.work.impl.background.systemalarm.ConstraintProxy$BatteryChargingProxy" android:directBootAware="false" android:enabled="false" android:exported="false" tools:targetApi="n">
      <intent-filter>
        <action android:name="android.intent.action.ACTION_POWER_CONNECTED" />
        <action android:name="android.intent.action.ACTION_POWER_DISCONNECTED" />
      </intent-filter>
    </receiver>
    <receiver android:name="androidx.work.impl.background.systemalarm.ConstraintProxy$BatteryNotLowProxy" android:directBootAware="false" android:enabled="false" android:exported="false" tools:targetApi="n">
      <intent-filter>
        <action android:name="android.intent.action.BATTERY_OKAY" />
        <action android:name="android.intent.action.BATTERY_LOW" />
      </intent-filter>
    </receiver>
    <receiver android:name="androidx.work.impl.background.systemalarm.ConstraintProxy$StorageNotLowProxy" android:directBootAware="false" android:enabled="false" android:exported="false" tools:targetApi="n">
      <intent-filter>
        <action android:name="android.intent.action.DEVICE_STORAGE_LOW" />
        <action android:name="android.intent.action.DEVICE_STORAGE_OK" />
      </intent-filter>
    </receiver>
    <receiver android:name="androidx.work.impl.background.systemalarm.ConstraintProxy$NetworkStateProxy" android:directBootAware="false" android:enabled="false" android:exported="false" tools:targetApi="n">
      <intent-filter>
        <action android:name="android.net.conn.CONNECTIVITY_CHANGE" />
      </intent-filter>
    </receiver>
    <receiver android:name="androidx.work.impl.background.systemalarm.RescheduleReceiver" android:directBootAware="false" android:enabled="false" android:exported="false" tools:targetApi="n">
      <intent-filter>
        <action android:name="android.intent.action.BOOT_COMPLETED" />
        <action android:name="android.intent.action.TIME_SET" />
        <action android:name="android.intent.action.TIMEZONE_CHANGED" />
      </intent-filter>
    </receiver>
    <receiver android:name="androidx.work.impl.background.systemalarm.ConstraintProxyUpdateReceiver" android:directBootAware="false" android:enabled="@bool/enable_system_alarm_service_default" android:exported="false" tools:targetApi="n">
      <intent-filter>
        <action android:name="androidx.work.impl.background.systemalarm.UpdateProxies" />
      </intent-filter>
    </receiver>
    <receiver android:name="androidx.work.impl.diagnostics.DiagnosticsReceiver" android:directBootAware="false" android:enabled="true" android:exported="true" android:permission="android.permission.DUMP" tools:targetApi="n">
      <intent-filter>
        <action android:name="androidx.work.diagnostics.REQUEST_DIAGNOSTICS" />
      </intent-filter>
    </receiver>
    <provider android:name="androidx.lifecycle.ProcessLifecycleOwnerInitializer" android:authorities="com.shiny.sample.lifecycle-process" android:exported="false" android:multiprocess="true" />
    <provider android:name="com.google.firebase.provider.FirebaseInitProvider" android:authorities="com.shiny.sample.firebaseinitprovider" android:directBootAware="true" android:exported="false" android:initOrder="100" />
    <service android:name="com.google.firebase.components.ComponentDiscoveryService" android:directBootAware="true" android:exported="false" tools:targetApi="n">
      <!--
                This registrar is not defined in the dynamic-module-support sdk itself to allow non-firebase
                clients to use it as well, by defining this registrar in their own core/common library.
            -->
      <meta-data android:name="com.google.firebase.components:com.google.firebase.dynamicloading.DynamicLoadingRegistrar" android:value="com.google.firebase.components.ComponentRegistrar" />
      <meta-data android:name="com.google.firebase.components:com.google.firebase.installations.FirebaseInstallationsRegistrar" android:value="com.google.firebase.components.ComponentRegistrar" />
      <meta-data android:name="com.google.firebase.components:com.google.firebase.iid.Registrar" android:value="com.google.firebase.components.ComponentRegistrar" />
      <meta-data android:name="com.google.firebase.components:com.google.firebase.datatransport.TransportRegistrar" android:value="com.google.firebase.components.ComponentRegistrar" />
      <meta-data android:name="com.google.firebase.components:com.google.firebase.messaging.FirebaseMessagingRegistrar" android:value="com.google.firebase.components.ComponentRegistrar" />
    </service>
    <receiver android:name="com.google.firebase.iid.FirebaseInstanceIdReceiver" android:exported="true" android:permission="com.google.android.c2dm.permission.SEND">
      <intent-filter>
        <action android:name="com.google.android.c2dm.intent.RECEIVE" />
      </intent-filter>
    </receiver>
    <!--
             FirebaseMessagingService performs security checks at runtime,
             but set to not exported to explicitly avoid allowing another app to call it.
        -->
    <service android:name="com.google.firebase.messaging.FirebaseMessagingService" android:directBootAware="true" android:exported="false">
      <intent-filter android:priority="-500">
        <action android:name="com.google.firebase.MESSAGING_EVENT" />
      </intent-filter>
    </service>
    <service android:name="com.google.android.datatransport.runtime.backends.TransportBackendDiscovery" android:exported="false">
      <meta-data android:name="backend:com.google.android.datatransport.cct.CctBackendFactory" android:value="cct" />
    </service>
    <service android:name="com.google.android.datatransport.runtime.scheduling.jobscheduling.JobInfoSchedulerService" android:exported="false" android:permission="android.permission.BIND_JOB_SERVICE"></service>
    <receiver android:name="com.google.android.datatransport.runtime.scheduling.jobscheduling.AlarmManagerSchedulerBroadcastReceiver" android:exported="false" />
  </application>
</manifest>
 */