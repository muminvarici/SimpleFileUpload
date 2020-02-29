using SimpleFileUpload.Entity;
using System.Linq;

namespace SimpleFileUpload.Context
{
	public abstract class MainContext
	{
		public static string GetMessageAction<T>()
		{
			var customAttribute =
			typeof(T).GetCustomAttributes(typeof(ActionAttribute), false).FirstOrDefault() as ActionAttribute;
			return customAttribute.Name;
		}
	}
}
