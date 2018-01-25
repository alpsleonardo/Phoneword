using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Phoneword.iOS;

// Register the Service Dependency
[assembly: Dependency(typeof(PhoneDialer))]

// This class implements the interface IDialer
// that is used on the Cross-Platform app to call
// the right method according to the platform

namespace Phoneword.iOS
{
	public class PhoneDialer : IDialer
	{
		public Task<bool> DialAsync(string number)
		{
			return Task.FromResult( 
				UIApplication.SharedApplication.OpenUrl(
				new NSUrl("tel:" + number))
			);
		}
	}
}
