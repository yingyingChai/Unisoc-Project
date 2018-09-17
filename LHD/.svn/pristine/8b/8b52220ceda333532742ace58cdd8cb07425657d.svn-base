var KaYiGrids = new Array();

function gotoPage(gridID, pageIndex) {
    for (var i = 0; i <= KaYiGrids.length - 1; i++) {
        if (KaYiGrids[i].id == gridID) {
            KaYiGrids[i].pageIndex = pageIndex;
            KaYiGrids[i].DisplayData();
        }
    }
}

function gotoGridPage(gridID, pageIndex) {

    if (parent.document.getElementById("lastPageIndex") != undefined) {
        parent.document.getElementById("lastPageIndex").value = pageIndex;
    }

    for (var i = 0; i <= KaYiGrids.length - 1; i++) 
    {
        if (KaYiGrids[i].id == gridID) {
            KaYiGrids[i].pageIndex = pageIndex;
            KaYiGrids[i].DisplayData();
        }
    }
}
function GetGrid(gridID) {
    var result;
    for (var i = 0; i <= KaYiGrids.length - 1; i++) {
        if (KaYiGrids[i].id == gridID) {
            result=KaYiGrids[i];
        }
    }
    return result;
}
function KaYiSelectAll(obj) {
    var layerID = obj.currentWorkingLayer;
    var checkBoxes = $(".KaYiChk_" + layerID);
    var selectAll=$("#selectAll" + layerID)[0].checked;
    for (var i = 0; i <= checkBoxes.length - 1; i++) {
        checkBoxes[i].checked = selectAll;
        var currentRow = $("#row" + obj.currentWorkingLayer + i);
        if (selectAll == true) {
            currentRow.removeClass(obj.unSelectClass);
            currentRow.addClass(obj.selectedClass);
        }
        else {
            currentRow.removeClass(obj.selectClass);
            currentRow.addClass(obj.unSelectClass);
        }
    }
}

function checkBoxChanged(obj) 
{
    var layerID = obj.currentWorkingLayer;
    var checkBoxes = $(".KaYiChk_" + layerID);
    var allSelected = true;
    for (var i = 0; i <= checkBoxes.length - 1; i++) {
        var currentRow=$("#row" + obj.currentWorkingLayer + i);
        if (checkBoxes[i].checked == false) {
            allSelected = false;
            currentRow.removeClass(obj.selectClass);
            currentRow.addClass(obj.unSelectClass);
        }
        else {
            currentRow.removeClass(obj.unSelectClass);
            currentRow.addClass(obj.selectedClass);
        }
    }
    $("#selectAll" + layerID)[0].checked = allSelected;
}


function CreateGrid(id, layer, pagerLayerID, tableDefine, url, urlParameters, action, selectedRowClass, unSelectedRowClass, pageIndex, pageSize) 
{
    var newGrid = new KaYiGrid(id, layer, pagerLayerID, tableDefine, url, urlParameters, action, selectedRowClass, unSelectedRowClass, pageIndex, pageSize,null);
    newGrid.DisplayData();
    KaYiGrids[KaYiGrids.length] = newGrid;
}


function CreateGrid(id, layer, pagerLayerID, tableDefine, url, urlParameters, action, selectedRowClass, unSelectedRowClass, pageIndex, pageSize,afterloadEvent) {
    var newGrid = new KaYiGrid(id, layer, pagerLayerID, tableDefine, url, urlParameters, action, selectedRowClass, unSelectedRowClass, pageIndex, pageSize,afterloadEvent);
    newGrid.DisplayData();
    KaYiGrids[KaYiGrids.length] = newGrid;
}




function toDateTime(dateStr) {
    return dateStr.replace("T", ' ');
}

function toSmallDateTime(dateStr)
{
    var tIndex = dateStr.indexOf("T");
    return dateStr.substring(0, tIndex);
}


function KaYiGrid(id,layer,pagerLayerID,tableDefine, url, urlParameters, action, selectedRowClass, unSelectedRowClass,pageIndex,pageSize,afterLoadEvent)
{ 
    var _this = this;    
    this.id = id;    
    this.currentWorkingLayer = layer;
    this.currentTableDefine = eval(tableDefine);
    this.selectedClass = selectedRowClass;
    this.unSelectClass = unSelectedRowClass;
    this.pageIndex = parseInt(pageIndex);
    this.pageSize =  parseInt(pageSize);
    this.pageCount = 0;
    this.pagerID = pagerLayerID;
    this.url = url;
    this.urlParameters = urlParameters;
    this.action = action;
    this.data = "";
    this.afterLoadEvent = afterLoadEvent;

    this.GetSelectedRows = function () {
        var checkboxes = $(".KaYiChk_" + _this.currentWorkingLayer);
        var result = new Array();
        var rows = eval(_this.data);
        var resultIndex = 0;
        for (var i = 0; i <= checkboxes.length - 1; i++) {
            if (checkboxes[i].checked == true) {
                result[resultIndex++] = rows[i];
            }
        }
        return result;
    };
    this.DisplayData = function () {
        currentWorkingLayer = _this.currentWorkingLayer;

        $("#" + _this.currentWorkingLayer).html(loadingPrompt); 

        if (_this.urlParameters == "" || _this.urlParameters == undefined || _this.urlParameters == null) {
            PostForm(url, "pageIndex=" + _this.pageIndex + "&pageSize=" + _this.pageSize, action, this.dataArrived, null);
        }
        else {
            PostForm(url, _this.urlParameters+ "&pageIndex=" + _this.pageIndex + "&pageSize=" + _this.pageSize, action, this.dataArrived, null);
        }

    };

    this.dataArrived = function (result) {
        var recordCountEndAt = result.indexOf('[');
        var data = result.substr(recordCountEndAt, result.length);
        _this.data = data;
        var recordCount = parseInt(result.substr(0, recordCountEndAt));

        //记录数量/页面大小 取整
        _this.pageCount = parseInt(recordCount / _this.pageSize);
        //计算余下的记录数量
        var recordLeft = recordCount - _this.pageCount * _this.pageSize;

        //如果有余下记录，则总页数+1
        if (recordLeft > 0) {
            _this.pageCount++;
        }

        //如果记录不足一页
        if (_this.pageCount < 1) {
            _this.pageCount = 1;
        }

        if (_this.pageIndex > _this.pageCount - 1) {
            _this.pageIndex--;
            _this.DisplayData();
        }
        else {
            var rows = eval(data);
            var output = "<table id='" + _this.currentWorkingLayer + "DataList'><thead><tr>"; ;
            for (var i = 0; i <= _this.currentTableDefine.length - 1; i++) {
                var columnType = _this.currentTableDefine[i]["ColumnType"];
                var style = "style='"
                if (_this.currentTableDefine[i]["Width"] != null && _this.currentTableDefine[i]["Width"] != "") {
                    style += "width:" + _this.currentTableDefine[i]["Width"] + ";";
                }
                style += "'";
                switch (columnType) {
                    case "CheckBox":
                        output += "<th class='" + _this.currentTableDefine[i]["HeaderClass"] + "' " + style + "><input type='checkbox' id='selectAll" + _this.currentWorkingLayer + "'/></th>";
                        break;
                    case "Radio":
                        output += "<th class='" + _this.currentTableDefine[i]["HeaderClass"] + "' " + style + ">" + _this.currentTableDefine[i]["Caption"] + "</th>";
                        break;
                    default:
                        output += "<th class='" + _this.currentTableDefine[i]["HeaderClass"] + "' " + style + ">" + _this.currentTableDefine[i]["Caption"] + "</th>";
                        break;
                }
            }
            output += "</tr></thead>";
            if (rows != undefined) {
                for (var rowIndex = 0; rowIndex <= rows.length - 1; rowIndex++) {
                    output += "<tr id='row" + _this.currentWorkingLayer + rowIndex + "' class='" + _this.unSelectClass + "'>";
                    for (var i = 0; i <= _this.currentTableDefine.length - 1; i++) {
                        var columnName = _this.currentTableDefine[i]["ColumnName"];
                        var fieldClass = _this.currentTableDefine[i]["RowClass"];
                        var fieldValue = rows[rowIndex][columnName];
                        if (fieldValue != null) {
                            var style = "style='"
                            if (_this.currentTableDefine[i]["Width"] != null && _this.currentTableDefine[i]["Width"] != "") {
                                style += "width:" + _this.currentTableDefine[i]["Width"] + ";";
                            }

                            if (_this.currentTableDefine[i]["Valign"] != null && _this.currentTableDefine[i]["Valign"] != "") {
                                style += "vertical-align:" + _this.currentTableDefine[i]["Valign"] + ";";
                            }

                            if (_this.currentTableDefine[i]["Align"] != null && _this.currentTableDefine[i]["Align"] != "") {
                                style += "text-align:" + _this.currentTableDefine[i]["Align"] + ";";
                            }
                            style += "'";
                            output += "<td class='" + fieldClass + "' " + style + " id='cell_" + _this.currentTableDefine[i]["ColumnName"] + "_" + rowIndex + "'>";
                            var columnType = _this.currentTableDefine[i]["ColumnType"];
                            switch (columnType) {
                                case "CheckBox":
                                    output += "<input type='checkbox' class='KaYiChk_" + _this.currentWorkingLayer + "' id='chk_" + _this.currentWorkingLayer + "_" + rows[rowIndex][columnName] + "' name='chk_" + _this.currentWorkingLayer + "_" + rows[rowIndex][columnName] + "'/>";
                                    break;
                                case "Radio":
                                    output += "<input type='radio' class='rdo_" + _this.currentWorkingLayer + "' id='rdo_" + _this.currentWorkingLayer + "_" + rows[rowIndex][columnName] + "' name='rdo_" + _this.currentWorkingLayer + "' value='" + rows[rowIndex][columnName] + "'/>";
                                    break;
                                case "SmallDateTime":
                                    output += toSmallDateTime(rows[rowIndex][columnName]);
                                    break;
                                case "DateTime":
                                    output +=  toDateTime(rows[rowIndex][columnName]);
                                    break;
                                case "Text":
                                    output += rows[rowIndex][columnName];
                                    break;
                                case "HyperLink":
                                    var url = _this.currentTableDefine[i]["Url"];
                                    var tag = "KaYiValueOf(";

                                    var useTag = false;
                                    while (url.indexOf(tag) >= 0) {
                                        var tagPosition = url.indexOf("KaYiValueOf(");
                                        var strBefore = url.substr(0, tagPosition);
                                        var strAfter = url.substr(tagPosition + tag.length, url.length - tagPosition + tag.length);
                                        var tagEndAt = strAfter.indexOf(");!");
                                        var tagName = strAfter.substr(0, tagEndAt);
                                        strAfter = strAfter.substr(tagEndAt + 1, strAfter.length - tagEndAt);

                                        if (tagName.substr(0, 1) == '\'' && tagName.substr(tagName.length - 1, 1) == '\'') {
                                            url = strBefore + tagName;
                                        }
                                        else
                                        {
                                            url = strBefore + rows[rowIndex][tagName] + strAfter;
                                        }
                                        useTag = true;
                                    }

                                    var displayField = _this.currentTableDefine[i]["DisplayField"];

                                    var linkClass = _this.currentTableDefine[i]["LinkClass"];
                                    if (linkClass == null) linkClass = "";

                                    var toggleString = "";
                                    if (_this.currentTableDefine[i]["DataToggle"]!="" && _this.currentTableDefine[i]["DataToggle"]!=null)
                                    {
                                        toggleString += " data-toggle='" + _this.currentTableDefine[i]["DataToggle"] + "'";
                                    }

                                    if (_this.currentTableDefine[i]["DataPlacement"]!="" && _this.currentTableDefine[i]["DataPlacement"]!=null)
                                    {
                                        toggleString += " data-placement='" + _this.currentTableDefine[i]["DataPlacement"] + "'";
                                    }

                                    if (_this.currentTableDefine[i]["DataContent"]!="" && _this.currentTableDefine[i]["DataContent"]!=null)
                                    {
                                        toggleString += " data-content='" + rows[rowIndex][_this.currentTableDefine[i]["DataContent"]] + "'";
                                    }

                                    if (_this.currentTableDefine[i]["DataOriginalTitle"] != "" && _this.currentTableDefine[i]["DataOriginalTitle"] != null)
                                    {
                                        toggleString += " data-original-title='" + rows[rowIndex][_this.currentTableDefine[i]["DataOriginalTitle"]] + "'";
                                    }
                                    else
                                    {
                                        toggleString += " data-original-title=''";
                                    }

                                    if (_this.currentTableDefine[i]["Title"] != "" && _this.currentTableDefine[i]["Title"] != null) {
                                        toggleString += "title='" + rows[rowIndex][_this.currentTableDefine[i]["Title"]] + "'";
                                    }
                                    else
                                    {
                                        toggleString += "title=''";
                                    }

                                    var linkTarget="";
                                    if (_this.currentTableDefine[i]["LinkTarget"])
                                    {
                                        linkTarget = " target='" + _this.currentTableDefine[i]["LinkTarget"] + "'";
                                    }


                                    if (displayField != "" && displayField != null) {
                                        if (useTag == true) {
                                            if (!displayField.indexOf("Fix")==0) {
                                                output += "<a href=" + url + " class='" + linkClass + "' "+toggleString+" "+linkTarget+">" + rows[rowIndex][displayField] + "</a>";
                                            }
                                            else
                                            {
                                                output += "<a href=" + url + " class='" + linkClass + "' "+toggleString+" "+linkTarget+">" + displayField.substr(3, displayField.length - 3) + "</a>";
                                            }
                                        }
                                        else {
                                            output += "<a href=" + url + rows[rowIndex][columnName] + " class='" + linkClass + "' "+toggleString+" "+linkTarget+">" + rows[rowIndex][displayField] + "</a>";
                                        }
                                    }
                                    else {
                                        if (url == "empty") {
                                            output += "<a id='" + rows[rowIndex][columnName] + "' class='" + linkClass + "' "+toggleString+" "+linkTarget+"></a>";
                                        }
                                        else {
                                            output += "<a href=" + url + " class='" + linkClass + "' "+toggleString+" "+linkTarget+"></a>";
                                        }
                                    }
                                    break;
                            }
                            output += "</td>";
                        }
                    }
                    output += "</tr>";
                }
            }
            output += "</table>";
            $("#" + _this.currentWorkingLayer).html(output);
            //注册全选动作
            $("#selectAll" + _this.currentWorkingLayer).change(function () {
                KaYiSelectAll(_this);
            });
            //注册选择动作
            $(".KaYiChk_" + _this.currentWorkingLayer).change(function () {
                checkBoxChanged(_this);
            });
            displayPager(_this);

            if (_this.afterLoadEvent != null)
            {
                _this.afterLoadEvent();
            }

        }
    }
}

function displayPager(grd) {
    var output = "";

    //首页

    if (grd.pageIndex == 0) {
        output += "<span class='pagerUnavailableFirstPage'></span>";
    }
    else {
        output += "<a class=pagerFirstPage href=javascript:gotoGridPage('" + grd.id + "',0)></a>";
    }
    //上一页链接
    if (grd.pageIndex == 0) {
        //如果当前是首页，则上一页不可用
        output += "<span class=pagerPrevPageNotAvailable></span>";
    }
    else {
        //否则上一页链接可用
        output += "<a class=pagerPreviousPage href=javascript:gotoGridPage('" + grd.id + "'," + (grd.pageIndex - 1) + ")></a>";
    }


    var startPage = 0;
    var endPage = 0;
    var maxPagerWidth = 500;
    if (grd.pageCount > 30) {
        startPage =grd.pageIndex- 15;
        endPage = grd.pageIndex + 14;
        if (startPage <= 0) {
            startPage = 0;
            endPage = 29;
        }
        if (startPage > 0) {
            output += "<span class='morePages'>...</span>";
        }

        if (endPage > grd.pageCount) {
            endPage = grd.pageCount;
        }

    }
    else {
        startPage = 0;
        endPage = grd.pageCount;
    }
    var pagerChars = "";
    var pagerLinks = 0;
    //第一页至最后一页
    for (var i = startPage; i <= endPage - 1; i++) {
        pagerLinks++;
        if (grd.pageIndex == i) {
            output += "<span class=currentPage>" + (i + 1) + "</span>";
        }
        else {
            output += "<a class=pagerToPage href=javascript:gotoGridPage('" + grd.id + "'," + i + ")>" + (i + 1) + "</a>";
        }

        pagerChars += i.toString();
        if (pagerChars.length * 8 + pagerLinks * 4 > maxPagerWidth) break;

    }

    if (endPage < this.pageCount) {

        output += "<span class='morePages'>...</span>";

    }
    //下一页链接
    if (grd.pageIndex == grd.pageCount - 1) {
        //如果已经是最后一页
        output += "<span class=pagerNextPageNotAvailable></span>";
    }
    else {
        //否则下一页链接可用
        output += "<a class=pagerNextPage href=javascript:gotoGridPage('" + grd.id + "'," + (grd.pageIndex + 1) + ")></a>";
    }

    //末页
    if (grd.pageIndex == grd.pageCount - 1) {
        output += "<span class='pagerUnavailableLastPage'></span>";
    }
    else {
        output += "<a class=pagerLastPage href=javascript:gotoGridPage('" + grd.id + "'," + (grd.pageCount - 1) + ")></a>";
    }


    $("#" + grd.pagerID).html(output);
}
