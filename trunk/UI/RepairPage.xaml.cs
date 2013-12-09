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
    public sealed partial class RepairPage : UI.Common.LayoutAwarePage
    {
        public RepairPage()
        {
            this.InitializeComponent();
        }

        private List<Repair> repairs;
        private Vehicle vehicle;
        private StructNavigator sn;

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
            vehicle = Service.AutoShopInstance.GetVehicleByIndex(sn.VehicleIndex);
            repairs = vehicle.Repairs;
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is disRepairded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void RegisteredRepairs_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateRepairs(repairs);
        }

        private void PopulateRepairs(List<Repair> RepairsToDisplay)
        {
            RegisteredRepairs.Items.Clear();

            foreach (var Repair in RepairsToDisplay)
            {
                var newListBoxItem = new ListBoxItem();

                newListBoxItem.Content = Repair.Caption + " " + Repair.Date;
                newListBoxItem.Tag = Repair;

                RegisteredRepairs.Items.Add(newListBoxItem);
            }
        }

        private void RegisteredRepairs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Repair selectedRepair = repairs[RegisteredRepairs.SelectedIndex];
            SelectedRepairDetails.Children.Clear();

            var selectedRepairProperties = selectedRepair.GetType().GetRuntimeProperties();

            foreach (var property in selectedRepairProperties)
            {
                var propertyStack = new StackPanel();
                propertyStack.Orientation = Orientation.Horizontal;
                SelectedRepairDetails.Children.Add(propertyStack);

                var propertyNameGrid = new Grid();
                propertyNameGrid.Width = 200;
                propertyStack.Children.Add(propertyNameGrid);

                var propertyValueGrid = new Grid();
                propertyStack.Children.Add(propertyValueGrid);
                if (property.Name == "ExchangedParts")
                {
                    var propertyNameBlock = new TextBlock();
                    propertyNameBlock.Text = "ExchangedParts";
                    propertyNameGrid.Children.Add(propertyNameBlock);

                    HyperlinkButton hb = new HyperlinkButton();
                    dynamic list = property.GetValue(selectedRepair);
                    List<Part> parts = list as List<Part>;
                    hb.Content = parts.Count().ToString();
                    hb.Click += HyperlinkButton_Click;

                    propertyValueGrid.Children.Add(hb);
                }
                else if (property.GetValue(selectedRepair) != null)
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
                    var value = property.GetValue(selectedRepair).ToString();
                    propertyValue.Text = value;
                    propertyValueGrid.Children.Add(propertyValue);
                }
            }
        }

        private void CancelRepairCreation_Click(object sender, RoutedEventArgs e)
        {
            AddRepairDialog.IsOpen = false;
        }

        private void AddRepair_Click(object sender, RoutedEventArgs e)
        {
            int guarantee = int.Parse(GuaranteeTextBox.Text);
            repairs.Add(new Repair(CaptionTextBox.Text, guarantee, new List<Part>())); // TODO: make page for parts
            Service.AutoShopInstance.GetVehicleByIndex((int)vehicle.Id).Repairs = repairs;
            App.SaveServiceInformation();
            this.Frame.Navigate(typeof(RepairPage), vehicle);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddRepairDialog.IsOpen = true;
        }

        private void AddRepairPropertyValuesField_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CaptionTextBox.Text != string.Empty && GuaranteeTextBox.Text != string.Empty)
            {
                int result = 0;

                if (int.TryParse(GuaranteeTextBox.Text, out result))
                {
                    AddRepair.IsEnabled = true;
                }
            }
            else
            {
                AddRepair.IsEnabled = false;
            }
        }

        private void EditRepairPropertyValuesField_TextChanged(object sender, TextChangedEventArgs e)
        {

            Repair currentlySelectedRepair = repairs[RegisteredRepairs.SelectedIndex];

            int guarantee = int.Parse(EditGuaranteeTextBox.Text);

            if (EditCaptionTextBox.Text != string.Empty &&
                EditGuaranteeTextBox.Text != string.Empty &&
                    (EditCaptionTextBox.Text != currentlySelectedRepair.Caption ||
                    guarantee != currentlySelectedRepair.Guarantee)
               )
            {
                int result = 0;

                if (int.TryParse(EditGuaranteeTextBox.Text, out result))
                {
                    SaveRepair.IsEnabled = true;
                }
                else
                {
                    SaveRepair.IsEnabled = false;
                }
            }
            else
            {
                SaveRepair.IsEnabled = false;
            }
        }

        private void SaveRepair_Click(object sender, RoutedEventArgs e)
        {

            Repair RepairToEdit = repairs[RegisteredRepairs.SelectedIndex];

            RepairToEdit.Caption = EditCaptionTextBox.Text;
            RepairToEdit.Guarantee = int.Parse(EditGuaranteeTextBox.Text);
           
            App.SaveServiceInformation();

            this.Frame.Navigate(typeof(RepairPage), sn);
        }

        private void CancelRepairEdit_Click(object sender, RoutedEventArgs e)
        {
            EditRepairDialog.IsOpen = false;
        }

        private void EditRepairPropertyValues_Loaded(object sender, RoutedEventArgs e)
        {
            Repair currentlySelectedRepair = repairs[RegisteredRepairs.SelectedIndex];

            EditCaptionTextBox.Text = currentlySelectedRepair.Caption;
            EditGuaranteeTextBox.Text = currentlySelectedRepair.Guarantee.ToString();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegisteredRepairs.SelectedItems.Count != 0)
            {
                EditRepairDialog.IsOpen = true;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(MainMenu));
            this.Frame.Navigate(typeof(CarsPage));
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchRepairDialog.IsOpen = true;
        }

        private void SearchRepairPropertyValues_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void KeywordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (KeywordTextBox.Text.Length >= 3)
            {
                SearchRepair.IsEnabled = true;
            }
            else
            {
                SearchRepair.IsEnabled = false;
            }
        }

        private void SearchRepair_Click(object sender, RoutedEventArgs e)
        {
            List<Repair> filteredRepairs = Helper.SearchForRepairs(KeywordTextBox.Text, vehicle);
            SearchRepairDialog.IsOpen = false;
            PopulateRepairs(filteredRepairs);
            ClearButton.IsEnabled = true;
        }

        private void CancelSearchRepair_Click(object sender, RoutedEventArgs e)
        {
            SearchRepairDialog.IsOpen = false;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearButton.IsEnabled = false;
            this.Frame.Navigate(typeof(RepairPage), sn);
        }

        private void ComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (CaptionTextBox.Text != string.Empty && GuaranteeTextBox.Text != string.Empty)
            {
                int result = 0;

                if (int.TryParse(GuaranteeTextBox.Text, out result))
                {
                    AddRepair.IsEnabled = true;
                }
            }
            else
            {
                AddRepair.IsEnabled = false;
            }
        }

        private void EditRepairPropertyValuesField_SelectionChanged(object sender, RoutedEventArgs e)
        {
            int guarantee = int.Parse(EditGuaranteeTextBox.Text);
            Repair currentlySelectedRepair = repairs[RegisteredRepairs.SelectedIndex];

            if (EditCaptionTextBox.Text != string.Empty &&
                EditGuaranteeTextBox.Text != string.Empty &&
                    (EditCaptionTextBox.Text != currentlySelectedRepair.Caption ||
                    guarantee != currentlySelectedRepair.Guarantee)
               )
            {
                int result = 0;

                if (int.TryParse(EditGuaranteeTextBox.Text, out result))
                {
                    SaveRepair.IsEnabled = true;
                }
                else
                {
                    SaveRepair.IsEnabled = false;
                }
            }
            else
            {
                SaveRepair.IsEnabled = false;
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            sn.RepairIndex = RegisteredRepairs.SelectedIndex;
            this.Frame.Navigate(typeof(PartPage), sn);
        }
    }
}
