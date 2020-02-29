using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFileUpload.Message
{
	public class BaseResponse
	{
		public Exception Error { get; set; }
		public object Data { get; set; }
	}

	public class GridResponse<T> : BaseResponse
	{
		public new GridResult<T> Data { get; set; }
	}

	public class GridResult<T>
	{
		public long TotalRecords { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public IEnumerable<T> Items { get; set; }
	}
}
