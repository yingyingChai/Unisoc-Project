var lotID = $("#lotID").val();
var $_scope;
var $_http;
var decisionChanged = false;
var uploadForOtherBinDispose = null;
var uploadForComment = null;
var pending = false;
$(document).ready(function () {
    $("#btnSubmit").attr("disabled", true);
    //initializeCommentAttachmentUploader();
    //initializeOtherBinDisposeAttachmentUploader();
    //PE和QA决策后的自动生成SPRD Decision
    $("#PEDispose").change(function () {
        DisplaySPRDDecision();
        hideOrShowOtherBinDispose(parseInt($(this).val()));
        $("#hidPEDispose").val($(this).val());
    });

    $("#QADispose").change(function () {
        DisplaySPRDDecision();
    });

    $("#chkOtherBinDispose").change(function () {
        if ($(this).is(":checked")) {
            $("#divOtherBinDispose").modal("show");
        }
    });

    $('#btnSubmit').click(function () {
        saveCommentOrDecision();
        $("#btnSubmit").attr("disabled", "true");
    });

    $("#othercheck").change(function () {
        if ($(this).is(":checked")) {
            $("#divOtherBinDispose").modal("show");
        }
    });

    $("#saveOtherBinDispose").click(function () {
        $("#hidPEDispose").val($("#PEDispose").val());
        saveOtherBinDispose();
    });

    $("#btnCancelOtherBinDispose").click(function () {
        $("#chkOtherBinDispose").attr("checked", false);
    });




    $("#cancelOtherBinDispose").click(function () {
        $("#chkOtherBinDispose").attr("checked", false);
    });

    $("#btnExportSwbins").click(
         function () {
             $.post("/Export/ExportSwbin?" + $("#frmDetails").serialize(), function (data) {
                 var response = new Response(data);
                 if (response.Code == 0) {
                     response.Next();
                 }
                 else {
                     alert(response.Message);
                 }
             });
         }
    );

    $(".chkOSATConfirmOptions").click(
        function () {
            $("#hidOSATConfirmed").val("1");
            decisionChanged = true;
            switchSubmitButton();
        }
    );

    $('#txtComment').keyup(function () {
        switchSubmitButton();
    });

    $("#hidOtherBinDisposeCommentID").val(guid());
    var otherBinDisposeCommentID = $("#hidOtherBinDisposeCommentID").val();
    var urlOtherBinDispose = "/Systems/Uploader?lotID=" + lotID + "&commentID=" + otherBinDisposeCommentID;
    uploadForOtherBinDispose = CreateUploader("OtherBinReleaseUploadButton", "上传文件", "font-size:20px", "OtherBinReleaseUploadPrompt", urlOtherBinDispose, OtherBinDisposeUploadFinished);

    $("#hidNewCommentID").val(guid());
    var commentID = $("#hidNewCommentID").val();
    var url = "/Systems/Uploader?lotID=" + lotID + "&commentID=" + commentID;
    uploadForComment = CreateUploader("uploadButton", "上传文件", "font-size:20px", "uploadPrompt", url, uploadFinished, uploadComplete);

    $("#SpanHoldReason").html(htmlDecodeByRegExp(_holdreason));
});

function htmlDecodeByRegExp(str) {
    var s = "";
    if (str.length == 0) return "";
    s = str.replace(/&amp;/g, "&");
    s = s.replace(/&lt;/g, "<");
    s = s.replace(/&gt;/g, ">");
    s = s.replace(/&nbsp;/g, " ");
    s = s.replace(/&#39;/g, "\'");
    s = s.replace(/&quot;/g, "\"");
    return s;
}
function initializeOtherBinDisposeAttachmentUploader() {
    //初始化Other Bin dispose的CommentID和上传组件
    $("#hidOtherBinDisposeCommentID").val(guid());
    var otherBinDisposeCommentID = $("#hidOtherBinDisposeCommentID").val();
    var urlOtherBinDispose = "/Systems/Uploader?lotID=" + lotID + "&commentID=" + otherBinDisposeCommentID;
}



function initializeCommentAttachmentUploader() {
    //初始化CommentID和上传组件
    $("#hidNewCommentID").val(guid());
    var commentID = $("#hidNewCommentID").val();
    var url = "/Systems/Uploader?lotID=" + lotID + "&commentID=" + commentID;
    uploadForComment.setUploadURL(url);
}

function uploadFinished(result, serverData) {
    $("#uploadPrompt").html("");
    var innerHtml = $("#attachmentList").html();
    var response = new Response(serverData);
    switch (response.Code) {
        case "FILE_UPLOADED_SUCCESSED":
            innerHtml += "<li id=\"fileItem_" + response.Tag + "\"><span class=\"fa fa-paperclip\" style=\"margin-right:5px;\"></span>" + result.name + "<a class=\"deletefile\" href=\"javascript:deleteFileFromAttachments('" + response.Tag + "')\" style=\"margin-left:10px\")\">删除</a></li>";
            $("#attachmentList").html(innerHtml);
            break;
        default:
            alert(response.Message);
    }
}

function OtherBinDisposeUploadFinished(result, serverData) {

    $("#OtherBinReleaseUploadPrompt").html("");
    var innerHtml = $("#OtherBinReleaseAttachmentList").html();
    var response = new Response(serverData);
    switch (response.Code) {
        case "FILE_UPLOADED_SUCCESSED":
            innerHtml += "<li id=\"fileItem_" + response.Tag + "\"><span class=\"fa fa-paperclip\" style=\"margin-right:5px;\"></span>" + result.name + "<a class=\"deletefile\" href=\"javascript:deleteFileFromAttachments('" + response.Tag + "')\" style=\"margin-left:10px\")\">删除</a></li>";
            $("#OtherBinReleaseAttachmentList").html(innerHtml);
            break;
        default:
            alert(response.Message);
    }
}

function switchSubmitButton() {
    if (decisionChanged || $.trim($("#txtComment").val()).length > 0 || $("#hidOSATConfirmed").val() == "1") {
        $("#btnSubmit").attr("disabled", false);
    }
    else {
        $("#btnSubmit").attr("disabled", true);
    }
}

function DisplaySPRDDecision() {
    decisionChanged = false;
    var QADispose = parseInt($("#QADispose").val());
    var PEDispose = parseInt($("#PEDispose").val());
    var role = $("#hidRole").val().toUpperCase();
    switch (role) {
        case "QA":
        case "QAADMIN":
            decisionChanged = (QADispose != -1);
            pending = (QADispose == 4);
            break;
        case "PE":
        case "PEADMIN":
            decisionChanged = (PEDispose != -1);
            pending = (PEDispose == 4);
            break;
    }
    switchSubmitButton();
    var larger = QADispose;
    if (PEDispose >= QADispose) larger = PEDispose;
    var displayText = "";
    switch (larger) {
        case 0:
            displayText = "Release";
            break;
        case 1:
            displayText = "Bin1 Release";
            break;
        case 2:
            displayText = "Rescreen";
            break;
        case 3:
            displayText = "Scrap";
            break; zz
        case 4:
            displayText = "Pending";
            break;
    }
    $("#SPRDDecisionText").html(displayText);
}

var app = angular.module('myApp', ['ngSanitize']);

app.controller('getCommentList', function ($scope, $http) {
    $_scope = $scope;
    $_http = $http;
    getComment(0);
});


function getComment(pageIndex) {
    $_http.post("/Lots/Comment/getCommentList?lotID=" + lotID + "&pageIndex=" + pageIndex)
            .success(function (response) {
                $_scope.data = response;
                currentPage = $_scope.data.currentPage + 1;
                totalPages = $_scope.data.totalPages;
                if (parseInt($_scope.data.rows.length) > 0) {
                    //分页功能
                    var pagerOptions = {
                        currentPage: currentPage,//当前页
                        totalPages: totalPages,//一共多少页
                        numberOfPages: 5,//每次显示多少页
                        pageUrl: function (type, page, current) {
                            return "javascript:void(0);";
                        }
                    }
                    $('#pagerComment').bootstrapPaginator(pagerOptions);
                    $("#pagerComment a").click(function () {
                        getComment($(this).data("page") - 1);
                    });
                }
            });
};

function saveCommentOrDecision() {
    //没有Decision,仅仅有Comment的情况
    if ($.trim($("#txtComment").val()).length > 0 && !decisionChanged) {
        $('#btnSubmit').val('Saving...');
        $.post("/Lots/Comment/AddComment", $("#frmDetails").serialize()).success(function () {
            $("#txtComment").val("");
            initializeCommentAttachmentUploader();
            getComment(0);
            $('#btnSubmit').val('Submit');
        });
    }

    //有Decision, 但可能有，也可能没有Comment的情况
    if (decisionChanged) {
        $('#btnSubmit').val('Saving...');
        var role = $("#hidRole").val().toUpperCase();
        $.post("/Lots/Dispose/SaveDecision", $("#frmDetails").serialize()).success(function (serverResult) {
            if ((role == "QA" || role == "QAADMIN") && !pending) {
                $("#QADispose").attr("disabled", true);
            }

            if ((role == "PE" || role == "PEADMIN") && !pending) {
                $("#PEDispose").attr("disabled", true);
            }

            if (role == "OSAT" || role == "OSATADMIN") {
                $(".chkOSATConfirmOptions").attr("disabled", true);
            }

            getComment(0);
            initializeCommentAttachmentUploader();

            $('#btnSubmit').val('Submit');

            var response = new Response(serverResult);
            $("#lblStatus").html(response.Tag);


        });
        decisionChanged = false;
    }
    $("#UploadTip").html("");
    $("#attachmentList").html("");
}

function hideOrShowOtherBinDispose(peDispose) {
    var qaDispose = parseInt($("#QADispose").val())
    if (peDispose == 1 || (qaDispose == 1 && peDispose == 0)) {
        $("#otherBinDisposeArea").css("display", "block");
    }
    else {
        $("#otherBinDisposeArea").css("display", "none");
        $("#chkOtherBinDispose").removeAttr("checked");
    }
}

//保存Other Bin dispose
function saveOtherBinDispose() {
    var peDispose = $("#PEDispose").val();
    var textarea = $("#txtOtherBinDisposeComment").val();

    if (textarea === null || textarea === "") {
        alert("Please input comment.");
        $("#chkOtherBinDispose").attr("checked", false);
        return;
    }
    else {
        $.post("/Lots/Dispose/doOtherBinDispose", $("#frmOtherBinDispose").serialize()).success(function (result) {
            var response = new Response(result);
            if (response.Code == "0") {
                $("#hidOtherBinDisposeDone").val("DONE");
                $("#chkOtherBinDispose").attr("checked", true);
                $("#chkOtherBinDispose").attr("disabled", true);
                $("#PEDispose").val(peDispose);
                $("#PEDispose").attr("disabled", true);
                getComment(0);
                $("#lblStatus").html(response.Tag);
            }
            else {
                $("#chkOtherBinDispose").attr("checked", false);
                $("PEDispose").val(1);
                $("#chkOtherBinDispose").attr("disabled", false);
            }
            $("#divOtherBinDispose").modal("hide");
        });
    }
}


app.controller('getSWBins', function ($scope, $http) {
    function getSWBin(pageIndex) {
        $http.post("/Lots/SWBin/getSWBin?lotID=" + lotID + "&pageSize=15&pageIndex=" + pageIndex + "&isPassed=" + $('#inputSBLType').val() + "&limited=" + $('#inputLimits').val() + "&code=" + $('#inputCode').val() + "&defect=" + $('#inputDefect').val() + "&qty=" + $('#inputQty').val() + "&failRate=" + $('#inputFailRate').val() + "&orderBy=" + $('#orderBy').val() + "&desc=" + $('#desc').val())
            .success(function (response) {
                $scope.SWdata = response;
                currentPage = $scope.SWdata.currentPage + 1;
                totalPages = $scope.SWdata.totalPages;
                if (parseInt($scope.SWdata.rows.length) > 0) {
                    //分页功能
                    var SWBin = {
                        currentPage: currentPage,//当前页
                        totalPages: totalPages,//一共多少页
                        numberOfPages: 5,//每次显示多少页
                        pageUrl: function (type, page, current) {
                            return "javascript:void(0);";
                        }
                    }
                    $('#swbinexample').bootstrapPaginator(SWBin);
                    $('#custom-pager').show();
                    $('#custom-pager .totalPages').html(totalPages ? totalPages : 0);
                    $('#custom-pager .currentPage').html(currentPage ? currentPage : 0);
                    $('#custom-pager .goPage').on('click', function () {
                        var currPageNum = $('#custom-pager .pageNum').val();
                        if (currPageNum > 0 && currPageNum <= totalPages) {
                            getSWBin(currPageNum - 1);
                        } else {
                            $('#custom-pager .pageNum').val('1');
                        }
                    });
                    $("#swbinexample a").click(function () {
                        getSWBin($(this).data("page") - 1);
                    });
                }
            });
    }

    $("#chkConfirmOtherBinDispose").click(
       function () {
           if ($("#chkBin1Release")[0].checked == false) {
               alert("You have to confirm Bin1 Release first.");
               $("#chkConfirmOtherBinDispose").attr("checked", false);
           }
       }

   );


    $('#btnCollapseSWBin').click(function () {
        $('#swbinsInput input').val('');
        $('#collapseSWBin').collapse('toggle');
        var ariaExpanded = $('#collapseSWBin').attr('aria-expanded');
        if (ariaExpanded === 'false') $('#btnCollapseSWBin span').html('+');
        if (ariaExpanded === 'true') {
            $('#btnCollapseSWBin span').html('-');
            getSWBin(0);
        }
    });
    $('#swbinsInput input').keyup(function () {
        getSWBin(0);
    });
    //排序
    $('#webinOrderby th').click(function () {
        var orderby = $(this).data("orderby");
        var desc = $(this).data("desc");
        var html = $(this).html();
        if (orderby != null) {
            $('#webinOrderby th span').html("")
            if (desc) {
                $(this).data("desc", false);
                $(this).children('span').html("&darr;");
            } else {
                $(this).data("desc", true);
                $(this).children('span').html("&uarr;");
            }
            $('#orderBy').val(orderby);
            $('#desc').val(desc);
            getSWBin(0);
        }
    });
});