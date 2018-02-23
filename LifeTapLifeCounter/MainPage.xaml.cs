using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

        private void TextBlock_SelectionChanged()
        {

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
            ContentDialog resetDialog = new ContentDialog
            {
                Title = " Are you sure you want to reset the score?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "No"
            };

            ContentDialogResult result = await resetDialog.ShowAsync();

            //Resets scores to 20 if "Yes" was clicked
            //Otherwise, do nothing
            if (result == ContentDialogResult.Primary)
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

        private void Player2NameInput_CharacterReceived(UIElement sender, CharacterReceivedRoutedEventArgs args)
        {
            if (args.Character == '\r')
            {
                FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
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
    }
}
