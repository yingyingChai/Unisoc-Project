$(document).ready(function () {
    //$.datetimepicker.setLocale('en');
    //$('#textCreateTime').datetimepicker({
    //    lang: 'ch',
    //    timepicker: false,
    //    format: 'Y-m-d',
    //    formatDate: 'Y-m-d',
    //    minDate: '2016/01/01', // yesterday is minimum date
    //    maxDate: getNowFormatDate()// and tommorow is maximum date calendar
    //});
    $('#textCreateTime').daterangepicker({
        endDate: moment(),
        minDate: '2016/01/01',
        maxDate: getNowFormatDate(),
        autoApply: true,
        locale: {
            format: 'YYYY/MM/DD',
        }
    });
    $('#textCreateTime').val("");
  
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
var currentPage = 0;
var $_scope;
var $_http;
var recordCounts = 0;
function HistoryList(pageIndex) {
    $("#loadingBox").css("display", "block");
    $("#queryResult").css("display", "none");
    $("#noLotsFound").css("display", "none");
    $_http.get("/Lots/wafer/HistorySearch?" + $("#frmQuery").serialize() + "&pageSize=15&pageIndex=" + pageIndex)
    .success(function (res) {
        $_scope.data = res;
        currentPage = $_scope.data.currentPage + 1;
        totalPages = $_scope.data.totalPages;
        $("#loadingBox").css("display", "none");
        $('#pager').html('');
        recordCounts = parseInt($_scope.data.rows.length);
        if (recordCounts > 0) {
            var options = {
                currentPage: currentPage,//当前页
                totalPages: totalPages,//一共多少页
                numberOfPages: 10//每次显示多少页     
            }
            $('#pager').bootstrapPaginator(options);
            $('#custom-pager').show();
            $('#custom-pager .totalPages').html(totalPages ? totalPages : 0);
            $('#custom-pager .currentPage').html(currentPage ? currentPage : 0);
            $('#custom-pager .goPage').on('click', function () {
                var currPageNum = $('#custom-pager .pageNum').val();
                if (currPageNum > 0 && currPageNum <= totalPages) {
                    HistoryList(currPageNum - 1);
                } else {
                    $('#custom-pager .pageNum').val('1');
                }
            });
            $(".pagination a").click(function () {
                HistoryList($(this).data("page") - 1);
            });
            $(".lotRows").css("display", "");
            $("#loadingLots").css("display", "none");
            $("#lotsNotFound").css("display", "none");
            //$(".operate_buttons").attr("disabled", false);
        } else {
            //如果没有记录，则提示没有内容 
          //  $(".operate_buttons").attr("disabled", "disabled");
            $(".lotRows").css("display", "none");
            $("#loadingLots").css("display", "none");
            $("#lotsNotFound").css("display", "");
            $("#btn_info_row").css("display", "none");
            $("#btn_info_confirm").css("display", "none");
        }
        //$(".btu_list input").attr("disabled", "disabled");
    });
}
app.controller('historytable', function ($scope, $http)
{
    $_http = $http;
    $_scope = $scope;
    $("#loadingBox").css("display", "block");
    $("#queryResult").css("display", "none");
    $("#noLotsFound").css("display", "none");
    $("#loadingLots").css("display", "none");
    //HistoryList(0);
    $(function () {
        var _time = null;
        //单击事件
        $('.table.table-hover tbody').on('click', 'tr', function ($scope) {
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
        $("#frmQuery input").keyup(function () {
            HistoryList(0);
        });
        $("#frmQuery input").change(function () {
            HistoryList(0);
        });
        $("#frmQuery select").change(function () {
            HistoryList(0);
        });
        //排序
        $('.table-query thead th').click(function () {
            var orderby = $(this).data("orderby");
            var desc = $(this).data("desc");
            var html = $(this).html();
            if (orderby != null) {
                $('.table-query th span').html("")
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
                HistoryList(0);
            }
        });
        $("#btnExport").click(function () {
            if (recordCounts > 0) {
                $.post("/Lots/Wafer/ExportHistory?" + $("#frmQuery").serialize(), function (data) {
                    var response = new Response(data);
                    if (response.Code == 0) {
                        response.Next();
                    }
                    else {
                        alert(response.Message);
                    }
                });
            } else {
                alert(" Not Lots to export.")
            }
        });
    });
})