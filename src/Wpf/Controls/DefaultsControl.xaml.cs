using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeLogger.Domain;
using TimeLogger.Domain.Models;

namespace Wpf.Controls;
/// <summary>
/// Interaction logic for DefaultsControl.xaml
/// </summary>
public partial class DefaultsControl : UserControl
{
    public DefaultsControl()
    {
        InitializeComponent();
        LoadDefaultsFromDatabase();
    }

    private void LoadDefaultsFromDatabase()
    {
        using var ctx = new TimeLoggerDbContext();
        var model = ctx.Defaults.FirstOrDefault();

        if (model != null)
        {
            hourlyRateTextBox.Text = model.HourlyRate.ToString();
            preBillCheckbox.IsChecked = model.HourlyRate > 0;
            hasCutOffCheckbox.IsChecked = model.HasCutOff;
            cutOffTextbox.Text = model.CutOff.ToString();
            minimumHoursTextbox.Text = model.MinimumHours.ToString();
            billingIncrementTextbox.Text = model.BillingIncrement.ToString();
            roundUpAfterTextbox.Text = model.RoundUpAfterXMinutes.ToString();
        } else
        {
            hourlyRateTextBox.Text = "0";
            preBillCheckbox.IsChecked = true;
            hasCutOffCheckbox.IsChecked = false;
            cutOffTextbox.Text = "0";
            minimumHoursTextbox.Text = "0.25";
            billingIncrementTextbox.Text = "0.25";
            roundUpAfterTextbox.Text = "0";
        }
    }

    private void submitForm_Click(object sender, RoutedEventArgs e)
    {
        var form = ValidateForm();

        if (form.isValid == true)
        {
            SaveToDatabase(form.model);
        } 
        else
        {
            MessageBox.Show("The form is not valid. Please check your entries and try again.");
            return;
        }
    }

    private void SaveToDatabase(Defaults model)
    {
        using TimeLoggerDbContext context = new();
        context.Defaults.FromSql($"DELETE FROM Defaults");
        context.AddAsync(model);
        context.SaveChangesAsync();
    }

    private (bool isValid, Defaults model) ValidateForm()
    {
        bool isValid = true;
        Defaults model = new();

        try
        {
            model.PreBill = preBillCheckbox.IsChecked == true;
            model.HasCutOff = hasCutOffCheckbox.IsChecked == true;
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
}
