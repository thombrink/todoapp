﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace Todoo.iOS {
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options) {

            var tint = UIColor.FromRGB(30, 142, 128);
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(242, 197, 0); //bar background
            UINavigationBar.Appearance.TintColor = tint; //Tint color of button items
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes {
                Font = UIFont.FromName("Avenir-Medium", 17f),
                TextColor = UIColor.Black
            });

            UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes {
                Font = UIFont.FromName("Avenir-Medium", 17f),
                ForegroundColor = UIColor.Black
            };

            UIBarButtonItem.Appearance.TintColor = tint; //Tint color of button items


            //NavigationBar Buttons 
            UIBarButtonItem.Appearance.SetTitleTextAttributes(new UITextAttributes {
                Font = UIFont.FromName("Avenir-Medium", 17f),
                TextColor = tint
            },
                UIControlState.Normal);

            //TabBar
            UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes {
                Font = UIFont.FromName("Avenir-Book", 10f)
            }, UIControlState.Normal);

            UITabBar.Appearance.TintColor = tint;

            UISwitch.Appearance.OnTintColor = tint;

            global::Xamarin.Forms.Forms.Init();

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
