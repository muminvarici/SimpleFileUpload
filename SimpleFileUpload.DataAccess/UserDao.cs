using SimpleFileUpload.Entity;
using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleFileUpload.DataAccess
{
	public static class UserDao
	{
		private static readonly string LogFileFormat = "{datetime}:{name-surname-mobileNo-birthDate-lastLocation}";
		private static readonly string UserLogFileName = "users.log";
		public static async void Save(UserModel user)
		{
			using (StreamWriter writer = File.CreateText(UserLogFileName))
			{
				string logString = ConvertUserToString(user);
				await writer.WriteAsync(logString);
			}
		}

		private static string ConvertUserToString(UserModel user)
		{
			string logString = LogFileFormat;
			logString.Replace("datetime", DateTime.Now.ToString());
			logString.Replace("name", user.Name);
			logString.Replace("surname", user.Surname);
			logString.Replace("mobileNo", user.MobileNo);
			logString.Replace("birthDate", user.BirthDate.ToShortDateString());
			logString.Replace("lastLocation", user.LastLocation);
			return logString;
		}
	}
}
