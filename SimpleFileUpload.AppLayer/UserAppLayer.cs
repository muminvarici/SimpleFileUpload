using SimpleFileUpload.Core;
using SimpleFileUpload.DataAccess;
using SimpleFileUpload.Entity;
using SimpleFileUpload.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleFileUpload.AppLayer
{
	public class UserAppLayer
	{
		private static UserElasticSearch UserRepository = new UserElasticSearch();

		public static BaseResponse FilterByName(UserFilterByNameRequest request)
		{
			return new BaseResponse { Data = UserRepository.FilterByName(request.PageNumber, request.PageSize, request.Filter/* "Kaley"*/) };
		}

		public static BaseResponse FilterByPhone(UserFilterByPhoneRequest request)
		{
			return new BaseResponse { Data = UserRepository.FilterByPhone(request.PageNumber, request.PageSize, request.Filter/*"1-666"*/) };
		}

		public static void SaveUsers(string path)
		{
			var excelData = new ExcelHelper().GetData(path);
			var items = ExtractUserListFromExcel(excelData);
			try
			{
				new UserElasticSearch().IndexBulk(items);
			}
			catch (Exception e)
			{
				throw;
			}


		}

		/// <summary>
		/// This method does it's operation as async by task.
		/// </summary>
		private static HashSet<UserModel> ExtractUserListFromExcel(List<Dictionary<string, string>> data)
		{
			var items = new HashSet<UserModel>();
			var taskList = new List<Task>();
			foreach (var item in data)
			{
				var instance = new Task(() =>
				{
					try
					{
						var index = data.IndexOf(item) + 1;
						UserModel row = ExtractRow(data, index, item);
						lock (items)
						{
							items.Add(row);
						}
					}
					catch (Exception)
					{
						throw;
					}
				});
				instance.Start();
				taskList.Add(instance);
			}

			SpinWait.SpinUntil(() =>
			{
				return taskList.FirstOrDefault(w => !w.IsCompleted) == null;
			});

			return items;
		}

		private static UserModel ExtractRow(List<Dictionary<string, string>> data, int index, Dictionary<string, string> item)
		{
			return new UserModel
			{
				BirthDate = Convert.ToDateTime(item["Birth Date"]),
				Name = item["Name"],
				Surname = item["Surname"],
				LastLocation = item["Last Location"],
				MobileNo = item["Mobile No"],
				Id = index
			};
		}
	}
}
