﻿using System;
using System.IO;
using System.Threading.Tasks;
using Waher.Networking.HTTP;

namespace Waher.IoTGateway.WebResources
{
	/// <summary>
	/// Web Resource for updating where backup files are storeed.
	/// </summary>
	public class UpdateBackupFolderSettings : HttpSynchronousResource, IHttpPostMethod
	{
		/// <summary>
		/// Web Resource for updating where backup files are storeed.
		/// </summary>
		public UpdateBackupFolderSettings()
			: base("/UpdateBackupFolderSettings")
		{
		}

		/// <summary>
		/// If the resource handles sub-paths.
		/// </summary>
		public override bool HandlesSubPaths
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// If the resource uses user sessions.
		/// </summary>
		public override bool UserSessions
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// If the POST method is allowed.
		/// </summary>
		public bool AllowsPOST => true;

		/// <summary>
		/// Executes the POST method on the resource.
		/// </summary>
		/// <param name="Request">HTTP Request</param>
		/// <param name="Response">HTTP Response</param>
		/// <exception cref="HttpException">If an error occurred when processing the method.</exception>
		public async Task POST(HttpRequest Request, HttpResponse Response)
		{
			Gateway.AssertUserAuthenticated(Request);

			if (!Request.HasData)
				throw new BadRequestException();

			object Obj = Request.DecodeData();
			string[] P;

			if (!(Obj is string s))
				throw new BadRequestException();

			P = s.Split('\n');
			if (P.Length != 2)
				throw new BadRequestException();

			string ExportFolder = P[0].Trim();
			string KeyFolder = P[1].Trim();
			int Len;

			Len = ExportFolder.Length;
			if (Len > 0 && (ExportFolder[Len - 1] == '/' || ExportFolder[Len - 1] == '\\'))
				ExportFolder = ExportFolder.Substring(0, Len - 1);

			Len = KeyFolder.Length;
			if (Len > 0 && (KeyFolder[Len - 1] == '/' || KeyFolder[Len - 1] == '\\'))
				KeyFolder = KeyFolder.Substring(0, Len - 1);

			if (!string.IsNullOrEmpty(ExportFolder))
			{
				if (!Path.IsPathRooted(ExportFolder))
				{
					Response.StatusCode = 400;
					Response.StatusMessage = "Bad Request";
					Response.ContentType = "text/plain";
					await Response.Write("Export folder must be rooted. Relative paths are not accepted.");
					await Response.SendResponse();
					return;
				}

				if (!Directory.Exists(ExportFolder))
				{
					Response.StatusCode = 400;
					Response.StatusMessage = "Bad Request";
					Response.ContentType = "text/plain";
					await Response.Write("Export folder does not exist, or cannot be accessed or reached.");
					await Response.SendResponse();
					return;
				}

				try
				{
					s = ExportFolder + Path.DirectorySeparatorChar + "test.txt";
					File.WriteAllText(s, "test");
					File.Delete(s);
				}
				catch (Exception)
				{
					Response.StatusCode = 400;
					Response.StatusMessage = "Bad Request";
					Response.ContentType = "text/plain";
					await Response.Write("Not allowed to write data to the export folder.");
					await Response.SendResponse();
					return;
				}
			}

			if (!string.IsNullOrEmpty(KeyFolder))
			{
				if (!Path.IsPathRooted(KeyFolder))
				{
					Response.StatusCode = 400;
					Response.StatusMessage = "Bad Request";
					Response.ContentType = "text/plain";
					await Response.Write("Key folder must be rooted. Relative paths are not accepted.");
					await Response.SendResponse();
					return;
				}

				if (!Directory.Exists(KeyFolder))
				{
					Response.StatusCode = 400;
					Response.StatusMessage = "Bad Request";
					Response.ContentType = "text/plain";
					await Response.Write("Key folder does not exist, or cannot be accessed or reached.");
					await Response.SendResponse();
					return;
				}

				try
				{
					s = KeyFolder + Path.DirectorySeparatorChar + "test.txt";
					File.WriteAllText(s, "test");
					File.Delete(s);
				}
				catch (Exception)
				{
					Response.StatusCode = 400;
					Response.StatusMessage = "Bad Request";
					Response.ContentType = "text/plain";
					await Response.Write("Not allowed to write data to the key folder.");
					await Response.SendResponse();
					return;
				}
			}

			Export.ExportFolder = ExportFolder;
			Export.ExportKeyFolder = KeyFolder;

			Response.StatusCode = 200;
			Response.StatusMessage = "OK";
			await Response.SendResponse();
		}
	}
}
