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
}
