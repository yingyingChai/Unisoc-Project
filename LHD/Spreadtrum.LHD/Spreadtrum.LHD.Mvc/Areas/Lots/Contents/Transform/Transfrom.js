var currentPage = 0;
var $_scope;
var $_http;
var recordCounts = 0;
$(document).ready(function () {
    //$.datetimepicker.setLocale('en');
    $('#textCompletionDate').daterangepicker({
        minDate: '2016/01/01',
        maxDate: getNowFormatDate(),
        autoApply: true,
        locale: {
            format: 'YYYY/MM/DD',
        },
    });
    $('#textCompletionDate').val("");
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
function GetAllTransform(pageIndex)
{
    $("#loadingBox").css("display", "block");
    $("#queryResult").css("display", "none");
    $("#noLotsFound").css("display", "none");
    $_http.get("/Lots/Transform/Search?" + $("#frmQuery").serialize() + "&pageSize=50&pageIndex=" + pageIndex)
        .success(function (res) {
            $_scope.data = res;
            currentPage = $_scope.data.currentPage + 1;
            totalPages = $_scope.data.totalPages;
            $("#loadingBox").css("display", "none");
            $('#pager').html('');
           
            $_scope.TdClass = function (curDate, status,day) {
                var result = "";
                if (status == 1) {
                    if (day > 3) {
                        result = "day3tdclass";
                    }
                    if (day >= 1 && day <= 3) {
                        result = "daytdclass";
                    }
                }
                return result;
            }

            $_scope.whatClassIsIt = function (status) {
                var result = "";
               // alert(status + "," + status == auto);
                if (status== auto)
                { result = "isOperationHold"; }
                return result;
            };

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
                        GetAllTransform(currPageNum - 1);
                    } else {
                        $('#custom-pager .pageNum').val('1');
                    }
                });
                $(".pagination a").click(function () {
                    GetAllTransform($(this).data("page") - 1);
                });
                $(".lotRows").css("display", "");
                $("#loadingLots").css("display", "none");
                $("#lotsNotFound").css("display", "none");
                if (roletype == 2 || roletype == 3) { //pe或qa才显示操作项
                    $("#btn_info_row").css("display", "block");
                    $("#btn_info_confirm").css("display", "block");
                }
               // $(".operate_buttons").attr("disabled", false);
            } else {
                //如果没有记录，则提示没有内容 
               // $(".operate_buttons").attr("disabled", "disabled");
                $(".lotRows").css("display", "none");
                $("#loadingLots").css("display", "none");
                $("#lotsNotFound").css("display", "");
            }
           // $(".btu_list input").attr("disabled", "disabled");
        });
}

app.controller('getAllLotTable', function ($scope, $http)
{
    $_http = $http;
    $_scope = $scope;
    GetAllTransform(0);
    $scope.checkAll = function () {  //全选全不选  
        for (var i in $scope.data.rows) {
            $scope.data.rows[i].check = $scope.all;
        }
    }
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
               
                if (!$(clicktr).hasClass('isclick') || !$(clicktr).hasClass('isOperationHold')) {
                    $(clicktr).addClass('isclick');
                    //btnDisabled(clicktr);
                }
            }, 300);
        });

        $('.table.table-hover tbody').on('dblclick', 'tr', function ($scope) {
            if (this.id == "lotsNotFound" || this.id == "loadingLots") return;
            clearTimeout(_time);
            //双击代码事件在此
            if ($('.table.table-hover').hasClass('table-query')) {
                window.location.href = "/Lots/transform/Detail?id=" + $(this).attr('id');
            }
        });

        $("#frmQuery input").not(".boxclass").keyup(function () {

            GetAllTransform(0);
        });
        $("#frmQuery input").not(".boxclass").change(function () {
            GetAllTransform(0);
        });

        $("#frmQuery select").change(function () {
            GetAllTransform(0);
        });
        //查询结束   
        $("#btnExportData").click(function () {
            if (recordCounts > 0) {
                $.post("/Lots/Transform/ExportData?" + $("#frmQuery").serialize(), function (data) {
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
           
            //var action = $(this).attr("attr_action");
            //$("#frmQuery").attr("action", "/Lots/Transform/ExportData");
            //$("#frmQuery").append($("<input type='hidden' name='Act' id='Act'  value='" + action + "'/>"));
            //$("#frmQuery").attr("target", "_blank");
            //$("#frmQuery").submit();
            //$("#frmQuery").attr("action", "");
            //$("#Act").remove();
            //$("#frmQuery").attr("target", "_self");
        });
        //dispose button
        $("input[name='btnoperate']").click(function () {
            $("input[name='btnoperate']").removeClass("btn-danger");
            $(this).addClass("btn-danger");
            var id = $(this).attr("attr_id");
            $("#hidstatus").val(id);
            $("#hidstatustext").val($(this).val());
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
                GetAllTransform(0);
            }
        });

        //one day
        $("a[name='btnDays']").click(function () {
            $("#textCompletionDate").val("");
            $("a[name='btnDays']").removeClass("btn-danger");
            $(this).addClass("btn-danger");
            var index = $(this).index();
            var day=0;
            switch (index) {
                case 2:
                    day = 1;
                    break;
                case 3:
                    day = 3;
                    break;
            }
            $("#hidLastDay").val(day);
            GetAllTransform(0);
        });
        $("#btnconfirm").click(function () {
            $(this).attr("disabled", "disabled");
            var strlotids = "";
            $("input[id^='chk_']").each(function () {
                chkval = $(this).prop("checked");
                if (chkval) {
                    if (strlotids != "") {
                        strlotids += ",";
                    }
                    strlotids += $(this).attr("attrid");
                }
            });
            if (strlotids == "" || strlotids==undefined) {
                alert("Please select item");
                $(this).removeAttr("disabled");
                return;
            }
            if (roletype == 2 || roletype == 3) {
                var status = $("#hidstatus").val();
                if (status <= 0 || status == undefined || status == "") {
                    $(this).removeAttr("disabled");
                    alert("Please dispose then first before confirmation.");
                    return;
                }
                var obj = { "lotids": strlotids, "status": status };
                $http({
                    method: "POST",
                    url: "/transform/PeDisposeByLot",
                    params: obj
                }).success(function (res) {
                   
                    if (res.suc > 0) {
                        alert("success");
                        GetAllTransform(currentPage - 1);
                    } else {
                        alert("fail");
                    }
                  
                });
            }
            $(this).removeAttr("disabled");
        });
    });

})

$(function () {
    $(document).scroll(function () {
        if ($(document).scrollTop() > 218) {
            $("#fixed_div").addClass("fixed_nav");
        } else {
            $("#fixed_div").removeClass("fixed_nav");
        }
    })
})


