using System;
using Xamarin.Forms;

namespace Phoneword
{
	public class AboutPage : ContentPage
	{
		Image image;

		public AboutPage()
		{
			this.Title = "About";
            this.Padding = new Thickness(20, 40, 20, 20);

			// Set the layout definition for the About Page
			StackLayout panel = new StackLayout
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Vertical,
				Spacing = 15,
			};

			panel.Children.Add(new Label 
			{ 
				Text = "Phoneword App",
				FontSize = 30,
				FontAttributes = FontAttributes.Bold,
			});

			panel.Children.Add(new Label
			{ 
				Text = "Version: 0.0.0.1",
			});

			panel.Children.Add(image = new Image
			{
				Aspect = Aspect.AspectFit,
				Source = "leo_profile.jpg",
			});

			panel.Children.Add(new Label{
				Text = "Developed by Leonardo Alps",
			});

			panel.Children.Add(new Label{
				Text = "alve0024@algonquinlive.com",
			});

			Content = panel;
		}
	}
}
