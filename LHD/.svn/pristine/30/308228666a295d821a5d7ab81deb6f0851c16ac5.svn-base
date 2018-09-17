var lotID = $("#hidid").val();
var currentPage = 0;
var $_http;
var $_scope;
var id = $("#hidid").val();
var uploadForComment = null;
var recordCount = 0;
var app = angular.module('myApp', ['ngSanitize']);
function GetWafer(pageIndex) {
    $_http.get("/lots/wafer/WaferSbin?" + $("#frmDetails").serialize() + "&pageSize=10&pageIndex=" + pageIndex)
        .success(function (res) {
            $_scope.waferdata = res;
            currentPage = $_scope.waferdata.currentPage + 1;
            totalPages = $_scope.waferdata.totalPages;
            recordCount = parseInt($_scope.waferdata.rows.length);
            if (recordCount > 0) {
                $_scope.HoldClass = function (pedispose, qadispose) {
                    var result = "";
                    if ((pedispose == hold || qadispose == hold) || (pedispose == rma || qadispose == rma) || (pedispose == rma || qadispose == hold)) {
                        result = "isHold";
                    }
                    return result;
                };
                var options = {
                    currentPage: currentPage,//当前页
                    totalPages: totalPages,//一共多少页
                    numberOfPages: 5,//每次显示多少页
                    pageUrl: function (type, page, current) {
                        return "javascript:void(0);";
                    }
                }
                $('#waferexample').bootstrapPaginator(options);
                $('#custom-pager').show();
                $('#custom-pager .totalPages').html(totalPages ? totalPages : 0);
                $('#custom-pager .currentPage').html(currentPage ? currentPage : 0);
                $('#custom-pager .goPage').on('click', function () {
                    var currPageNum = $('#custom-pager .pageNum').val();
                    if (currPageNum > 0 && currPageNum <= totalPages) {
                        GetWafer(currPageNum - 1);
                    } else {
                        $('#custom-pager .pageNum').val('1');
                    }
                });
                $("#waferexample a").click(function () {
                    GetWafer($(this).data("page") - 1);
                });
            }
        });
   
}

app.controller('detailList', function ($scope, $http) {
    $_http = $http;
    $_scope = $scope;
    GetWafer(0);
    getComment(0);
    $(function () {
        $.datetimepicker.setLocale('en');
        $('#textStartTime').datetimepicker({
            lang: 'ch',
            timepicker: false,
            format: 'Y-m-d',
            formatDate: 'Y-m-d',
            minDate: '2016/01/01', // yesterday is minimum date
            maxDate: getNowFormatDate()// and tommorow is maximum date calendar
        });
        $("#hidNewCommentID").val(guid());
        var commentID = $("#hidNewCommentID").val();
        var url = "/Systems/Uploader?LotID=" + lotID + "&commentID=" + commentID;
        uploadForComment = CreateUploader("uploadButton", "上传文件", "font-size:20px", "uploadPrompt", url, uploadFinished, uploadComplete);
        $("#btnSubmit").attr("disabled", true);
        //显示隐藏wafer
        $("#btnCollapseWafer").click(function () {
            $('#waferTrInput input').val('');
            $('#selectStatus').find("option").attr("selected", false);
            $('#collapsewafer').collapse('toggle');
            var ariaExpanded = $('#collapsewafer').attr('aria-expanded');
            if (ariaExpanded === 'false') $('#btnCollapseWafer span').html('+');
            if (ariaExpanded === 'true') {
                $('#btnCollapseWafer span').html('-');
                ShowImg();
            }
        });
        $("#waferTrInput input").keyup(function () {
            GetWafer(0);
        });
        $("#waferTrInput input").change(function () {
            GetWafer(0);
        });
        var _time = null;
        //单击事件
        $('#waferTable tbody').on('click', 'tr', function ($scope) {
            if (this.id == "lotsNotFound" || this.id == "loadingLots") return;
            var clicktr = $(this);
            clearTimeout(_time);
            _time = setTimeout(function ($scope) {
                //单击事件在这里
                $('.table.table-hover tbody tr').removeClass('isclick');
                if (!$(clicktr).hasClass('isclick')) {
                    $(clicktr).addClass('isclick');
                    //btnDisabled(clicktr);
                }
            }, 300);
        });
        //排序
        $('#waferTable thead th').not(".notorder").click(function () {
            var orderby = $(this).data("orderby");
            var desc = $(this).data("desc");
            var html = $(this).html();
            if (orderby != null) {
                $('#waferOrderby span').empty();
                if (desc) {
                    $(this).data("desc", false);
                    $(this).children('span').html("&darr;");
                }
                else {
                    $(this).data("desc", true);
                    $(this).children('span').html("&uarr;");
                }
                $('#orderBy').val(orderby);
                $('#desc').val(desc);
                GetWafer(0);
            }
        });
        $('#txtComment').keyup(function () {
            switchSubmitButton();
        });
        //save comment
        $("#btnSubmit").click(function () {
            var content=$.trim($("#txtComment").val());
            if (content.length > 0) {
                $('#btnSubmit').val('Saving...');
                $.post("/lots/Transform/AddComment", $("#frmcomment").serialize()).success(function () {
                    $("#txtComment").val("");
                    $("#attachmentList").empty();
                    initializeCommentAttachmentUploader();
                    getComment(0);
                    $('#btnSubmit').val('Submit');
                    $("#btnSubmit").attr("disabled", true);
                });
            }
        });
        // export
        $("a[articlelist='exportList']").click(function () {
            //var action = $(this).attr("attr_action");
            //var wafer_id = $("#inputWafer_id").val();
            //var status = $("#selectStatus").find("option:selected").val();
            //var equipment = $("#inputEquipment").val();
            //var program = $("#inputProgram").val();
            //var total = $("#inputTotal").val();
            //var inpyield = $("#inputYield").val();
            //var time = $("#inputStartTime").val();
            //var orderBy = $("#orderBy").val();
            //var desc = $("#desc").val();
            //$("#form1").attr("action", "/lots/wafer/ExportData");
            //$("#form1").append($("<input type='hidden' name='Wafer_id' id='Wafer_id'  value='"+wafer_id+"'/>"));
            //$("#form1").append($("<input type='hidden' name='Wafer_Program' id='Wafer_Program'  value='" + program + "'/>"));
            //$("#form1").append($("<input type='hidden' name='Equipment' id='Equipment'  value='" + equipment + "'/>"));
            //$("#form1").append($("<input type='hidden' name='Total' id='Total'  value='" + total + "'/>"));
            //$("#form1").append($("<input type='hidden' name='Yield' id='Yield'  value='" + inpyield + "'/>"));
            //$("#form1").append($("<input type='hidden' name='StartTime' id='StartTime'  value='" + time + "'/>"));
            //$("#form1").append($("<input type='hidden' name='Status' id='Status'  value='" + status + "'/>"));
            //$("#form1").append($("<input type='hidden' name='TransformId' id='TransformId'  value='" + id + "'/>"));
            //$("#form1").append($("<input type='hidden' name='Act' id='Act'  value='" + action + "'/>"));
            //$("#form1").append($("<input type='hidden' name='OrderBy' id='OrderBy'  value='" + orderBy + "'/>"));
            //$("#form1").append($("<input type='hidden' name='OrderDesc' id='OrderDesc'  value='" + desc + "'/>"));
            //$("#form1").attr("target", "_blank");
            //$("#form1").submit();
            //$("#form1").attr("action", "");
            //$("#form1").html("");
            //$("#form1").attr("target", "_self");
            if (recordCount > 0) {
                $("#frmDetails").attr("action", "/lots/wafer/exportwafer");
                $("#frmDetails").attr("target", "_blank");
                $("#frmDetails").submit();
                $("#frmDetails").attr("target", "");
                $("#frmDetails").attr("action", "");

                //$.post("/lots/wafer/exportwafer?" + $("#frmDetails").serialize(), function (res) {

                //});
            } else {
                alert(" Not Wafers to export.")
            }
            
        });

       
      

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
function initializeCommentAttachmentUploader() {
    //初始化CommentID和上传组件
    $("#hidNewCommentID").val(guid());
    var commentID = $("#hidNewCommentID").val();
    var url = "/Systems/Uploader?lotID=" + lotID + "&commentID=" + commentID;
    uploadForComment.setUploadURL(url);
}
//comment保存按钮
function switchSubmitButton() {
    if ($.trim($("#txtComment").val()).length > 0) {
        $("#btnSubmit").attr("disabled", false);
    }
    else {
        $("#btnSubmit").attr("disabled", true);
    }
}
//comment
function getComment(pageIndex) {
    $_http.post("/lots/Transform/CommentList?lotId=" + id + "&pageSize=15&pageIndex=" + pageIndex)
        .success(function (response) {
            $_scope.data = response;
            currentPage = $_scope.data.currentPage + 1;
            totalPages = $_scope.data.totalPages;
            if (parseInt($_scope.data.rows.length) > 0) {
                var options = {
                    currentPage: currentPage,
                    totalPages: totalPages,
                    numberOfPages: 5,
                    pageUrl: function (type, page, current) {
                        return "javascript:void(0);";
                    }
                }
                $('#pagerComment').bootstrapPaginator(options);
                $("#pagerComment a").click(function () {
                    getComment($(this).data("page") - 1);
                });
            }
    });
}
//显示img
function ShowImg() {
    $("[rel=drevil]").each(function () {
        var _this = this;
        var img = $(this).attr("attr_img");
        $(this).popover({
            trigger: 'manual',
            placement: 'right', //placement of the popover. also can use top, bottom, left or right
            //title: '<div style="text-align:center; color:red; text-decoration:underline; font-size:14px;"> Muah ha ha</div>', //this is the top title bar of the popover. add some basic css
            html: 'true', //needed to show html of course  
            content: '<div id="popOverBox"><img src="'+img+'" width="251" height="201" /></div>', //this is the content of the html box. add the image here or anything you want really.
            animation: false
        }).on("mouseenter", function () {
            $(this).popover("show");
            $(this).siblings(".popover").on("mouseleave", function () {
                $(_this).popover('hide');
            });
        }).on("mouseleave", function () {
            setTimeout(function () {
                if (!$(".popover:hover").length) {
                    $(_this).popover("hide")
                }
            }, 100);
        });
    });
}





