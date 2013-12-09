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
    public sealed partial class PartPage : UI.Common.LayoutAwarePage
    {
        public PartPage()
        {
            this.InitializeComponent();
        }

        StructNavigator sn;
        private List<Part> parts;
        private Repair repair;

        private Distributor distirbutor;
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
            sn = (StructNavigator)navigationParameter;

            if (sn.IsDistributor)
            {
                distirbutor = Service.AutoShopInstance.Distributors[sn.DistributorIndex];
                parts = distirbutor.Parts;
            }
            else
            {
                Repair repair = Service.AutoShopInstance.GetVehicleByIndex(sn.VehicleIndex).Repairs[sn.RepairIndex];
                parts = repair.ExchangedParts;
            }
        }
        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is disPartded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void RegisteredParts_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateParts(parts);
        }

        private void PopulateParts(List<Part> partsToDisplay)
        {
            RegisteredParts.Items.Clear();

            foreach (var part in partsToDisplay)
            {
                var newListBoxItem = new ListBoxItem();

                newListBoxItem.Content = part.Name;
                newListBoxItem.Tag = part;

                RegisteredParts.Items.Add(newListBoxItem);
            }
        }

        private void RegisteredParts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Part selectedPart = parts[RegisteredParts.SelectedIndex];
            SelectedPartDetails.Children.Clear();

            var selectedPartProperties = selectedPart.GetType().GetRuntimeProperties();

            foreach (var property in selectedPartProperties)
            {
                var propertyStack = new StackPanel();
                propertyStack.Orientation = Orientation.Horizontal;
                SelectedPartDetails.Children.Add(propertyStack);

                var propertyNameGrid = new Grid();
                propertyNameGrid.Width = 200;
                propertyStack.Children.Add(propertyNameGrid);

                var propertyValueGrid = new Grid();
                propertyStack.Children.Add(propertyValueGrid);
                if (property.Name == "VehicleList"){ }
                else if (property.GetValue(selectedPart) != null)
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
                    var value = property.GetValue(selectedPart).ToString();
                    propertyValue.Text = value;
                    propertyValueGrid.Children.Add(propertyValue);
                }
            }
        }



        private void CancelPartCreation_Click(object sender, RoutedEventArgs e)
        {
            AddPartDialog.IsOpen = false;
        }

        private void AddPart_Click(object sender, RoutedEventArgs e)
        {
            long id = long.Parse(IdTextBox.Text);
            decimal price = decimal.Parse(PriceTextBox.Text);
            parts.Add(new Part(id, NameTextBox.Text, price));
            if (sn.IsDistributor)
            {
                Service.AutoShopInstance.Distributors[sn.DistributorIndex].Parts = parts;
            }
            else
            {
                Service.AutoShopInstance.GetVehicleByIndex(sn.VehicleIndex).Repairs[sn.RepairIndex].ExchangedParts = parts;
            }
                App.SaveServiceInformation();
            this.Frame.Navigate(typeof(PartPage), sn);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddPartDialog.IsOpen = true;
        }

        private void AddPartPropertyValuesField_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IdTextBox.Text != string.Empty && NameTextBox.Text != string.Empty && PriceTextBox.Text != string.Empty)
            {
                long id = 0;
                decimal price = 0;
                if (long.TryParse(IdTextBox.Text, out id) && decimal.TryParse(PriceTextBox.Text, out price))
                {
                    AddPart.IsEnabled = true;
                }
            }
            else
            {
                AddPart.IsEnabled = false;
            }
        }

        private void EditPartPropertyValuesField_TextChanged(object sender, TextChangedEventArgs e)
        {

            long id = long.Parse(EditIdTextBox.Text);
            decimal price = decimal.Parse(EditPriceTextBox.Text);
            Part currentlySelectedPart = parts[RegisteredParts.SelectedIndex];


            if (EditIdTextBox.Text != string.Empty &&
                EditNameTextBox.Text != string.Empty &&
                EditPriceTextBox.Text != string.Empty &&
                (id != currentlySelectedPart.Id ||
                    price != currentlySelectedPart.Price || EditNameTextBox.Text != currentlySelectedPart.Name)
               )
            {
                long idNew = 0;
                decimal priceNew = 0;
                if (long.TryParse(EditIdTextBox.Text, out idNew) && decimal.TryParse(EditPriceTextBox.Text, out priceNew))
                {
                    SavePart.IsEnabled = true;
                }
                else
                {
                    SavePart.IsEnabled = false;
                }
            }
            else
            {
                SavePart.IsEnabled = false;
            }
        }

        private void SavePart_Click(object sender, RoutedEventArgs e)
        {

            Part partToEdit = parts[RegisteredParts.SelectedIndex];

            partToEdit.Name = EditNameTextBox.Text;

            partToEdit.Id = long.Parse(EditIdTextBox.Text);
            partToEdit.Price = decimal.Parse(EditPriceTextBox.Text);

            App.SaveServiceInformation();

            this.Frame.Navigate(typeof(PartPage), sn);
        }

        private void CancelPartEdit_Click(object sender, RoutedEventArgs e)
        {
            EditPartDialog.IsOpen = false;
        }

        private void EditPartPropertyValues_Loaded(object sender, RoutedEventArgs e)
        {
            Part currentlySelectedPart = parts[RegisteredParts.SelectedIndex];

            EditIdTextBox.Text = currentlySelectedPart.Id.ToString();
            EditNameTextBox.Text = currentlySelectedPart.Name;
            EditPriceTextBox.Text = currentlySelectedPart.Price.ToString();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegisteredParts.SelectedItems.Count != 0)
            {
                EditPartDialog.IsOpen = true;
            }
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainMenu));
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPartDialog.IsOpen = true;
        }

        private void SearchPartPropertyValues_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void KeywordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (KeywordTextBox.Text.Length >= 3)
            {
                SearchPart.IsEnabled = true;
            }
            else
            {
                SearchPart.IsEnabled = false;
            }
        }

        private void SearchPart_Click(object sender, RoutedEventArgs e)
        {
            List<Part> filteredParts = Helper.SearchForParts(KeywordTextBox.Text, repair);
            SearchPartDialog.IsOpen = false;
            PopulateParts(filteredParts);
            ClearButton.IsEnabled = true;
        }

        private void CancelSearchPart_Click(object sender, RoutedEventArgs e)
        {
            SearchPartDialog.IsOpen = false;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearButton.IsEnabled = false;
            this.Frame.Navigate(typeof(PartPage), sn);
        }
    }
}
