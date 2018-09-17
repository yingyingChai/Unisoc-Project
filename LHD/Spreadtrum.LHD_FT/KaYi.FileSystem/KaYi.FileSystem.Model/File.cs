using System;
namespace KaYi.FileSystem.Model
{
	public class File
	{
		private string _FileID = string.Empty;
		private string _ParentID = string.Empty;
		private FileTypes _FileType;
		private string _TokenID = string.Empty;
		private DateTime _CreateTime;
		private string _OriginalFileName = string.Empty;
		private string _ExtendFileName = string.Empty;
		private string _Title = string.Empty;
		private string _Keyword = string.Empty;
		private string _Memo = string.Empty;
		private bool _ConvertToPDFSuccessed = true;
		private string _Tag = string.Empty;
		private FileStates _FileState;
		private long _FileSize;
		private string _ClientIP = string.Empty;
		private string _ContentType = string.Empty;
		private string _UploaderID = string.Empty;
		private string _OwnerID = string.Empty;
		private string _RelativeProjectNames = string.Empty;
		private string _RelativeUserNames = string.Empty;
		private string _FullPathForDisplay = string.Empty;
		private string thumbnail = string.Empty;
		private string quickID = string.Empty;
		private string categoryQueryIDs = string.Empty;
		private int payLevel;
		private string largeThumbnail = string.Empty;
		private int secretLevel;
		private int totalFiles;
		private int totalFolders;
		private long totalSize;
		private int sequence;
		private string _StoreAbsolutePath = string.Empty;
		private string _StorageAbsoluteFileName = string.Empty;
		private string _StoreRelativePath = string.Empty;
		private string _PDFAbsolutePath = string.Empty;
		private string _PDFAbsoluteFileName = string.Empty;
		private string _PDFRelativePath = string.Empty;
		private bool displayInNotifications;
		private bool publicReview;
		private bool publicDownload;
		private bool previewAfterPermit;
		private bool downloadAfterPermit;
		private int downloadTimes;
		private int readCount;
		private string ownerName = string.Empty;
		private int openedTimes;
		public string OwnerName
		{
			get
			{
				return this.ownerName;
			}
			set
			{
				this.ownerName = value;
			}
		}
		public int ReadCount
		{
			get
			{
				return this.readCount;
			}
			set
			{
				this.readCount = value;
			}
		}
		public int DownloadTimes
		{
			get
			{
				return this.downloadTimes;
			}
			set
			{
				this.downloadTimes = value;
			}
		}
		public int OpenedTimes
		{
			get
			{
				return this.openedTimes;
			}
			set
			{
				this.openedTimes = value;
			}
		}
		public bool DisplayInNotifications
		{
			get
			{
				return this.displayInNotifications;
			}
			set
			{
				this.displayInNotifications = value;
			}
		}
		public bool PublicReview
		{
			get
			{
				return this.publicReview;
			}
			set
			{
				this.publicReview = value;
			}
		}
		public bool PublicDownload
		{
			get
			{
				return this.publicDownload;
			}
			set
			{
				this.publicDownload = value;
			}
		}
		public bool PreviewAfterPermit
		{
			get
			{
				return this.previewAfterPermit;
			}
			set
			{
				this.previewAfterPermit = value;
			}
		}
		public bool DownloadAfterPermit
		{
			get
			{
				return this.downloadAfterPermit;
			}
			set
			{
				this.downloadAfterPermit = value;
			}
		}
		public string LargeThumbnail
		{
			get
			{
				return this.largeThumbnail;
			}
			set
			{
				this.largeThumbnail = value;
			}
		}
		public int PayLevel
		{
			get
			{
				return this.payLevel;
			}
			set
			{
				this.payLevel = value;
			}
		}
		public int SecretLevel
		{
			get
			{
				return this.secretLevel;
			}
			set
			{
				this.secretLevel = value;
			}
		}
		public string CategoryQueryIDs
		{
			get
			{
				return this.categoryQueryIDs;
			}
			set
			{
				this.categoryQueryIDs = value;
			}
		}
		public string QuickID
		{
			get
			{
				return this.quickID;
			}
			set
			{
				this.quickID = value;
			}
		}
		public int TotalFiles
		{
			get
			{
				return this.totalFiles;
			}
			set
			{
				this.totalFiles = value;
			}
		}
		public int TotalFolders
		{
			get
			{
				return this.totalFolders;
			}
			set
			{
				this.totalFolders = value;
			}
		}
		public long TotalSize
		{
			get
			{
				return this.totalSize;
			}
			set
			{
				this.totalSize = value;
			}
		}
		public int Sequence
		{
			get
			{
				return this.sequence;
			}
			set
			{
				this.sequence = value;
			}
		}
		public string Thumbnail
		{
			get
			{
				return this.thumbnail;
			}
			set
			{
				this.thumbnail = value;
			}
		}
		public string FullPathForDisplay
		{
			get
			{
				return this._FullPathForDisplay;
			}
			set
			{
				this._FullPathForDisplay = value;
			}
		}
		public string RelativeProjectNames
		{
			get
			{
				return this._RelativeProjectNames;
			}
			set
			{
				this._RelativeProjectNames = value;
			}
		}
		public string RelativeUserNames
		{
			get
			{
				return this._RelativeUserNames;
			}
			set
			{
				this._RelativeUserNames = value;
			}
		}
		public string OwnerID
		{
			get
			{
				return this._OwnerID;
			}
			set
			{
				this._OwnerID = value;
			}
		}
		public string FileID
		{
			get
			{
				return this._FileID;
			}
			set
			{
				this._FileID = value;
			}
		}
		public string ParentID
		{
			get
			{
				return this._ParentID;
			}
			set
			{
				this._ParentID = value;
			}
		}
		public FileTypes FileType
		{
			get
			{
				return this._FileType;
			}
			set
			{
				this._FileType = value;
			}
		}
		public string TokenID
		{
			get
			{
				return this._TokenID;
			}
			set
			{
				this._TokenID = value;
			}
		}
		public DateTime CreateTime
		{
			get
			{
				return this._CreateTime;
			}
			set
			{
				this._CreateTime = value;
			}
		}
		public string OriginalFileName
		{
			get
			{
				return this._OriginalFileName;
			}
			set
			{
				this._OriginalFileName = value;
			}
		}
		public string ExtendFileName
		{
			get
			{
				return this._ExtendFileName;
			}
			set
			{
				this._ExtendFileName = value;
			}
		}
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				this._Title = value;
			}
		}
		public string Keyword
		{
			get
			{
				return this._Keyword;
			}
			set
			{
				this._Keyword = value;
			}
		}
		public string Memo
		{
			get
			{
				return this._Memo;
			}
			set
			{
				this._Memo = value;
			}
		}
		public bool ConvertToPDFSuccessed
		{
			get
			{
				return this._ConvertToPDFSuccessed;
			}
			set
			{
				this._ConvertToPDFSuccessed = value;
			}
		}
		public string Tag
		{
			get
			{
				return this._Tag;
			}
			set
			{
				this._Tag = value;
			}
		}
		public FileStates FileState
		{
			get
			{
				return this._FileState;
			}
			set
			{
				this._FileState = value;
			}
		}
		public long FileSize
		{
			get
			{
				return this._FileSize;
			}
			set
			{
				this._FileSize = value;
			}
		}
		public string ClientIP
		{
			get
			{
				return this._ClientIP;
			}
			set
			{
				this._ClientIP = value;
			}
		}
		public string ContentType
		{
			get
			{
				return this._ContentType;
			}
			set
			{
				this._ContentType = value;
			}
		}
		public string UploaderID
		{
			get
			{
				return this._UploaderID;
			}
			set
			{
				this._UploaderID = value;
			}
		}
		public string StoreAbsolutePath
		{
			get
			{
				return this._StoreAbsolutePath;
			}
			set
			{
				this._StoreAbsolutePath = value;
			}
		}
		public string StorageAbsoluteFileName
		{
			get
			{
				return this._StorageAbsoluteFileName;
			}
			set
			{
				this._StorageAbsoluteFileName = value;
			}
		}
		public string StoreRelativePath
		{
			get
			{
				return this._StoreRelativePath;
			}
			set
			{
				this._StoreRelativePath = value;
			}
		}
		public string PDFAbsolutePath
		{
			get
			{
				return this._PDFAbsolutePath;
			}
			set
			{
				this._PDFAbsolutePath = value;
			}
		}
		public string PDFAbsoluteFileName
		{
			get
			{
				return this._PDFAbsoluteFileName;
			}
			set
			{
				this._PDFAbsoluteFileName = value;
			}
		}
		public string PDFRelativePath
		{
			get
			{
				return this._PDFRelativePath;
			}
			set
			{
				this._PDFRelativePath = value;
			}
		}
	}
}
