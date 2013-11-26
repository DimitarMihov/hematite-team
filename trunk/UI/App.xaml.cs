﻿using GarageManagementSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace UI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            // TODO: Create methods to load cars, employees, distributors etc from a DB or a file
            Service.AutoShopInstance.AddVehicle(new Vehicle("Peugeot", "106", 1999, "CA 3919 LN"));
            Service.AutoShopInstance.AddVehicle(new Vehicle("BMW", "5", 2002, "PA 8750 HA"));
            Service.AutoShopInstance.AddVehicle(new Vehicle("Mercedes", "SLK", 2010, "A 8993 MM"));

            Service.AutoShopInstance.AddDistributor(new Distributor("MotoPfohe Ltd", "089 901 5632", "order@motopfohe.bg"));
            Service.AutoShopInstance.AddDistributor(new Distributor("BetterCars Ltd", "089 598 8915", "order_parts@bettercars.bg"));
            Service.AutoShopInstance.AddDistributor(new Distributor("PartsForPeople Ltd", "088 991 5987", "office@peopleparts.bg"));

            Service.AutoShopInstance.AddEmployee(new Employee("Marin Ivanov", 500, "088 909 5135"));
            Service.AutoShopInstance.AddEmployee(new Employee("Ivan Petrov", 750, "089 165 2384"));
            Service.AutoShopInstance.AddEmployee(new Employee("Kiril Manolov", 800, "087 595 1635"));
            Service.AutoShopInstance.AddEmployee(new Employee("Iliya Szechev", 450, "085 168 4865"));
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainMenu), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Create a method to save all data (vehicles, employees, distibutors etc to a DB or a file)
            deferral.Complete();
        }
    }
}