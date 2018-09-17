var currentPage = 0;
var $_scope;
var $_http;
var recordCounts = 0;
$(document).ready(function () {

    $('#textCompletionDate').daterangepicker({
        endDate: moment(),
        minDate: '2016/01/01',
        maxDate: getNowFormatDate(),
        autoApply: true,
        locale: {
            format: 'YYYY/MM/DD',
        }
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
function LoadAllWafer(pageIndex)
{
    $("#loadingBox").css("display", "block");
    $("#queryResult").css("display", "none");
    $("#noLotsFound").css("display", "none");
    $_http.get("/Lots/wafer/search?" + $("#frmQuery").serialize() + "&pageSize=50&pageIndex=" + pageIndex)
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
                       LoadAllWafer(currPageNum - 1);
                   } else {
                       $('#custom-pager .pageNum').val('1');
                   }
               });
               $(".pagination a").click(function () {
                   LoadAllWafer($(this).data("page") - 1);
               });
               $(".lotRows").css("display", "");
               $("#loadingLots").css("display", "none");
               $("#lotsNotFound").css("display", "none");
              // $(".operate_buttons").attr("disabled", false);
               if (roletype == 2 || roletype == 3) { //pe或qa才显示操作项
                   $("#btn_info_row").css("display", "block");
               }
               $("#btn_info_confirm").css("display", "block");
           } else {
               //如果没有记录，则提示没有内容 
             //  $(".operate_buttons").attr("disabled", "disabled");
               $(".lotRows").css("display", "none");
               $("#loadingLots").css("display", "none");
               $("#lotsNotFound").css("display", "");
               $("#btn_info_row").css("display", "none");
               $("#btn_info_confirm").css("display", "none");
           }
          // $(".btu_list input").attr("disabled", "disabled");
       });
}
app.controller('getAllLotTable', function ($scope, $http)
{
    
    $_http = $http;
    $_scope = $scope;
    LoadAllWafer(0);
    
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
                if (!$(clicktr).hasClass('isclick')) {
                    $(clicktr).addClass('isclick');
                    $("td").find("textarea").css("color", "#646464");
                    //btnDisabled(clicktr);
                }
            }, 300);
        });
        $("#frmQuery input").not(".boxclass").keyup(function () {
            LoadAllWafer(0);
        });
        $("#frmQuery input").not(".boxclass").change(function () {
            LoadAllWafer(0);
        });
        $("#frmQuery select").change(function () {
            LoadAllWafer(0);
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
                LoadAllWafer(0);
            }
        });
        $("input[name='btnoperate']").click(function () {
            $("input[name='btnoperate']").removeClass("btn-danger");
            $(this).addClass("btn-danger");
            var id = $(this).attr("attr_id");
            $("#hidstatus").val(id);
            $("#hidstatustext").val($(this).val());
        });
        //one day
        $("a[name='btnDays']").click(function () {
            $("#textCompletionDate").val("");
            $("a[name='btnDays']").removeClass("btn-danger");
            $(this).addClass("btn-danger");
            var index = $(this).index();
            var day = 0;
            switch (index) {
                case 1:
                    day = 1;
                    break;
                case 2:
                    day = 3;
                    break;
            }
            $("#hidLastDay").val(day);
            LoadAllWafer(0);
        });
        //导出
        $("#btnExportData").click(function () {
            if (recordCounts > 0) {
                $.post("/Lots/wafer/ExportData?" + $("#frmQuery").serialize(), function (data) {
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

        $("#btnconfirm").click(function () {
            $(this).attr("disabled", "disabled");
            var chkval;
            var strwaferids = "";
            var strlotids = "";
            $("input[id^='chk_']").each(function () {
                chkval = $(this).prop("checked");
                if (chkval) {
                    if (strwaferids != "") {
                        strwaferids += ",";
                    }
                    if (strlotids != "") {
                        strlotids += ",";
                    }
                    strwaferids += $(this).attr("attrwafer");
                    strlotids += $(this).attr("attrlot");
                }
            });
            if (strwaferids == "") {
                $(this).removeAttr("disabled");
                alert("Please select item");
                return;
            }
            $("#hidwaferids").val(strwaferids);
            $("#hidlotids").val(strlotids);
            var isconfirm = true;
            if (roletype == 2 || roletype == 3) {//pe/qa
                var status = $("#hidstatus").val();
                if (status <= 0 || status == undefined || status == "") {
                    $(this).removeAttr("disabled");
                    alert("Please dispose then first before confirmation.");
                    return;
                }
                var id = "";
                var txt = "";
                var error = 0;
                if (roletype == 2) {//pe
                    $("td[id^='pedispose_']").each(function () {
                        txt = $(this).attr("attr_text").trim();
                        if (txt == "") {
                            id = $(this).attr("attr_id");
                            chkval = $("#chk_" + id).prop("checked");
                            if (!chkval) {
                                error++;
                            }
                        }
                    });
                }
                if (roletype == 3) {//qa
                    $("td[id^='qadispose_']").each(function () {
                        txt = $(this).attr("attr_text").trim();
                        if (txt == "") {
                            id = $(this).attr("attr_id");
                            chkval = $("#chk_" + id).prop("checked");
                            if (!chkval) {
                                error++;
                            }
                        }
                    });
                }
                if (error > 0) {
                    $(this).removeAttr("disabled");
                    isconfirm = confirm("you have " + error + " item not selected");
                }
            }
           
            if (isconfirm)
            {
                $http.post("/Lots/wafer/PEQADispose?" + $("#frmQuery").serialize()).success(function (res) {
                    if (res.suc > 0) {
                       alert("success");
                       LoadAllWafer(currentPage - 1);
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