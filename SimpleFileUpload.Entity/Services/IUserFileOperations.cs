using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileUpload.Entity.Services
{
	public interface IUserFileOperations
	{
		Task SaveAsync(UserModel user);
		Task SaveAsync(IEnumerable<UserModel> items);

	}
}
