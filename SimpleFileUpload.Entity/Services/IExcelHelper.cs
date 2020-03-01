using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFileUpload.Entity.Services
{
	public interface IExcelHelper
	{
		List<Dictionary<string, string>> GetData(string path, int sheetNumber = 0);
	}
}
