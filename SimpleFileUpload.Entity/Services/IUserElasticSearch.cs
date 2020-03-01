using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFileUpload.Entity.Services
{
	public interface IUserElasticSearch
	{
		IEnumerable<UserModel> FilterByName(int pageNumber, int pageSize, string filter, out long totalRecords);
		IEnumerable<UserModel> FilterByPhone(int pageNumber, int pageSize, string filter, out long totalRecords);
		bool Index(UserModel item, string indexName);
		bool IndexBulk(IEnumerable<UserModel> items, out object error);
	}
}
