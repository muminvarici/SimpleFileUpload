using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SimpleFileUpload.Message
{
	public abstract class BaseRequest
	{
		//public string Action { get; }
		//public BaseRequest(string action)
		//{
		//	Action = action;
		//}
	}

	public abstract class BaseFilterRequest : BaseRequest
	{
		[DataMember]
		public int PageNumber { get; set; }
		[DataMember]
		public int PageSize { get; set; }
		[DataMember]
		public string Filter { get; set; }
	}
}
