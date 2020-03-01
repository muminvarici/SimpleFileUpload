using OfficeOpenXml;
using SimpleFileUpload.Entity.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleFileUpload.Core
{
	public class ExcelHelper : IExcelHelper
	{
		public List<Dictionary<string, string>> GetData(string path, int sheetNumber = 0)
		{
			try
			{
				List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
				//C:\Users\muminv\AppData\Local\Temp\tmpD011.tmp
				using (var package = new ExcelPackage(new FileInfo(path)))
				{
					var sheet = package.Workbook.Worksheets[sheetNumber];
					var sheetData = ((object[,])sheet.Cells.Value);
					var headers = sheet.GetHeaderColumns();
					for (int rowNumber = 1; rowNumber <= sheetData.GetUpperBound(0); rowNumber++)
					{
						//var rowContent = sheet.Row(rowNumber);
						try
						{
							Dictionary<string, string> data = GetRowData(sheetData, headers, rowNumber);
							result.Add(data);
						}
						catch (Exception e)
						{

							throw;
						}
					}

				}
				return result;
			}
			catch (Exception e)
			{
				throw;
			}
		}

		private Dictionary<string, string> GetRowData(object[,] sheetData, string[] headers, int rowNumber)
		{
			Dictionary<string, string> data = new Dictionary<string, string>();
			for (int columnNumber = 0; columnNumber < headers.Length; columnNumber++)
			{
				var cellData = sheetData[rowNumber, columnNumber].ToString();
				var field = headers[columnNumber];
				data[field] = cellData;
				//rowContent.
			}

			return data;
		}
	}

	public static class ExcelExtensions
	{
		public static string[] GetHeaderColumns(this ExcelWorksheet sheet)
		{
			List<string> columnNames = new List<string>();
			foreach (var cell in sheet.Cells[sheet.Dimension.Start.Row, sheet.Dimension.Start.Column, 1, sheet.Dimension.End.Column])
			{
				columnNames.Add(cell.Text);
				cell.Dispose();
			}
			return columnNames.ToArray();
		}
	}
}
