using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LifeTapLifeCounter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string Player1NameSettingsID = "Player1Name";
        private const string Player2NameSettingsID = "Player2Name";
        private const string StartingLifeSettingsID = "StartingLife";

        int? StartingLife = null;

        public MainPage()
        {
            this.InitializeComponent();

            // Load Player Names
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string Player1NameValue = localSettings.Values[Player1NameSettingsID] as string;
            if (!string.IsNullOrWhiteSpace(Player1NameValue))
            {
                Player1NameInput.Text = Player1NameValue;
            }

            string Player2NameValue = localSettings.Values[Player2NameSettingsID] as string;
            if (!string.IsNullOrWhiteSpace(Player2NameValue))
            {
                Player2NameInput.Text = Player2NameValue;
            }

            //Load Starting Life Totals
            StartingLife = localSettings.Values[StartingLifeSettingsID] as int?;
            if (StartingLife == null)
            {
                StartingLifeOption20.IsChecked = true;
                StartingLife = 20;
            }
            else if (StartingLife == 20)
            {
                StartingLifeOption20.IsChecked = true;
            }
            else
            {
                StartingLifeOption40.IsChecked = true;
                StartingLife = 40;
            }

            Player1Score.Text = StartingLife.Value.ToString();
            Player2Score.Text = StartingLife.Value.ToString();
        }

        private void Player1Minus1_Click(object sender, RoutedEventArgs e)
        {
            var score = int.Parse(Player1Score.Text);
            score--;
            Player1Score.Text = score.ToString();
        }

        private void Player1Plus1_Click(object sender, RoutedEventArgs e)
        {
            var score = int.Parse(Player1Score.Text);
            score++;
            Player1Score.Text = score.ToString();
        }

        private void Player2Minus2_Click(object sender, RoutedEventArgs e)
        {
            var score = int.Parse(Player2Score.Text);
            score--;
            Player2Score.Text = score.ToString();
        }

        private void Player2Plus2_Click(object sender, RoutedEventArgs e)
        {
            var score = int.Parse(Player2Score.Text);
            score++;
            Player2Score.Text = score.ToString();
        }

        private async void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Windows.UI.Popups.MessageDialog("Are you sure you want to reset the score?");

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Yes") { Id = 0 });
            dialog.Commands.Add(new Windows.UI.Popups.UICommand("No") { Id = 1 });
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();
            if ((int)result.Id == 0)
            {
                Player1Score.Text = StartingLife.Value.ToString();
                Player2Score.Text = StartingLife.Value.ToString();
            }
        }

        private void Player1NameInput_LostFocus(object sender, RoutedEventArgs e)
        {
            // Save player name
            if (!string.IsNullOrWhiteSpace(Player1NameInput.Text))
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                localSettings.Values[Player1NameSettingsID] = Player1NameInput.Text;
            }
        }

        private void Player2NameInput_LostFocus(object sender, RoutedEventArgs e)
        {
            // Save player name
            if (!string.IsNullOrWhiteSpace(Player2NameInput.Text))
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                localSettings.Values[Player2NameSettingsID] = Player2NameInput.Text;
            }
        }

        private void StartingLifeOption20_Checked(object sender, RoutedEventArgs e)
        {
            StartingLife = 20;
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[StartingLifeSettingsID] = StartingLife.Value;
        }

        private void StartingLifeOption40_Checked(object sender, RoutedEventArgs e)
        {
            StartingLife = 40;
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[StartingLifeSettingsID] = StartingLife.Value;
        }

        private async void Hyperlink_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            // The URI to launch
            var uriBing = new Uri(@"mailto:leon@ferzkopp.net");

            // Launch the URI
            var success = await Windows.System.Launcher.LaunchUriAsync(uriBing);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // If we have a phone contract, hide the status bar
            if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
            {
                var statusBar = StatusBar.GetForCurrentView();
                await statusBar.HideAsync();
            }
        }
    }
}
