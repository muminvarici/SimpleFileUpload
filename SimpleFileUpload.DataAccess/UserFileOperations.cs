using SimpleFileUpload.Entity;
using SimpleFileUpload.Entity.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SimpleFileUpload.DataAccess
{
	public class UserFileOperations : IUserFileOperations
	{
		private readonly string LogFileFormat = "{datetime}:{name-surname-mobileNo-birthDate-lastLocation}" + Environment.NewLine;
		private readonly string UserLogFileName = "users.log";
		public async Task SaveAsync(UserModel user)
		{
			using (StreamWriter writer = File.AppendText(UserLogFileName))
			{
				string logString = ConvertUserToString(user);
				await writer.WriteAsync(logString);
			}
		}

		private string ConvertUserToString(UserModel user)
		{
			string logString = LogFileFormat;
			logString = logString.Replace("datetime", DateTime.Now.ToString());
			logString = logString.Replace("name", user.Name);
			logString = logString.Replace("surname", user.Surname);
			logString = logString.Replace("mobileNo", user.MobileNo);
			logString = logString.Replace("birthDate", user.BirthDate.ToShortDateString());
			logString = logString.Replace("lastLocation", user.LastLocation);
			return logString;
		}

		public async Task SaveAsync(IEnumerable<UserModel> items)
		{
			foreach (var item in items)
			{
				await SaveAsync(item);
			}
		}
	}
}
