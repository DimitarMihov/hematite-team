using GarageManagementSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace UI
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class CarsPage : UI.Common.LayoutAwarePage
    {
        public CarsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegisteredCars.SelectedValue != null)
            {
                DeleteCarConfirmationPopup.IsOpen = true;
            }            
        }

        private void RegisteredCars_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateVehicles();
        }

        private void PopulateVehicles()
        {
            RegisteredCars.Items.Clear();

            foreach (var vehicle in Service.AutoShopInstance.GetVehiclesList())
            {
                var newListBoxItem = new ListBoxItem();

                newListBoxItem.Content = vehicle.Manufacturer + " " + vehicle.Model + " " + vehicle.RegistrationNumber;
                newListBoxItem.Tag = vehicle;

                RegisteredCars.Items.Add(newListBoxItem);
            }
        }

        private void RegisteredCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Vehicle selectedCar = Service.AutoShopInstance.GetVehicleByIndex(RegisteredCars.SelectedIndex);
            SelectedCarDetails.Children.Clear();

            var selectedCarProperties = selectedCar.GetType().GetRuntimeProperties();

            foreach (var property in selectedCarProperties)
            {
                var propertyStack = new StackPanel();
                propertyStack.Orientation = Orientation.Horizontal;
                SelectedCarDetails.Children.Add(propertyStack);

                var propertyNameGrid = new Grid();
                propertyNameGrid.Width = 200;
                propertyStack.Children.Add(propertyNameGrid);

                var propertyValueGrid = new Grid();
                propertyStack.Children.Add(propertyValueGrid);

                if (property.GetValue(selectedCar) != null)
                {
                    var propertyNameBlock = new TextBlock();
                    string propertyName;

                    var descriptionAttribute = property.GetCustomAttributes().FirstOrDefault(attr => attr.GetType().Name == "DescriptionAttribute");

                    if (descriptionAttribute != null)
                    {
                        propertyName = descriptionAttribute.GetType().GetRuntimeProperty("Name").GetValue(descriptionAttribute).ToString().ToUpper();
                    }
                    else
                    {
                        propertyName = property.Name.ToString().ToUpper();
                    }

                    propertyNameBlock.Text = propertyName;
                    propertyNameGrid.Children.Add(propertyNameBlock);

                    var propertyValue = new TextBlock();
                    var value = property.GetValue(selectedCar).ToString();
                    propertyValue.Text = value;
                    propertyValueGrid.Children.Add(propertyValue);
                }
            }            
        }

        private void DeleteCar_Click(object sender, RoutedEventArgs e)
        {
            Service.AutoShopInstance.RemoveVehicle(Service.AutoShopInstance.GetVehicleByIndex(RegisteredCars.SelectedIndex));
            DeleteCarConfirmationPopup.IsOpen = false;
            this.Frame.Navigate(typeof(CarsPage));
        }

        private void CancelCarDeletion_Click(object sender, RoutedEventArgs e)
        {
            DeleteCarConfirmationPopup.IsOpen = false;
        }

        private void CancelCarCreation_Click(object sender, RoutedEventArgs e)
        {
            AddCarDialog.IsOpen = false;
        }

        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            Service.AutoShopInstance.AddVehicle(new Vehicle(ManufacuturerTextBox.Text, ModelTextBox.Text, int.Parse(YearTextBox.Text), RegNumberTextBox.Text));
            this.Frame.Navigate(typeof(CarsPage));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddCarDialog.IsOpen = true;
        }

        private void AddCarPropertyValuesField_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ManufacuturerTextBox.Text != string.Empty && ModelTextBox.Text != string.Empty && YearTextBox.Text != string.Empty && RegNumberTextBox.Text != string.Empty)
            {
                int result = 0;

                if (Int32.TryParse(YearTextBox.Text, out result))
                {
                    AddCar.IsEnabled = true;
                }
            }
            else
            {
                AddCar.IsEnabled = false;
            }
        }

        private void EditCarPropertyValuesField_TextChanged(object sender, TextChangedEventArgs e)
        {
            Vehicle currentlySelectedCar = Service.AutoShopInstance.GetVehicleByIndex(RegisteredCars.SelectedIndex);

            if (EditManufacuturerTextBox.Text != string.Empty && 
                EditModelTextBox.Text != string.Empty &&
                EditYearTextBox.Text != string.Empty &&
                EditRegNumberTextBox.Text != string.Empty &&
                    (EditManufacuturerTextBox.Text != currentlySelectedCar.Manufacturer || 
                    EditModelTextBox.Text != currentlySelectedCar.Model || 
                    EditYearTextBox.Text != currentlySelectedCar.Year.ToString() || 
                    EditRegNumberTextBox.Text != currentlySelectedCar.RegistrationNumber)
               )
            {
                int result = 0;

                if (Int32.TryParse(EditYearTextBox.Text, out result))
                {
                    SaveCar.IsEnabled = true;
                }
                else
                {
                    SaveCar.IsEnabled = false;
                }
            }
            else
            {
                SaveCar.IsEnabled = false;
            }
        }

        private void SaveCar_Click(object sender, RoutedEventArgs e)
        {
            Vehicle vehicleToEdit = Service.AutoShopInstance.GetVehicleByIndex(RegisteredCars.SelectedIndex);

            vehicleToEdit.Manufacturer = EditManufacuturerTextBox.Text;
            vehicleToEdit.Model = EditModelTextBox.Text;
            vehicleToEdit.Year = int.Parse(EditYearTextBox.Text);
            vehicleToEdit.RegistrationNumber = EditRegNumberTextBox.Text;

            this.Frame.Navigate(typeof(CarsPage));
        }

        private void CancelCarEdit_Click(object sender, RoutedEventArgs e)
        {
            EditCarDialog.IsOpen = false;
        }

        private void EditCarPropertyValues_Loaded(object sender, RoutedEventArgs e)
        {
            Vehicle currentlySelectedCar = Service.AutoShopInstance.GetVehicleByIndex(RegisteredCars.SelectedIndex);

            EditManufacuturerTextBox.Text = currentlySelectedCar.Manufacturer;
            EditModelTextBox.Text = currentlySelectedCar.Model;
            EditYearTextBox.Text = currentlySelectedCar.Year.ToString();
            EditRegNumberTextBox.Text = currentlySelectedCar.RegistrationNumber;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegisteredCars.SelectedItems.Count != 0)
            {
                EditCarDialog.IsOpen = true;
            }            
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainMenu));
        }
    }
}
