using KaYi.FileSystem.DAL;
using KaYi.FileSystem.Model;
using System;
using System.Collections.Generic;
namespace KaYi.FileSystem.Services
{
	public static class FileSystemService
	{
		private static FileGateway fileGateway = new FileGateway();
		private static FileHitGateway hitGateway = new FileHitGateway();
		public static void HitFile(string fileID, string sessionID, string clientID, string hitType)
		{
			FileHit fileHit = new FileHit();
			fileHit.ClientID = clientID;
			fileHit.CreateTime = DateTime.Now;
			fileHit.FileID = fileID;
			fileHit.HitID = Guid.NewGuid().ToString();
			fileHit.HitTime = DateTime.Now;
			fileHit.HitType = hitType;
			fileHit.SessionID = sessionID;
			fileHit.UpdateStamp = Guid.NewGuid().ToString();
			fileHit.UpdateTime = DateTime.Now;
			FileSystemService.hitGateway.AddNew(fileHit);
		}
		private static File ReadExtendPropertiesOfFile(File file)
		{
			if (file == null)
			{
				return null;
			}
			if (file.FileType == FileTypes.Folder)
			{
				IList<File> allFilesInFolder = FileSystemService.GetAllFilesInFolder(file.FileID);
				foreach (File current in allFilesInFolder)
				{
					file.TotalSize += current.FileSize;
				}
				file.TotalFiles = allFilesInFolder.Count;
				file.TotalFolders = 0;
			}
			return file;
		}
		private static IList<File> GetAllFilesInFolder(string folderID)
		{
			int num = 0;
			IList<File> filesWithoutExtendPropertyBy = FileSystemService.GetFilesWithoutExtendPropertyBy("", "", folderID, "", "", FileStates.NotSpecified, FileTypes.File, "", false, 0, 9999, out num);
			using (IEnumerator<File> enumerator = FileSystemService.GetFilesWithoutExtendPropertyBy("", "", folderID, "", "", FileStates.NotSpecified, FileTypes.Folder, "", false, 0, 9999, out num).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					foreach (File current in FileSystemService.GetAllFilesInFolder(enumerator.Current.FileID))
					{
						filesWithoutExtendPropertyBy.Add(current);
					}
				}
			}
			return filesWithoutExtendPropertyBy;
		}
		private static IList<File> ReadExtendPropertiesOfFiles(IList<File> files)
		{
			for (int i = 0; i <= files.Count - 1; i++)
			{
				files[i] = FileSystemService.ReadExtendPropertiesOfFile(files[i]);
			}
			return files;
		}
		public static void AddFile(File file)
		{
			FileSystemService.fileGateway.AddNew(file);
		}
		public static File GetFileByID(string fileID)
		{
			return FileSystemService.fileGateway.GetFileByID(fileID);
		}
		public static File GetFileByIDWithExtendProperties(string fileID)
		{
			return FileSystemService.ReadExtendPropertiesOfFile(FileSystemService.GetFileByID(fileID));
		}
		public static File GetFileByQuickID(string quickID)
		{
			return FileSystemService.fileGateway.GetFileByQuickID(quickID);
		}
		public static void UpdateFile(File file)
		{
			FileSystemService.fileGateway.UpdateByPK(file);
		}
		public static IList<File> GetHotFiles(int pageIndex, int pageSize, out int recordCount)
		{
			return FileSystemService.ReadExtendPropertiesOfFiles(FileSystemService.fileGateway.GetHotFiles(pageIndex, pageSize, out recordCount));
		}
		public static IList<File> GetFilesBy(string ownerID, string keyword, string parentID, string startTime, string endTime, FileStates fileState, FileTypes fileType, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
		{
			return FileSystemService.ReadExtendPropertiesOfFiles(FileSystemService.fileGateway.GetFilesBy(ownerID, keyword, parentID, startTime, endTime, fileState, fileType, orderBy, desc, pageIndex, pageSize, out recordCount));
		}
		public static IList<File> GetFilesByCatIds(string ownerID, string keyword, string parentID, string startTime, string endTime, IList<string> catIds, FileStates fileState, FileTypes fileType, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
		{
			return FileSystemService.ReadExtendPropertiesOfFiles(FileSystemService.fileGateway.GetFilesByCatIds(ownerID, keyword, parentID, startTime, endTime, catIds, fileState, fileType, orderBy, desc, pageIndex, pageSize, out recordCount));
		}
		public static IList<File> GetFilesBy(string ownerID, string keyword, string parentID, string startTime, string endTime, FileStates fileStates, FileTypes fileType, bool displayInNotificationsOnly, bool publicReviewOnly, bool publicDownloadOnly, bool previewAfterPermitOnly, bool downloadAfterPermitOnly, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
		{
			return FileSystemService.ReadExtendPropertiesOfFiles(FileSystemService.fileGateway.GetFilesBy(ownerID, keyword, parentID, startTime, endTime, fileStates, fileType, displayInNotificationsOnly, publicReviewOnly, publicDownloadOnly, previewAfterPermitOnly, downloadAfterPermitOnly, orderBy, desc, pageIndex, pageSize, out recordCount));
		}
		public static IList<File> GetFilesWithoutExtendPropertyBy(string ownerID, string keyword, string parentID, string startTime, string endTime, FileStates fileStates, FileTypes fileType, bool displayInNotificationsOnly, bool publicReviewOnly, bool publicDownloadOnly, bool previewAfterPermitOnly, bool downloadAfterPermitOnly, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
		{
			return FileSystemService.fileGateway.GetFilesBy(ownerID, keyword, parentID, startTime, endTime, fileStates, fileType, displayInNotificationsOnly, publicReviewOnly, publicDownloadOnly, previewAfterPermitOnly, downloadAfterPermitOnly, orderBy, desc, pageIndex, pageSize, out recordCount);
		}
		public static IList<File> GetFilesWithoutExtendPropertyBy(string ownerID, string keyword, string parentID, string startTime, string endTime, FileStates fileState, FileTypes fileType, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
		{
			return FileSystemService.fileGateway.GetFilesBy(ownerID, keyword, parentID, startTime, endTime, fileState, fileType, orderBy, desc, pageIndex, pageSize, out recordCount);
		}
		public static bool ExistSameFileNameInFolder(string parentID, string filename, string excludeFileID)
		{
			return FileSystemService.fileGateway.ExistSameFileNameInFolder(parentID, filename, excludeFileID);
		}
		public static string CreateFolderIn(string ownerID, string parentFolderID, string newFolderID, string folderName)
		{
			return FileSystemService.CreateFolderWithQuickID(ownerID, parentFolderID, newFolderID, folderName, "");
		}
		public static string CreateFolderWithQuickID(string ownerID, string parentFolderID, string newFolderID, string folderName, string quickID)
		{
			File file = new File();
			file.ClientIP = "";
			file.ContentType = "";
			file.ConvertToPDFSuccessed = false;
			file.CreateTime = DateTime.Now;
			file.ExtendFileName = "";
			file.FileID = newFolderID;
			file.FileSize = 0L;
			file.FileState = FileStates.Normal;
			file.FileType = FileTypes.Folder;
			file.Keyword = "";
			file.Memo = "";
			file.OriginalFileName = folderName;
			file.OwnerID = ownerID;
			file.ParentID = parentFolderID;
			file.PDFAbsolutePath = "";
			file.PDFAbsoluteFileName = "";
			file.PDFRelativePath = "";
			file.StoreAbsolutePath = "";
			file.StoreRelativePath = "";
			file.StorageAbsoluteFileName = "";
			file.Tag = "";
			file.Title = folderName;
			file.TokenID = Guid.NewGuid().ToString();
			file.UploaderID = ownerID;
			file.QuickID = quickID;
			File fileByID = FileSystemService.GetFileByID(parentFolderID);
			if (fileByID != null)
			{
				file.DisplayInNotifications = fileByID.DisplayInNotifications;
				file.PublicDownload = fileByID.PublicDownload;
				file.PublicReview = fileByID.PublicReview;
				file.DownloadAfterPermit = fileByID.DownloadAfterPermit;
				file.PreviewAfterPermit = fileByID.PreviewAfterPermit;
			}
			string result;
			try
			{
				FileSystemService.fileGateway.AddNew(file);
				result = "SUCCESSED";
			}
			catch (Exception arg_157_0)
			{
				result = arg_157_0.Message;
			}
			return result;
		}
		public static string DeleteFile(string accountID, string fileID)
		{
			return "SUCCESSED";
		}
		public static string RenameFile(string accountID, string fileID, string newFolderName)
		{
			File fileByID = FileSystemService.fileGateway.GetFileByID(fileID);
			fileByID.OriginalFileName = newFolderName;
			string result;
			try
			{
				FileSystemService.fileGateway.UpdateByPK(fileByID);
				result = "SUCCESSED";
			}
			catch (Exception arg_26_0)
			{
				result = arg_26_0.Message;
			}
			return result;
		}
		public static IList<File> GetParentFolders(string currentFolderID)
		{
			IList<File> list = new List<File>();
			File fileByID;
			while ((fileByID = FileSystemService.fileGateway.GetFileByID(currentFolderID)) != null && fileByID.ParentID.Trim() != "UserRoot")
			{
				list.Add(fileByID);
				currentFolderID = fileByID.ParentID;
			}
			return list;
		}
		public static string GetFilePath(string fileID)
		{
			IList<File> parentFolders = FileSystemService.GetParentFolders(fileID);
			File fileByID = FileSystemService.GetFileByID(fileID);
			string text = string.Empty;
			for (int i = parentFolders.Count - 1; i >= 1; i--)
			{
				text = text + parentFolders[i].OriginalFileName + "/";
			}
			text += fileByID.OriginalFileName;
			return "//" + text;
		}
		public static File GetUserFolder(string accountID, string folder)
		{
			int num = 0;
			IList<File> filesBy = FileSystemService.fileGateway.GetFilesBy(accountID, "", folder, "", "", FileStates.NotSpecified, FileTypes.Folder, "", false, 0, 1, out num);
			if (filesBy.Count > 0)
			{
				return filesBy[0];
			}
			return null;
		}
		public static void MoveToFolder(string accountID, string fileID, string destFolderID)
		{
			File fileByID = FileSystemService.fileGateway.GetFileByID(fileID);
			if (fileByID != null)
			{
				fileByID.ParentID = destFolderID;
				FileSystemService.fileGateway.UpdateByPK(fileByID);
			}
		}
		public static string CopyToFolder(string accountID, string fileID, string destFolderID)
		{
			return "SUCCESSED";
		}
	}
}
