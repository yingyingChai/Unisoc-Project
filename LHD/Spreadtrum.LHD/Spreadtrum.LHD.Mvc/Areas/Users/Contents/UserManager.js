var currentPage = 0;
var $_scope;
var $_http;
function displayAllUsers(pageIndex)
{
    //var searchData = '&fullName=' + $('#txtFullName').val() + '&email=' + $('#txtEmail').val() + '&roleText=' + $('#cmbRoleText').val() + '&accountState=' + $('#cmbAccountState').val();
    $_http.get("/Users/UserManager/DisplayUsersByRole?" + $("#frmUserManager").serialize() + '&pageSize=15&pageIndex=' + pageIndex)
      .success(function (response) {
          $_scope.data = response;
          currentPage = $_scope.data.currentPage + 1;
          totalPages = $_scope.data.totalPages;         
          $('#pager').html('');
          if (parseInt($_scope.data.rows.length) > 0) {
              //否则显示数据
              var options = {
                  currentPage: currentPage,//当前页
                  totalPages: totalPages,//一共多少页
                  numberOfPages: 10//每次显示多少页                    
              }
              $('#pagerForUsers').bootstrapPaginator(options);
              $('#custom-pager').show();
              $('#custom-pager .totalPages').html(totalPages ? totalPages : 0);
              $('#custom-pager .currentPage').html(currentPage ? currentPage : 0);
              $('#custom-pager .goPage').on('click', function () {
                  var currPageNum = $('#custom-pager .pageNum').val();
                  if (currPageNum > 0 && currPageNum <= totalPages) {
                      displayAllUsers(currPageNum - 1);
                  } else {
                      $('#custom-pager .pageNum').val('1');
                  }
              });
              $(".pagination a").click(function () {
                  displayAllUsers($(this).data("page") - 1);
              });
          }
          else {
              //如果没有记录，则提示没有内容
              
          }
          
      });
}

$(function () {
    $('#btnSave').click(function () {
        $('#DisableUserNull').modal("show");
    });
    $('#btnDisableUsers').click(function () {
        $.post('/Users/UserManager/DisableUsers', $('#frmUserManager').serialize(), function (result) {
            displayAllUsers(0);
        });
    });
    $('#peUserInput input').on('keyup', function () {
        displayAllUsers(0);
    });
    $('#peUserInput select').on('change', function () {
        displayAllUsers(0);
    });
});


app.controller('frmUserManager', function ($scope, $http)
{
    $_http = $http;
    $_scope = $scope;
    displayAllUsers(0);
    $(function () {
        $("#txtKeyword").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Users/SPRD/SearchUserByKeyword",
                    dataType: "json",
                    data: {
                        keyword: $("#txtKeyword").val()
                    },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return {                                
                                label: item.Account + "(" + item.ChineseName + ")   / " + item.JobEName,
                                value: item
                            }
                        }));
                    }
                });
            },
            minLength: 1,
            select: function (event, ui) {
                var user = ui.item.value;
                $("#txtAccount").val(user.Account);
                $("#txtJobName").val(user.JobEName);
                $("#txtChineseName").val(user.ChineseName);
                $("#txtEnglishName").val(user.EnglishName);
                $("#hidNewUserSPRDID").val(user.Id);
                $("#txtKeyword").val(user.Account + "(" + user.ChineseName + ")   / " + user.JobEName);
                $("#btnAddUser").attr("disabled", false);
                return false;
            },
            open: function () {
                $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
            },
            close: function () {
                $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
            }
        });

        $('#dlgNewUser').on('hidden.bs.modal', function (e) {
            $("#txtKeyword").val("");
            $("#txtEnglishName").val("");
            $("#txtChineseName").val("");
            $("#txtAccount").val("");
            $("#chkManager").val("");
            $('#addUserTip').text('');
            $('#hidNewUserSPRDID').val('');
            $("#btnAddUser").attr("disabled", "disabled");
        });

        $('#btnAddUser').click(function () {
          
            var id = $('#hidNewUserSPRDID').val();
         
            if (id === null || id === '') {
                $('#tipAddUser').text('请选择要添加的用户');
                return;
            };
            var ftval = $("#chkft").prop("checked");
            var cpval = $("#chkcp").prop("checked");
           
            if (!ftval && !cpval) {
                $('#tipAddUser').text('请选择JobType');
                return;
            }
            var jobtype = "#";
            if (ftval && !cpval) {
                jobtype += "FT";
            } else if (!ftval && cpval) {
                jobtype += "CP";
            } else if (ftval && cpval) {
                jobtype += "FT#CP";
            }
            jobtype += "#";
            alert(jobtype);
            $("#hidJobType").val(jobtype);
            $.post('/Users/SPRD/AddSprdUserToLHD', $('#addUserForm').serialize(), function (result) {
                var response = new Response(result);
                switch (response.Code)
                {
                    case "0":
                        break;
                    case "1":
                        break;                        
                }
                displayAllUsers(0);
            });
        });

    });



});
