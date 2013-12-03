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
    public sealed partial class EmployeesPage : UI.Common.LayoutAwarePage
    {
        public EmployeesPage()
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
            if (RegisteredEmployees.SelectedValue != null)
            {
                DeleteEmployeeConfirmationPopup.IsOpen = true;
            }
        }

        private void RegisteredEmployees_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateEmployees();
        }

        private void PopulateEmployees()
        {
            RegisteredEmployees.Items.Clear();

            foreach (var employee in Service.AutoShopInstance.GetEmployeesList())
            {
                var newListBoxItem = new ListBoxItem();

                newListBoxItem.Content = employee.Name + " " + employee.Position;
                newListBoxItem.Tag = employee;

                RegisteredEmployees.Items.Add(newListBoxItem);
            }
        }

        private void RegisteredEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee selectedEmployee = Service.AutoShopInstance.GetEmployeeByIndex(RegisteredEmployees.SelectedIndex);
            SelectedEmployeeDetails.Children.Clear();

            var selectedEmployeeProperties = selectedEmployee.GetType().GetRuntimeProperties();

            foreach (var property in selectedEmployeeProperties)
            {
                var propertyStack = new StackPanel();
                propertyStack.Orientation = Orientation.Horizontal;
                SelectedEmployeeDetails.Children.Add(propertyStack);

                var propertyNameGrid = new Grid();
                propertyNameGrid.Width = 200;
                propertyStack.Children.Add(propertyNameGrid);

                var propertyValueGrid = new Grid();
                propertyStack.Children.Add(propertyValueGrid);

                if (property.GetValue(selectedEmployee) != null)
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
                    var value = property.GetValue(selectedEmployee).ToString();
                    propertyValue.Text = value;
                    propertyValueGrid.Children.Add(propertyValue);
                }
            }
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            Service.AutoShopInstance.RemoveEmployee(Service.AutoShopInstance.GetEmployeeByIndex(RegisteredEmployees.SelectedIndex));
            DeleteEmployeeConfirmationPopup.IsOpen = false;
            this.Frame.Navigate(typeof(EmployeesPage));
        }

        private void CancelEmployeeDeletion_Click(object sender, RoutedEventArgs e)
        {
            DeleteEmployeeConfirmationPopup.IsOpen = false;
        }

        private void CancelEmployeeCreation_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeDialog.IsOpen = false;
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            Service.AutoShopInstance.AddEmployee(new Employee(NameTextBox.Text, decimal.Parse(SalaryTextBox.Text), PhoneTextBox.Text));
            this.Frame.Navigate(typeof(EmployeesPage));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeDialog.IsOpen = true;
        }

        private void AddEmployeePropertyValuesField_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameTextBox.Text != string.Empty && SalaryTextBox.Text != string.Empty && PhoneTextBox.Text != string.Empty)
            {
                decimal result = 0;

                if (decimal.TryParse(SalaryTextBox.Text, out result))
                {
                    AddEmployee.IsEnabled = true;
                }
            }
            else
            {
                AddEmployee.IsEnabled = false;
            }
        }

        private void EditEmployeePropertyValuesField_TextChanged(object sender, TextChangedEventArgs e)
        {
            Employee currentlySelectedEmployee = Service.AutoShopInstance.GetEmployeeByIndex(RegisteredEmployees.SelectedIndex);

            if (EditNameTextBox.Text != string.Empty &&
                EditSalaryTextBox.Text != string.Empty &&
                EditEmailTextBox.Text != string.Empty &&
                    (EditNameTextBox.Text != currentlySelectedEmployee.Name ||
                    EditSalaryTextBox.Text != currentlySelectedEmployee.Salary.ToString() ||
                    EditEmailTextBox.Text != currentlySelectedEmployee.Email)
               )
            {
                int result = 0;

                if (Int32.TryParse(EditSalaryTextBox.Text, out result))
                {
                    SaveEmployee.IsEnabled = true;
                }
                else
                {
                    SaveEmployee.IsEnabled = false;
                }
            }
            else
            {
                SaveEmployee.IsEnabled = false;
            }
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainMenu));
        }

        private void SaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee EmployeeToEdit = Service.AutoShopInstance.GetEmployeeByIndex(RegisteredEmployees.SelectedIndex);

            EmployeeToEdit.Name = EditNameTextBox.Text;
            EmployeeToEdit.Salary = int.Parse(EditSalaryTextBox.Text);
            EmployeeToEdit.Email = EditEmailTextBox.Text;

            this.Frame.Navigate(typeof(EmployeesPage));
        }

        private void CancelEmployeeEdit_Click(object sender, RoutedEventArgs e)
        {
            EditEmployeeDialog.IsOpen = false;
        }

        private void EditEmployeePropertyValues_Loaded(object sender, RoutedEventArgs e)
        {
            Employee currentlySelectedEmployee = Service.AutoShopInstance.GetEmployeeByIndex(RegisteredEmployees.SelectedIndex);

            EditNameTextBox.Text = currentlySelectedEmployee.Name;
            EditSalaryTextBox.Text = currentlySelectedEmployee.Salary.ToString();
            EditEmailTextBox.Text = currentlySelectedEmployee.Email;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegisteredEmployees.SelectedItems.Count != 0)
            {
                EditEmployeeDialog.IsOpen = true;
            }
        }
    }
}
