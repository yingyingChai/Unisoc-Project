var recordCounts = 0;

var uploaderForRecall = null;
var uploaderForManualHold = null;

$(document).ready(function () {
    $.datetimepicker.setLocale('en');
   
    $('#txtCompletionDate').datetimepicker({
        lang: 'ch',
        timepicker: false,
        format: 'Y-m-d',
        formatDate: 'Y-m-d',
        minDate: '2016/01/01', // yesterday is minimum date
        maxDate: getNowFormatDate()// and tommorow is maximum date calendar

    });
  
});


function getNowFormatDate() {
    var date = new Date();
    var seperator1 = "-";

    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var strDate = date.getDate();
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (strDate >= 0 && strDate <= 9) {
        strDate = "0" + strDate;
    }
    var currentdate = year + seperator1 + month + seperator1 + strDate;
    return currentdate;
}


$(function () {
   
    $("#hidRecallCommentID").val(guid());
    var recallCommentID = $("#hidRecallCommentID").val();
    var lotID = $('.table.table-hover tbody tr.isclick').attr('id');
    var url = "/Systems/Uploader?lotID=" + lotID + "&commentID=" + recallCommentID;
    uploaderForRecall = CreateUploader("LotRecallUploadButton", "上传文件", "font-size:20px", "LotRecallUploadPrompt", url, LotRecalluploadFinished);

    $("#hidManualHoldCommentID").val(guid());
    var manualCommentID = $("#hidManualHoldCommentID").val();
    var lotID = $('.table.table-hover tbody tr.isclick').attr('id');
    var url = "/Systems/Uploader?lotID=" + lotID + "&commentID=" + manualCommentID;
    uploaderForManualHold = CreateUploader("ManualHoldUploadButton", "上传文件", "font-size:20px", "ManualHoldUploadUploadPrompt", url, ManualHoldUploadFinished);

}) 
function initializeRecallUploader()
{
    $("#hidRecallCommentID").val(guid());
    var recallCommentID = $("#hidRecallCommentID").val();
    var lotID = $('.table.table-hover tbody tr.isclick').attr('id');
    var url = "/Systems/Uploader?lotID=" + lotID + "&commentID=" + recallCommentID;
    uploaderForRecall.setUploadURL(url);
}

function LotRecalluploadFinished(result,serverData) {    
    $("#LotRecallUploadPrompt").html("");
    var innerHtml = $("#LotRecallAttachmentList").html();
    var response = new Response(serverData);
    switch (response.Code) {
        case "FILE_UPLOADED_SUCCESSED":
            innerHtml += "<li id=\"fileItem_" + response.Tag + "\"><span class=\"fa fa-paperclip\" style=\"margin-right:5px;\"></span>" + result.name + "<a class=\"deletefile\" href=\"javascript:deleteFileFromAttachments('" + response.Tag + "')\" style=\"margin-left:10px\")\">删除</a></li>";
            $("#LotRecallAttachmentList").html(innerHtml);
            break;
        default:
            alert(response.Message);
    }
}



function initializeManualHoldUploader() {
    $("#hidManualHoldCommentID").val(guid());
    var manualCommentID = $("#hidManualHoldCommentID").val();
    var lotID = $('.table.table-hover tbody tr.isclick').attr('id');
    var url = "/Systems/Uploader?lotID=" + lotID + "&commentID=" + manualCommentID;
    uploaderForManualHold.setUploadURL(url);

}

function ManualHoldUploadFinished(result,serverData) {
    $("#ManualHoldUploadUploadPrompt").html("");
    var innerHtml = $("#ManualHoldAttachmentList").html();
    var response = new Response(serverData);
    switch (response.Code) {
        case "FILE_UPLOADED_SUCCESSED":
            innerHtml += "<li id=\"fileItem_"+response.Tag+"\"><span class=\"fa fa-paperclip\" style=\"margin-right:5px;\"></span>" + result.name + "<a class=\"deletefile\" href=\"javascript:deleteFileFromAttachments('" + response.Tag + "')\" style=\"margin-left:10px\")\">删除</a></li>";
            $("#ManualHoldAttachmentList").html(innerHtml);
            break;
        default:
            alert(response.Message);
    }
}

var currentPage = 0;
var $_scope;
var $_http;
function removeHTMLTag(str) {
    var reTag = /<(?:.|\s)*?>/g;
    return str.replace(reTag, "");
}

function getAllLots(pageIndex) {

    $("#loadingBox").css("display", "block");
    $("#queryResult").css("display", "none");
    $("#noLotsFound").css("display", "none");
    $_http.get("/Lots/Query/Search?" + $("#frmQuery").serialize() + '&pageSize=15&pageIndex=' + pageIndex)
        .success(function (response) {
            $.each(response.rows, function (idx, obj) {
                obj.HoldReason = removeHTMLTag(obj.HoldReason);
            });
            $_scope.data = response;

            currentPage = $_scope.data.currentPage + 1;
            totalPages = $_scope.data.totalPages;
            $_scope.whatClassIsIt = function (lastDecision) {
                var result = "";
                switch (lastDecision) {
                    case 0:
                        result = "isRelease";
                        break;
                    case 1:
                        result = "isBin1Release";
                        break;
                    case 2:
                        result = "isRescreen";
                    case 3:
                        result = "isScrap";
                        break;
                    case 4:
                        result = "isPending";
                        break;
                    case 253:
                    case 254:
                        result = "isHold";
                        break;
                }
                return result;
            };

            $("#loadingBox").css("display", "none");
            $('#pager').html('');
            recordCounts=parseInt($_scope.data.rows.length);
            if (recordCounts > 0) {
                //否则显示数据
                var options = {
                    currentPage: currentPage,//当前页
                    totalPages: totalPages,//一共多少页
                    numberOfPages: 10//每次显示多少页                    
                }
                $('#pager').bootstrapPaginator(options);
                $('#custom-pager').show();
                $('#custom-pager .totalPages').html(totalPages?totalPages:0);
                $('#custom-pager .currentPage').html(currentPage?currentPage:0);
                $('#custom-pager .goPage').on('click', function () {
                    var currPageNum = $('#custom-pager .pageNum').val();
                    if (currPageNum > 0 && currPageNum <= totalPages) {
                        getAllLots(currPageNum - 1);
                    } else {
                        $('#custom-pager .pageNum').val('1');
                    }
                });
                $(".pagination a").click(function () {
                    getAllLots($(this).data("page") - 1);
                });
                $(".lotRows").css("display", "");
                $("#loadingLots").css("display", "none");
                $("#lotsNotFound").css("display", "none");
                $(".operate_buttons").attr("disabled", false);
                
            }
            else {
                //如果没有记录，则提示没有内容 
                $(".operate_buttons").attr("disabled","disabled");
                $(".lotRows").css("display", "none");
                $("#loadingLots").css("display", "none");
                $("#lotsNotFound").css("display", "");                
                
            }
            $(".btu_list input").attr("disabled", "disabled");
        });

}

app.controller('getAllLotTable', function ($scope, $http)
{
    $_http = $http;
    $_scope = $scope;
    getAllLots(0);

    $(function () {
        //双击单击
        //单击双击事件绑定开始
        var _time = null;
        $('.table.table-hover tbody').on('click', 'tr', function ($scope) {
            if (this.id == "lotsNotFound" || this.id == "loadingLots") return;
            var clicktr = $(this);
            clearTimeout(_time);
            _time = setTimeout(function ($scope) {
                //单击事件在这里
                $('.table.table-hover tbody tr').removeClass('isclick');
                if (!$('.table.table-hover').hasClass('isedit') || !$(clicktr).hasClass('isclick')) {
                    $(clicktr).addClass('isclick');
                    btnDisabled(clicktr);
                }
            }, 300);
        });
        $('.table.table-hover tbody').on('dblclick', 'tr', function ($scope) {
            if (this.id == "lotsNotFound" || this.id=="loadingLots") return;
            clearTimeout(_time);
            //双击代码事件在此
            if ($('.table.table-hover').hasClass('table-query')) {
                //window.location.href = "/Lots/Details?LotID=" + $(this).attr('id');
                window.open("/Lots/Details?LotID=" + $(this).attr('id'));
            }
        });

        function btnDisabled(tr) {
            $(".btu_list input").attr("disabled", "disabled");
            $(".btu_list input.btnComment").removeAttr("disabled");
            $(".btu_list input.btnConfirm").removeAttr("disabled");
            var Role = $("#hidCurrentUserRole").val();
            var AutoJudgeResult = $(tr).data('autojudgeresult');
            var ManualHold = $(tr).data('manualhold');
            var QADecision = $(tr).data('qa');
            var PEDecision = $(tr).data('pe');
            var SPRDDecision = $(tr).data('sprd');
            var lastDecision = $(tr).data('lastdecision');
            
            /*255	NORMAL
            254	HOLD
            253	MANUAL HOLD
            0	RELEASE
            1	Bin1 Release
            2	Rescreen
            3	Scrap
            4	Pending*/

            if (lastDecision != 254 && lastDecision != 253)
            {
                $(".btu_list input.btnManualHold").removeAttr("disabled");
            }

            if (lastDecision != 255)
            {
                if (((Role === "PE" || Role === "PEAdmin") && (PEDecision === '' || PEDecision==='Pending')) || ((Role === "QA" || Role === "QAAdmin") && (QADecision === '' || QADecision==='Pending'))) {
                    $(".btu_list input.btnDispose").removeAttr("disabled");
                }
            }

            if (SPRDDecision != '' || QADecision != '' || PEDecision != '' || ManualHold==true) {
                $(".btu_list input.btnRecall").removeAttr("disabled");
            }
        }


        //选中查看
        $('#url-comment,#url-dispose,#url-confirm').click(function () {
            var isclickcount = $('.table.table-hover tbody tr.isclick').length;
            if (isclickcount > 1) {
                $('#comment1').modal('show');
            } else if (isclickcount === 1) {
                window.location.href = "/Lots/Details?LotID=" + $('.table.table-hover tbody tr.isclick').attr('id');
            }
            else if (isclickcount === 0) {
                $('#comment0').modal('show');
            }
            else {
                $('#comment').modal('show');
            }
        });

        //单击双击事件绑定结束
        //提交查询事件
        $("#QueryTr select").change(function () {
            getAllLots(0);
        });

        $('#QueryTr input').keyup(function () {
            getAllLots(0);
        });

        $('#QueryTr input').change(function () {
            getAllLots(0);
        });

        $('#QueryTrSubmit').click(function () {
        	getAllLots(0);
        });

        $('#btnManualHoldLot').click(function () {
            if ($('.table.table-hover').hasClass('table-query')) {
                var lotID=$('.table.table-hover tbody tr.isclick').attr('id');
                var holdReason = $('#Manual_HoldReason').val();
                var manualHoldCommentID = $("#hidManualHoldCommentID").val();
                $.post("/Lots/Dispose/ManualHold", { lotID: lotID, HoldReason: holdReason, commentID: manualHoldCommentID }, function (data) {
                    getAllLots(0);
                });
            }
        });

        $('#btnSetAllReaded').click(
            function ()
            {
                $.post("/Lots/Query/SetAllReaded", function (data) {
                    getAllLots(0);
                    try {
                        
                        //$('#Notification').css("display","none");
                        //$('#WaitForConfirm').css("display","none");
                        //$('#WaitForDispose').css("display","none");
                        $('#NewComments').css("display","none");
                        //$('#WaitForOtherBinDispose').css("display","none");
                    }
                    catch (ex) {
                        //do nothing 
                    }


                });
            }
        );


        $('#btnExportLots').click(
            function () {
                if (recordCounts > 0) {
                    $.post("/Export/ExportLots?" + $("#frmQuery").serialize(), function (data) {
                        var response = new Response(data);
                        if (response.Code == 0) {
                            response.Next();
                        }
                        else {
                            alert(response.Message);
                        }
                    });
                }
                else {
                    alert(" Not Lots to export.")
                }
            }
        );


        $('#btnRecallLot').click(function () {
            if ($('.table.table-hover').hasClass('table-query')) {
                var lotID= $('.table.table-hover tbody tr.isclick').attr('id');
                var commentID = $("#hidRecallCommentID").val();
                var comment= $("#txtRecallReason").val();
                $.post("/Lots/Dispose/Recall", { lotID: lotID, commentID: commentID, comment: comment }, function (data) {                    
                    initializeRecallUploader()
                    getAllLots(0);
                });
            }
        });

        $('.btu_list input[type=submit]').click(function () {
            if ($('.table.table-hover').hasClass('table-query')) {
                var selectedRowCount = $('.table.table-hover tbody tr.isclick').length;
                switch (selectedRowCount) {
                    case 0:
                        $('#comment0').modal('show');
                        break;
                    case 1:
                        $('#' + $(this).data('type')).modal('show');
                        break;
                    default:
                        $('#comment').modal('show');
                        break;
                }
            }
        });
        
        //排序
        $('.table-query thead th').click(function () {
            var orderby = $(this).data("orderby");
            var desc = $(this).data("desc");
            var html = $(this).html();
            if (orderby != null)
            {
                $('.table-query th span').html("")
                if (desc)
                {
                    $(this).data("desc", false);
                    $(this).children('span').html("&darr;");
                }
                else
                {
                    $(this).data("desc", true);
                    $(this).children('span').html("&uarr;");
                }
                $('#orderBy').val(orderby);
                $('#desc').val(desc);
                getAllLots(0);
            }
        });

    })


});