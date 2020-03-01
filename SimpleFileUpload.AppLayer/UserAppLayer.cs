using Elasticsearch.Net;
using SimpleFileUpload.Entity;
using SimpleFileUpload.Entity.Services;
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
		private readonly IUserElasticSearch UserRepository;
		private readonly IUserFileOperations UserFileOperations;
		private readonly IExcelHelper ExcelHelper;

		public UserAppLayer(IUserElasticSearch userRepository, IUserFileOperations userFileOperations, IExcelHelper excelHelper)
		{
			UserRepository = userRepository;
			ExcelHelper = excelHelper;
			UserFileOperations = userFileOperations;
		}

		public BaseResponse FilterByName(UserFilterByNameRequest request)
		{
			var items = UserRepository.FilterByName(request.PageNumber, request.PageSize, request.Filter, out long totalRecords);
			return new GridResponse<UserModel>
			{
				Data = new GridResult<UserModel>
				{
					Items = items,
					PageNumber = request.PageNumber,
					PageSize = request.PageSize,
					TotalRecords = totalRecords
				}
			};
		}

		public BaseResponse FilterByPhone(UserFilterByPhoneRequest request)
		{
			var items = UserRepository.FilterByPhone(request.PageNumber, request.PageSize, request.Filter, out long totalRecords);
			return new GridResponse<UserModel>
			{
				Data = new GridResult<UserModel>
				{
					Items = items,
					PageNumber = request.PageNumber,
					PageSize = request.PageSize,
					TotalRecords = totalRecords
				}
			};
		}

		public void SaveUsers(string path)
		{
			var excelData = ExcelHelper.GetData(path);
			var items = ExtractUserListFromExcel(excelData);
			try
			{
				if (!UserRepository.IndexBulk(items, out object error))
				{
					throw new Exception(((ServerError)error).Error.Reason);
				}
				UserFileOperations.SaveAsync(items);
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// This method does it's operation as async by task.
		/// </summary>
		private HashSet<UserModel> ExtractUserListFromExcel(List<Dictionary<string, string>> data)
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

		private UserModel ExtractRow(List<Dictionary<string, string>> data, int index, Dictionary<string, string> item)
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
