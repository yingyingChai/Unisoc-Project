var KaYiDialogs = new Array();
function CreateDialog(ID, BaseUrl, Width, Height, onOK, onCancel, onClose, afterOK, afterCancel, afterClose) {

    var dialog = new KaYiDialog();
    dialog.ID = ID;
    dialog.BaseUrl = BaseUrl;
    dialog.Height = Height;
    dialog.Width = Width;
    dialog.onOK = onOK;
    dialog.onCancel = onCancel;
    dialog.onClose = onClose;
    dialog.afterOK = afterOK;
    dialog.afterCancel = afterCancel;
    dialog.afterClose = afterClose;
    KaYiDialogs[KaYiDialogs.length] = dialog;
}
function AppendDialog(dialog) {
    KaYiDialogs[KaYiDialogs.length] = dialog;
}
function GetDialog(dialogID) {
    var result=null;
    for (var i = 0; i <= KaYiDialogs.length - 1; i++) {
        if (KaYiDialogs[i].ID == dialogID) {
            result = KaYiDialogs[i];
        }
    }
    return result;
}
function KaYiDialog() {
    var _this = this;
    
    //如果要执行窗口页面内部的脚本，定义这三个事件
    this.onClose = null;
    this.onOK = null;
    this.onCancel = null;
    //如果要执行窗口外部的脚本，定义者三个事件
    this.afterClose = null;
    this.afterOK = null;
    this.afterCancel = null;

    this.Width = 0;
    this.Height = 0;
    this.Title = "";
    this.BaseUrl = "";
    this.ID = "";
    this.dialogObj = null;

    this.GetInnerFunction=function(functionName) {
        return  "iframe" +_this.ID+ "." + functionName;
    }

    this.Close = function () {
        try {
            setTimeout(function () { _this.dialogObj.dialog('close'); }, 1);
        }
        catch (ex)
        {
            //donothing 
        }
    }

    this.ShowCloseDialog = function (title, parameters) {
        _this.dialogObj = $('<div id=dlg' + _this.ID + '></div>')
                       .html('<iframe id=iframe' + _this.ID + ' frameborder=0 src=' + _this.BaseUrl + '?' + parameters + '  width=100% height=100% border=0 scrolling=no/>')
                       .dialog(
                                {
                                    autoOpen: false,
                                    modal: true,
                                    width: _this.Width,
                                    height: _this.Height,
                                    title: title,
                                    buttons:
                                    {
                                        "关闭": function () {
                                            if (_this.onOK != null && _this.onOK != "") {
                                                window.eval('parent.iframe' + _this.ID + '.' + _this.onOK);
                                            }
                                            TryToRun(_this.afterOK);
                                        }
                                    },
                                    close: function () {
                                        if (_this.onClose != null && _this.onClose != "") {
                                            eval('parent.iframe' + _this.ID + '.' + _this.onClose);
                                        }
                                        TryToRun(_this.afterClose);
                                        _this.dialogObj.dialog("destroy").detach();
                                    }
                                }
                       );
        _this.dialogObj.dialog('open');
    }



    this.ShowDialog = function (title, parameters) {
        _this.dialogObj = $('<div id=dlg' + _this.ID + '></div>')
                       .html('<iframe id=iframe' + _this.ID + ' frameborder=0 src=' + _this.BaseUrl + '?' + parameters + '  width=100% height=100% border=0 scrolling=no/>')
                       .dialog(
                                {
                                    autoOpen: false,
                                    modal: true,
                                    width: _this.Width,
                                    height: _this.Height,
                                    title: title,
                                    buttons:
                                    {
                                        "确定": function () {
                                            if (_this.onOK != null && _this.onOK != "") {
                                                
                                                window.eval('parent.iframe' + _this.ID + '.' + _this.onOK);
                                            }
                                            TryToRun(_this.afterOK);
                                        },
                                        "取消": function () {

                                            if (_this.onCancel != null && _this.onCancel != "") {
                                                eval('parent.iframe' + _this.ID + '.' + _this.onCancel);
                                            }
                                            _this.dialogObj.dialog("close");
                                            TryToRun(_this.afterCancel);
                                        }
                                    },
                                    close: function () {
                                        if (_this.onClose != null && _this.onClose != "") {
                                            eval('parent.iframe' + _this.ID + '.' + _this.onClose);
                                        }
                                        TryToRun(_this.afterClose);
                                        _this.dialogObj.dialog("destroy").detach();
                                    }
                                }
                       );
        _this.dialogObj.dialog('open');
    }


    this.ShowDialogWithoutButtons = function (title, parameters) {
        _this.dialogObj = $('<div id=dlg' + _this.ID + '></div>')
                       .html('<iframe id=iframe' + _this.ID + ' frameborder=0 src=' + _this.BaseUrl + '?' + parameters + '  width=100% height=100% border=0 scrolling=no/>')
                       .dialog(
                                {
                                    autoOpen: false,
                                    modal: true,
                                    width: _this.Width,
                                    height: _this.Height,
                                    title: title
                                   
                                }
                       );
        _this.dialogObj.dialog('open');
    }



    this.ShowNextDialog = function (title, parameters) {
        _this.dialogObj = $('<div id=dlg' + _this.ID + '></div>')
                       .html('<iframe id=iframe' + _this.ID + ' frameborder=0 src=' + _this.BaseUrl + '?' + parameters + '  width=100% height=100% border=0 scrolling=no/>')
                       .dialog(
                                {
                                    autoOpen: false,
                                    modal: true,
                                    width: _this.Width,
                                    height: _this.Height,
                                    title: title,
                                    buttons:
                                    {
                                        "下一步": function () {
                                            if (_this.onOK != null && _this.onOK != "") {
                                                eval('parent.iframe' + _this.ID + '.' + _this.onOK);
                                            }
                                            TryToRun(_this.afterOK);
                                        },
                                        "取消": function () {

                                            if (_this.onCancel != null && _this.onCancel != "") {
                                                eval('parent.iframe' + _this.ID + '.' + _this.onCancel);
                                            }
                                            _this.dialogObj.dialog("close");
                                            TryToRun(_this.afterCancel);
                                        }
                                    },
                                    close: function () {
                                        if (_this.onClose != null && _this.onClose != "") {
                                            eval('parent.iframe' + _this.ID + '.' + _this.onClose);
                                        }
                                        TryToRun(_this.afterClose);
                                        _this.dialogObj.dialog("destroy").detach();
                                    }
                                }
                       );
        _this.dialogObj.dialog('open');
    }


}

function TryToRun(str) {

    if (str != null && str != "") {
        var text = str.toString();
        if (text.substr(0, 1) == "#") {
            var cmd = text.substr(1, text.length - 1);
            eval(cmd);
        }
        else {
            str.call();
        }
    }
}

function donothing()
{

}

function ShowMessage(msgType, title, prompt, timeoutSecond, HideFunction) {
    this.dlgDiv = $("<div title='" + title + "'><p>" + prompt + "</p></div>");
    $("#dialog:ui-dialog").dialog("destroy");
    this.dlgDiv.dialog({
        height: 140,
        modal: true,
        resizable: false
    });
    var ttl = timeoutSecond || 1000;
    setTimeout(function () {
        this.dlgDiv.dialog("close")
        if (HideFunction != null && HideFunction != "") {
            HideFunction.call();
        }
    }, ttl);
}

function MsgBoxOKOnly(msgType, title, prompt, okFunction) {
    this.dlgDiv = $("<div title='" + title + "'><p>" + prompt + "</p></div>");
    $("#dialog:ui-dialog").dialog("destroy");
    this.dlgDiv.dialog({
        modal: true,
        resizable: false,
        buttons: {
            "确定": function () {
                if (okFunction != null && okFunction != "") {
                    $(this).dialog("close");
                    okFunction.call();
                }
                else {
                    $(this).dialog("close");
                }
            }
        }
    });
}

function MsgBox(msgType, title, prompt, yesFunction, noFunction) {
    this.dlgDiv = $("<div title='" + title + "'><p><span class='ui-icon ui-icon-alert' style='float:left; margin:0 7px 20px 0;'></span>" + prompt + "</p></div>");
    $("#dialog:ui-dialog").dialog("destroy");
    this.dlgDiv.dialog({
        resizable: false,
        height: 140,
        modal: true,
        buttons: {
            "确定": function () {
                $(this).dialog("close");
                if (yesFunction != null && yesFunction != "") {
                    yesFunction.call();
                }

            },
            "取消": function () {
                $(this).dialog("close");
                if (noFunction != null && noFunction != "") {
                    noFunction.call();
                }

            }
        }
    });
}


