namespace K_BMI_Calculator;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    public void OnHeightImperialChanged(object sender, ValueChangedEventArgs e)
    {
        int ft = (int)(e.NewValue / 12);
        int inches = (int)(e.NewValue % 12);
        HeightImperialLabel.Text = $"{ft}ft. {inches}in.";
    }

    public void CalculateBMI(object sender, EventArgs e)
    {
        double BMI = 0;
        string units = Units.SelectedItem?.ToString();

        if (units == null)
        {
            ErrorLabel.Text = "Please select a unit first!";
            ErrorLabel.IsVisible = true;
            return;
        }
        ErrorLabel.IsVisible = false;

        if (units.Contains("Imperial"))
        {
            double heightImperial = HeightImperial.Value; // total inches
            double weightImperial = WeightImperial.Value;
            BMI = 703 * (weightImperial / Math.Pow(heightImperial, 2));
        }
        else if (units.Contains("Metric"))
        {
            double heightMetric = HeightMetric.Value;
            double weightMetric = WeightMetric.Value;
            BMI = weightMetric / Math.Pow((heightMetric / 100), 2);
        }
        else
        {
            BMI = -1;
        }

        string category;
        string description;
        Color categoryColor;

        if (BMI < 18.5)
        {
            category = "Underweight";
            description = "A BMI below 18.5 suggests you may be underweight. Consider speaking with a healthcare provider about a healthy diet plan.";
            categoryColor = Colors.SkyBlue;
        }
        else if (BMI < 25.0)
        {
            category = "Normal";
            description = "A BMI between 18.5 and 24.9 is considered a healthy weight range. Keep it up!";
            categoryColor = Colors.LightGreen;
        }
        else if (BMI < 30.0)
        {
            category = "Overweight";
            description = "A BMI between 25 and 29.9 suggests you may be overweight. A balanced diet and regular exercise can help.";
            categoryColor = Colors.Orange;
        }
        else
        {
            category = "Obese";
            description = "A BMI of 30 or above is considered obese. It is recommended to consult a healthcare provider.";
            categoryColor = Colors.Tomato;
        }

        PreResults.IsVisible = false;
        Results.IsVisible = true;
        BMIValueLabel.Text = $"{BMI:F1}";
        BMICategoryLabel.Text = category;
        BMICategoryLabel.TextColor = categoryColor;
        BMIValueLabel.TextColor = categoryColor;
        BMIDescriptionLabel.Text = description;
    }

    public void ChangeUnits(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        string selected = picker.SelectedItem?.ToString();

        ImperialUnits.IsVisible = selected == "Imperial";
        MetricUnits.IsVisible = selected == "Metric";
    }

}