using GarageManagementSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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

            LoadHardCodeInformation(); // TODO: Comment that after creating the load methods
            SaveServiceInformation("serverInfo"); // TODO: Transfer this to the adding and removing methods
            // TODO: Create methods to load cars, employees, distributors etc from a file
            // LoadServiceInformation();
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


        private static void LoadHardCodeInformation()
        {
            // Owners
            Address owner1Address = new Address("Sofia", "2000", "Geo Milev", "ul. Ivan Vazov", 2, "no comment");
            Owner owner1 = new Owner("Ivan Peshev", owner1Address, "0883442233", "coco@ABV.BG", "no comment");

            Address owner2Address = new Address("Plovdiv", "1800", "j.k. Zapad", "ul. Opulchenska", 132, "no comment");
            Owner owner2 = new Owner("Georgi Georgiev", owner2Address, "0883412333", "macao@abv.bg", "no comment");

            // Parts
            List<Part> parts = new List<Part>(){ 
                new Part(1234, "Wheel", 50.00m, new List<VehicleInformation>(){ 
                    new VehicleInformation("Peugeot", "106", 1999, FuelType.Diesel, Gearbox.Automatic),
                    new VehicleInformation("BMW", "5", 2002, FuelType.Electric, Gearbox.SemiAutomatic),
                    new VehicleInformation("Mercedes", "SLK", 2010, FuelType.Electric, Gearbox.SemiAutomatic)
                    } )
            };

            // Repairs
            List<Repair> repairsCar1 = new List<Repair>();
            repairsCar1.Add(new Repair("Change wheels", 48, parts, DateTime.Now));

            List<Repair> repairsCar2 = new List<Repair>();
            repairsCar2.Add(new Repair("Change front wheels", 48, parts, DateTime.Now));

            // Vehicles
            Service.AutoShopInstance.AddVehicle(new Vehicle("Peugeot", "106", 1999, 80, 30000,
                FuelType.Diesel, Gearbox.Automatic, owner1, "red", "very good", "CA 1234 AC", repairsCar1, Status.Accepted));
            
            Service.AutoShopInstance.AddVehicle(new Vehicle("BMW", "5", 2002, 80, 350000, FuelType.Electric, Gearbox.SemiAutomatic,
                owner2, "red", "no comments", "PA 8750 HA", repairsCar2, Status.Accepted));
            
            Service.AutoShopInstance.AddVehicle(new Vehicle("Mercedes", "SLK", 2010, 140, 110000, FuelType.Electric, Gearbox.SemiAutomatic,
                owner2, "red", "no comments", "A 8993 MM", repairsCar2, Status.Accepted));

            // Distributors
            Address distributor1Address = new Address("Sofia", "2000", "j.k. Studentski grad", "ul. Vladishka", 23, "no comment");
            Address distributor2Address = new Address("Sofia", "2000", "Obelq", "ul. Hristo Smirnenski", 55, "no comment");
            Address distributor3Address = new Address("Sofia", "2000", "j.k. Dianabat", "ul. G.M.Dimitrov", 111, "no comment");
           
            Service.AutoShopInstance.AddDistributor(new Distributor("MotoPfohe Ltd", distributor1Address, "089 901 5632", "order@motopfohe.bg", "no comment"));
            Service.AutoShopInstance.AddDistributor(new Distributor("BetterCars Ltd", distributor2Address, "089 598 8915", "order_parts@bettercars.bg", "no comment"));
            Service.AutoShopInstance.AddDistributor(new Distributor("PartsForPeople Ltd", distributor3Address, "088 991 5987", "office@peopleparts.bg", "no comment"));

            // Employees
            Address employee1Address = new Address("Sofia", "2000", "j.k. Mladost 1", "ul. Aleksander Batemberg", 39, "no comment");
            Address employee2Address = new Address("Sofia", "2000", "j.k. Lulin", "ul. Todor Kableshkov", 24, "no comment");
            
            Service.AutoShopInstance.AddEmployee(new Employee(
                "Marin Ivanov", employee1Address, "0883442233", "coco@ABV.BG", "no comment", 500, Position.Accountant, 2));
            Service.AutoShopInstance.AddEmployee(new Employee(
                "Kiril Manolov", employee2Address, "0883212233", "kkks@ABV.BG", "no comment", 300, Position.JunorMechanic, 2));
        }

        public async static void LoadServiceInformation()
        {
            StorageFile file = await KnownFolders.PicturesLibrary.GetFileAsync("vehicleinfo.txt");

            if (file != null)
            {
                try
                {
                    string fileContent = await FileIO.ReadTextAsync(file); // Chete go celiq
                    // Spit po \n\r

                    //OutputTextBlock.Text = "The following text was read from '" + file.Name + "':" + Environment.NewLine + Environment.NewLine + fileContent;
                }
                catch (FileNotFoundException)
                {
                    //rootPage.NotifyUserFileNotExist();
                }
            }
            else
            {
                // TODO: create the file
            }
        }


        public async static void SaveServiceInformation(string fileName)
        {
            string fullPath = fileName + ".txt";
            StorageFile writer = null;
            bool isFileExists = false;

            try
            {
                writer = await KnownFolders.PicturesLibrary.GetFileAsync(fullPath);
            }
            catch (FileNotFoundException)
            {
                CreateFile(fullPath);
                isFileExists = true;
            }

            if (isFileExists)
            {
                writer = await KnownFolders.PicturesLibrary.GetFileAsync(fullPath);
            }

            try
            {
                string userContent = Service.SaveServiceInformation();

                if (!String.IsNullOrEmpty(userContent))
                {

                    await FileIO.WriteTextAsync(writer, userContent);
                    // OutputTextBlock.Text = "The following text was written to '" + writer.Name + "':" + Environment.NewLine + Environment.NewLine + userContent;
                }
                else
                {
                    //OutputTextBlock.Text = "The text box is empty, please write something and then click 'Write' again.";
                }
            }
            catch (FileNotFoundException)
            {
                //rootPage.NotifyUserFileNotExist();
            }

        }

        public async static void CreateFile(string fileName)
        {
            StorageFolder storageFolder = KnownFolders.PicturesLibrary;
            StorageFile file = await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
        }
    }
}
