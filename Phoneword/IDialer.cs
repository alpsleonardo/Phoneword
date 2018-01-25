using System;
using System.Threading.Tasks;

namespace Phoneword
{
	// This interface is used as abstraction to be implemented
	// as platform-dependency by iOS and Android
	public interface IDialer
	{
		Task<bool> DialAsync(string number);
	}
}
