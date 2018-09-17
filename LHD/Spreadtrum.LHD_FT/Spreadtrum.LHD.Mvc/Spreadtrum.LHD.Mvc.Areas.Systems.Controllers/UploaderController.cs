namespace Spreadtrum.LHD.Mvc.Areas.Systems.Controllers
{
    using KaYi.FileSystem.Model;
    using KaYi.FileSystem.Services;
    using KaYi.Utilities;
    using KaYi.Web.Infrastructure.Model.System.HandlerResponses;
    using Spreadtrum.LHD.Mvc.Areas.Shared;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Web;

    public class UploaderController : BaseController
    {
        public string deleteAttachmentByID()
        {
            HandlerResponse response = null;
            ResponseTypes tip;
            TipTypes information;
            string str = base.Request.QueryString["fileID"];
            if (StringHelper.isNullOrEmpty(str))
            {
                tip = ResponseTypes.Tip;
                information = TipTypes.Information;
                response = new HandlerResponse("FILE_ID_EMPTY", str, tip.ToString(), information.ToString(), "", "", "");
                base.Response.Write(response.GenerateJsonResponse());
                return null;
            }
            KaYi.FileSystem.Model.File fileByID = FileSystemService.GetFileByID(str);
            if (fileByID != null)
            {
                fileByID.FileState = FileStates.Deleted;
                FileSystemService.UpdateFile(fileByID);
                tip = ResponseTypes.Tip;
                information = TipTypes.Information;
                response = new HandlerResponse("FILE_DELETE_SUCCESSED", fileByID.OriginalFileName, tip.ToString(), information.ToString(), "", "", fileByID.FileID);
                base.Response.Write(response.GenerateJsonResponse());
                return null;
            }
            tip = ResponseTypes.Tip;
            information = TipTypes.Information;
            response = new HandlerResponse("FILE_NOT_FOUND", str, tip.ToString(), information.ToString(), "", "", "");
            base.Response.Write(response.GenerateJsonResponse());
            return null;
        }

        public string Index()
        {
            ResponseTypes tip;
            TipTypes information;
            string str = ConfigurationManager.AppSettings["StorageRelativePath"];
            string str2 = ConfigurationManager.AppSettings["StorageAbsolutePath"];
            HandlerResponse response = null;
            HttpPostedFileBase base2 = base.Request.Files[0];
            string fileName = base2.FileName;
            string parentID = base.Request.QueryString["CommentID"];
            int recordCount = 0;
            using (IEnumerator<KaYi.FileSystem.Model.File> enumerator = FileSystemService.GetFilesBy("", "", parentID, "", "", FileStates.Normal, FileTypes.File, "", false, 0, 0x270f, out recordCount).GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.OriginalFileName.ToUpper() == fileName.ToUpper())
                    {
                        tip = ResponseTypes.Tip;
                        information = TipTypes.Information;
                        response = new HandlerResponse("FILE_ALREADY_EXIST", string.Format("文件{0}已存在", fileName), tip.ToString(), information.ToString(), "", "", "");
                        base.Response.Write(response.GenerateJsonResponse());
                        return null;
                    }
                }
            }
            if (((base.Request.RequestType == "POST") && (base.Request.Files.Count > 0)) && (!StringHelper.isNullOrEmpty(base.Request.QueryString["LotID"]) && !StringHelper.isNullOrEmpty(base.Request.QueryString["CommentID"])))
            {
                string str6 = base.Request.QueryString["LotID"];
                parentID = base.Request.QueryString["CommentID"];
                DateTime now = DateTime.Now;
                KaYi.FileSystem.Model.File file = new KaYi.FileSystem.Model.File();
                string str7 = Guid.NewGuid().ToString();
                file.ContentType = base2.ContentType;
                file.ConvertToPDFSuccessed = false;
                file.CreateTime = DateTime.Now;
                file.ExtendFileName = FileService.GetExtendFileName(base2.FileName);
                file.FileID = str7;
                file.FileSize = base2.ContentLength;
                file.FileState = FileStates.Normal;
                file.FileType = FileTypes.File;
                file.FullPathForDisplay = "";
                file.Keyword = "";
                file.Memo = "";
                file.OriginalFileName = base2.FileName;
                file.OwnerID = str6;
                file.ParentID = parentID;
                file.PDFAbsolutePath = "";
                file.PDFAbsoluteFileName = "";
                file.PDFRelativePath = "";
                file.RelativeProjectNames = "";
                file.RelativeUserNames = "";
                file.StoreAbsolutePath = string.Format(@"{0}{1}\{2}\", str2, str6, parentID);
                file.StorageAbsoluteFileName = file.StoreAbsolutePath + @"\" + file.OriginalFileName;
                file.StoreRelativePath = string.Format(@"{0}{1}\{2}\", str, str6, parentID);
                file.StoreRelativePath = file.StoreRelativePath.Replace('\\', '/');
                file.Tag = "Unconfirmed";
                file.Title = "";
                file.TokenID = Guid.NewGuid().ToString();
                file.UploaderID = BaseController.CurrentUserInfo.UserID;
                Directory.CreateDirectory(file.StoreAbsolutePath);
                base2.SaveAs(file.StorageAbsoluteFileName);
                FileSystemService.AddFile(file);
                tip = ResponseTypes.Tip;
                information = TipTypes.Information;
                response = new HandlerResponse("FILE_UPLOADED_SUCCESSED", file.OriginalFileName, tip.ToString(), information.ToString(), "", "", file.FileID);
                base.Response.Write(response.GenerateJsonResponse());
                return "";
            }
            tip = ResponseTypes.Tip;
            information = TipTypes.Information;
            response = new HandlerResponse("FILE_UPLOAD_FAILED", "文件上传错误", tip.ToString(), information.ToString(), "", "", "");
            base.Response.Write(response.GenerateJsonResponse());
            return "";
        }
    }
}

