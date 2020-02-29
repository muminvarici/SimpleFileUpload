using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SimpleFileUpload.Entity
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public class ActionAttribute : Attribute
	{
		public string Name { get; set; }
	}
}
