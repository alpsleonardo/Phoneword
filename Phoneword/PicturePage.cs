using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;

namespace Phoneword
{
	public class PicturePage : ContentPage
	{
		Button takePictureButton;
		Image image;
		
		public PicturePage()
		{
			this.Title = "Media";
			this.Padding = new Thickness(20, 40, 20, 20);
            this.BackgroundImage = "wallpaper.jpg";

			StackLayout panel = new StackLayout
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Vertical,
				Spacing = 15,
			};


			// Define an image button that is used to take a picture
			panel.Children.Add(takePictureButton = new Button 
			{
				Image = "cam.ico",
			});

			panel.Children.Add(image = new Image
			{
				Aspect = Aspect.AspectFit
			});

			// Set the Event
			takePictureButton.Clicked += OnPictureButtonClicked;

			// Set the content of the page
			this.Content = panel;
		}


		// Simple cross platform plugin to take photos and video or pick them 
		// from a gallery from shared code.
		// https://components.xamarin.com/view/mediaplugin
		async void OnPictureButtonClicked(object sender, EventArgs e)
		{
			// Check if the camera is available or supported	
			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
			{
				await DisplayAlert("No camera", "No camera available.", "OK");
				return;
			}

			// Take the picture
			var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
			{
				Directory = "My Photos",
				Name = "my_img" + DateTime.Today.ToString() + "+jpg"
			});

			if (file == null) return;

			// Show the image on the page
			image.Source = ImageSource.FromStream(() =>
			{
				var stream = file.GetStream();
				file.Dispose();
				return stream;
			});
		}
	}
}
