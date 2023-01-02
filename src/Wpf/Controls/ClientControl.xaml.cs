using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TimeLogger.Domain;
using TimeLogger.Domain.Models;

namespace Wpf.Controls;
/// <summary>
/// Interaction logic for ClientControl.xaml
/// </summary>
public partial class ClientControl : UserControl
{
    private ObservableCollection<Client> clients = new();
    bool isNewEntry = true;
    public ClientControl()
    {
        InitializeComponent();
        InitializeClientList();
        WireUpClientDropdown();
        ToggleFormFieldsDisplay(false);
    }

    private void WireUpClientDropdown()
    {
        clientDropDown.ItemsSource = clients;
        clientDropDown.DisplayMemberPath = "Name";
        clientDropDown.SelectedValuePath = "Id";
    }

    private void InitializeClientList()
    {
        using var context = new TimeLoggerDbContext();
        clients = new(context.Client.OrderBy(x => x.Name).ToList());
    }

    private void newButton_Click(object sender, RoutedEventArgs e)
    {
        isNewEntry = true;

        ToggleFormFieldsDisplay(displayFields: true);

        LoadDefaults();

    }
    private void editButton_Click(object sender, RoutedEventArgs e)
    {
        isNewEntry = false;

        if (clientDropDown.SelectedItem == null)
        {
            MessageBox.Show("Please select a client first.");
            return;
        }
        ToggleFormFieldsDisplay(displayFields: true);

        LoadClient();

    }
    private void submitForm_Click(object sender, RoutedEventArgs e)
    {
        if (isNewEntry == true)
        {
            InsertClient();
        }
        else
        {
            UpdateClient();
        }
        ResetForm();
    }
    private void clearForm_Click(object sender, RoutedEventArgs e)
    {
        ResetForm();
    }

    private void InsertClient()
    {
        var form = ValidateForm();
        if (form.isValid != true)
        {
            MessageBox.Show("Client is not valid, please check your data and try again.");
            return;
        }
        else
        {
            using var context = new TimeLoggerDbContext();
            context.Add(form.model);
            context.SaveChanges();
            clients.Add(form.model);
        }
        ClearFormData();

        MessageBox.Show("Success");
    }

    private void LoadClient()
    {
        var clientId = (int)clientDropDown.SelectedValue;
        using var context = new TimeLoggerDbContext();
        var client = context.Client.FirstOrDefault(x => x.Id == clientId);
        nameTextBox.Text = client.Name;
        emailTextBox.Text = client.Email;
        hourlyRateTextBox.Text = client.HourlyRate.ToString();
        preBillCheckbox.IsChecked = client.PreBill;
        hasCutOffCheckbox.IsChecked = client.HasCutOff;
        cutOffTextbox.Text = client.CutOff.ToString();
        minimumHoursTextbox.Text = client.MinimumHours.ToString();
        billingIncrementTextbox.Text = client.BillingIncrement.ToString();
        roundUpAfterTextbox.Text = client.RoundUpAfterXMinutes.ToString();
    }

    private void UpdateClient()
    {
        var form = ValidateForm();

        if (form.isValid != true)
        {
            MessageBox.Show("Client is not valid, please check your data and try again.");
            return;
        }
        using var context = new TimeLoggerDbContext();
        context.Client.Update(form.model);
        context.SaveChanges();

        MessageBox.Show("Success");
    }

    private void ResetForm()
    {
        isNewEntry = true;
        ClearFormData();
        ToggleFormFieldsDisplay(false);
        WireUpClientDropdown();
    }

    private void ClearFormData()
    {
        nameTextBox.Text = "";
        emailTextBox.Text = "";
        hourlyRateTextBox.Text = "0";
        preBillCheckbox.IsChecked = true;
        hasCutOffCheckbox.IsChecked = false;
        cutOffTextbox.Text = "0";
        minimumHoursTextbox.Text = "0.25";
        billingIncrementTextbox.Text = "0.25";
        roundUpAfterTextbox.Text = "0";
    }

    private void LoadDefaults()
    {
        using var context = new TimeLoggerDbContext();
        Defaults? model = context.Defaults.FirstOrDefault();

        if (model == null)
        {
            ClearFormData();
            return;
        }
        nameTextBox.Text = "";
        emailTextBox.Text = "";
        hourlyRateTextBox.Text = model.HourlyRate.ToString();
        preBillCheckbox.IsChecked = model.HourlyRate > 0;
        hasCutOffCheckbox.IsChecked = model.HasCutOff;
        cutOffTextbox.Text = model.CutOff.ToString();
        minimumHoursTextbox.Text = model.MinimumHours.ToString();
        billingIncrementTextbox.Text = model.BillingIncrement.ToString();
        roundUpAfterTextbox.Text = model.RoundUpAfterXMinutes.ToString();
    }

    private (bool isValid, Client model) ValidateForm()
    {
        bool isValid = true;
        Client model = new();
        model.Id = isNewEntry != true ? (int)clientDropDown.SelectedValue : model.Id;

        try
        {
            model.Name = nameTextBox.Text;
            model.Email = emailTextBox.Text;

            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Email)) 
            {
                isValid = false;
            }

            model.PreBill = preBillCheckbox.IsChecked ?? false;
            model.HasCutOff = hasCutOffCheckbox.IsChecked ?? false;
            model.HourlyRate = float.Parse(hourlyRateTextBox.Text);
            model.CutOff = int.Parse(cutOffTextbox.Text);
            model.MinimumHours = float.Parse(minimumHoursTextbox.Text);
            model.BillingIncrement = float.Parse(billingIncrementTextbox.Text);
            model.RoundUpAfterXMinutes = int.Parse(roundUpAfterTextbox.Text);
        }
        catch
        {
            isValid = false;
        }

        return (isValid, model);
    }

    private void ToggleFormFieldsDisplay(bool displayFields)
    {
        Visibility display = displayFields ? Visibility.Visible : Visibility.Collapsed;
        Visibility buttonVisibility = !displayFields ? Visibility.Visible : Visibility.Collapsed;

        nameStackPanel.Visibility = display;
        emailStackPanel.Visibility = display;
        hourlyRateStackPanel.Visibility = display;
        checkBoxStackPanel.Visibility = display;
        hourOptionsStackPanel.Visibility = display;
        incrementsStackPanel.Visibility = display;
        submitForm.Visibility = display;
        editButton.Visibility = buttonVisibility;
        newButton.Visibility = buttonVisibility;
        clientStackPanel.Visibility = buttonVisibility;
        buttonStackPanel.Visibility = display;
    }


}
