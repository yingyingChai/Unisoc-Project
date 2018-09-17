function guid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

function deleteFileFromAttachments(fileId) {
    var deleteUrl = "/Systems/Uploader/deleteAttachmentByID";
    PostForm(deleteUrl, "fileID=" + fileId, "", deleteSuccessed, communicateFailed);
}

function deleteSuccessed(result)
{
    var response = new Response(result);
    switch (response.Code)
    {
        case "FILE_DELETE_SUCCESSED":
            $("#fileItem_" + response.Tag).css("display", "none");

            break;
    }
}