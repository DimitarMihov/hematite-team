using GarageManagementSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using System.Reflection;
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
    public sealed partial class DistributorsPage : UI.Common.LayoutAwarePage
    {
        public DistributorsPage()
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
            if (RegisteredDistributors.SelectedValue != null)
            {
                DeleteDistributorConfirmationPopup.IsOpen = true;
            }
        }

        private void RegisteredDistributors_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateDistributors();
        }

        private void PopulateDistributors()
        {
            RegisteredDistributors.Items.Clear();

            foreach (var distributor in Service.AutoShopInstance.GetDistributorsList())
            {
                var newListBoxItem = new ListBoxItem();

                newListBoxItem.Content = distributor.Name;
                newListBoxItem.Tag = distributor;

                RegisteredDistributors.Items.Add(newListBoxItem);
            }
        }

        private void RegisteredDistributors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Distributor selectedDistributor = Service.AutoShopInstance.GetDistributorByIndex(RegisteredDistributors.SelectedIndex);
            SelectedDistributorDetails.Children.Clear();

            var selectedDistributorProperties = selectedDistributor.GetType().GetRuntimeProperties();

            foreach (var property in selectedDistributorProperties)
            {
                var propertyStack = new StackPanel();
                propertyStack.Orientation = Orientation.Horizontal;
                SelectedDistributorDetails.Children.Add(propertyStack);

                var propertyNameGrid = new Grid();
                propertyNameGrid.Width = 200;
                propertyStack.Children.Add(propertyNameGrid);

                var propertyValueGrid = new Grid();
                propertyStack.Children.Add(propertyValueGrid);

                if (property.GetValue(selectedDistributor) != null)
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
                    var value = property.GetValue(selectedDistributor).ToString();
                    propertyValue.Text = value;
                    propertyValueGrid.Children.Add(propertyValue);
                }
            }
        }

        private void DeleteDistributor_Click(object sender, RoutedEventArgs e)
        {
            Service.AutoShopInstance.RemoveDistributor(Service.AutoShopInstance.GetDistributorByIndex(RegisteredDistributors.SelectedIndex));
            DeleteDistributorConfirmationPopup.IsOpen = false;
            this.Frame.Navigate(typeof(DistributorsPage));
        }

        private void CancelDistributorDeletion_Click(object sender, RoutedEventArgs e)
        {
            DeleteDistributorConfirmationPopup.IsOpen = false;
        }

        private void CancelDistributorCreation_Click(object sender, RoutedEventArgs e)
        {
            AddDistributorDialog.IsOpen = false;
        }

        private void AddDistributor_Click(object sender, RoutedEventArgs e)
        {
            Service.AutoShopInstance.AddDistributor(new Distributor(NameTextBox.Text, PhoneTextBox.Text, EmailTextBox.Text));
            this.Frame.Navigate(typeof(DistributorsPage));
        }

        private void EditDistributorPropertyValuesField_TextChanged(object sender, TextChangedEventArgs e)
        {
            Distributor currentlySelectedDistributor = Service.AutoShopInstance.GetDistributorByIndex(RegisteredDistributors.SelectedIndex);

            if (EditNameTextBox.Text != string.Empty &&
                EditPhoneTextBox.Text != string.Empty &&
                EditEmailTextBox.Text != string.Empty &&
                    (EditNameTextBox.Text != currentlySelectedDistributor.Name ||
                    EditPhoneTextBox.Text != currentlySelectedDistributor.Phone.ToString() ||
                    EditEmailTextBox.Text != currentlySelectedDistributor.Email)
               )
            {
                SaveDistributor.IsEnabled = true;
            }
            else
            {
                SaveDistributor.IsEnabled = false;
            }
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainMenu));
        }

        private void SaveDistributor_Click(object sender, RoutedEventArgs e)
        {
            Distributor distributorToEdit = Service.AutoShopInstance.GetDistributorByIndex(RegisteredDistributors.SelectedIndex);

            distributorToEdit.Name = EditNameTextBox.Text;
            distributorToEdit.Phone = EditPhoneTextBox.Text;
            distributorToEdit.Email = EditEmailTextBox.Text;

            this.Frame.Navigate(typeof(DistributorsPage));
        }

        private void CancelDistributorEdit_Click(object sender, RoutedEventArgs e)
        {
            EditDistributorDialog.IsOpen = false;
        }

        private void EditDistributorPropertyValues_Loaded(object sender, RoutedEventArgs e)
        {
            Distributor currentlySelectedDistributor = Service.AutoShopInstance.GetDistributorByIndex(RegisteredDistributors.SelectedIndex);

            EditNameTextBox.Text = currentlySelectedDistributor.Name;
            EditPhoneTextBox.Text = currentlySelectedDistributor.Phone;
            EditEmailTextBox.Text = currentlySelectedDistributor.Email;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegisteredDistributors.SelectedItems.Count != 0)
            {
                EditDistributorDialog.IsOpen = true;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddDistributorDialog.IsOpen = true;
        }

        private void AddDistributorPropertyValuesField_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameTextBox.Text != string.Empty && PhoneTextBox.Text != string.Empty && EmailTextBox.Text != string.Empty)
            {
                AddDistributor.IsEnabled = true;
            }
            else
            {
                AddDistributor.IsEnabled = false;
            }
        }
    }
}
