/*
 *  Leonardo Alps - alve0024@algonquincollege.com
 * 
 * 	Instructor: Tony
 * 
 */

using System;
using Plugin.Media;
using Xamarin.Forms;

namespace Phoneword
{
	public class MainPage : ContentPage
	{
		Entry phoneNumberText;
		Button translateButton;
		Button callButton;
		Button aboutButton;
		Button takePictureButton;
		Button videoButton;
		Button pickPictureButton;
		string translatedNumber;
		Image image;

		public MainPage()
		{
			this.Title = "Main";
			this.Padding = new Thickness(20, 40, 20, 20);
			this.BackgroundImage = "wallpaper.jpg";

			// Setup layout definition to the Main Page
			StackLayout panel = new StackLayout
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Vertical,
				Spacing = 15,
			};

			panel.Children.Add(new Label
			{
				Text = "Enter a Phoneword:",
			});

			panel.Children.Add(phoneNumberText = new Entry 
			{ 
				Text = "1-613-XAMARIN",
			});

			panel.Children.Add(translateButton = new Button 
			{ 
				Text = "Translate",
			});

			// Starts enabled and only after the user tap translate
			// this button is enable to make the call
			panel.Children.Add(callButton = new Button 
			{ 
				Text = "Call",
				IsEnabled = false,
			});

			panel.Children.Add(takePictureButton = new Button
			{
				Text = "Take a picture",
			});

			panel.Children.Add(pickPictureButton = new Button 
			{ 
				Text = "Choose a picture"
			});

			panel.Children.Add(videoButton = new Button
			{
				Text = "Pick a Video",
			});

			panel.Children.Add(aboutButton = new Button 
			{ 
				Text = "About",
			});

			panel.Children.Add(image = new Image
			{
				Aspect = Aspect.AspectFit
			});

			// Set the event whe the button is tapped.
			translateButton.Clicked += OnTranslate;
			callButton.Clicked += OnCall;
			aboutButton.Clicked += OnAboutButtonClicked;
			takePictureButton.Clicked += OnPictureButtonClicked;
			videoButton.Clicked += OnVideoButtonClicked;
			pickPictureButton.Clicked += OnPickPictureButtonClicked;

			// Set the contont of the page
			this.Content = panel;
		}

		// Used to translate from a word to numbers
		private void OnTranslate(object sender, EventArgs e)
		{
			string enteredNumber = phoneNumberText.Text;
			translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber);
			if (!string.IsNullOrEmpty(translatedNumber))
			{
				callButton.IsEnabled = true;
				callButton.Text = "Call " + translatedNumber;
			}
			else 
			{
				callButton.IsEnabled = false;
				callButton.Text = "Call";
			}
		}

		// Cross-platform method that makes the call
		async void OnCall(object sender, System.EventArgs e)
		{
			if (await this.DisplayAlert(
				"Dial a Number",
				"Would you like to call " + translatedNumber + "?",
				"Yes",
				"No"))
			{
				// Call the interface and according to the platform
				// compiled the implemented method is called.
				var dialer = DependencyService.Get<IDialer>();
				if (dialer != null)
				{
					// Call the methos on iOS or Android
					await dialer.DialAsync(translatedNumber);	
				}
			}
		}

		private async void OnAboutButtonClicked(object sender, EventArgs e)
		{
			Page AboutPage = new NavigationPage(new AboutPage());
			await Navigation.PushAsync(AboutPage, true);
		}

		private async void OnPictureButtonClicked(object sender, EventArgs e)
		{
			Page PicturePage = new NavigationPage(new PicturePage());
			await Navigation.PushAsync(PicturePage, true);
		}

		private async void OnVideoButtonClicked(object sender, EventArgs e)
		{
			if (!CrossMedia.Current.IsPickVideoSupported)
			{
				await DisplayAlert("Videos Not Supported", ":( Permission not granted to videos.", "OK");
				return;
			}
			var file = await CrossMedia.Current.PickVideoAsync();

			if (file == null)
				return;

			await DisplayAlert("Video Selected", "Location: " + file.Path, "OK");
			file.Dispose();
		}

		private async void OnPickPictureButtonClicked(object sender, EventArgs e)
		{
			if (!CrossMedia.Current.IsPickPhotoSupported)
	        {
	          await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
	          return;
	        }
	        var file = await CrossMedia.Current.PickPhotoAsync();


	        if (file == null)
	          return;

	        image.Source = ImageSource.FromStream(() =>
	        {
	          var stream = file.GetStream();
			  file.Dispose();
	          return stream;
	        });
		}
	}
}
