var serverError = "服务器通信失败，请重试。";
function htmlEncode(value) {
    return $('<div/>').text(value).html();
}
function htmlDecode(value) {
    return $('<div/>').html(value).text();
}
function replaceInvalidChars(source) {
    while (source.indexOf('+') >= 0) {
        source = source.replace('+', "%2B");
    }
    return source;
}

function PostForm(url, parameters, action, successFunction, failedFunction) {
    
    var queryString = window.location.search;
    if (queryString != "" && queryString != null) {
        url += queryString + "&" + parameters;
    }
    else {
        url += "?" + parameters;
    }

    $.ajax({
        url: url,
        type: "POST",
        data: {
            KaYiData: $(document.forms[0]).serialize(),
            KaYiAction: action
        },
        timeout: 30000,
        dataType: "text",
        async: false,
        success: function (result) {
            if (successFunction != null && successFunction != "") {
                successFunction(result);
            }
        },
        error: function (result) {
            if (failedFunction != null && failedFunction != "") {                
                failedFunction(result)
            }
        }
    });

}

function Redirect(url, newWindow) {
    if (newWindow == false) {
        document.location = url;
    }
    else {
        window.open(url);
    }
}

function Response(result) {
    var _this = this;
    this.Code = "";
    this.Message = "";
    this.ResponseType = "";
    this.TipType = "";
    this.NextUrl = "";
    this.Script = "";
    this.Tag = "";
    this.Next = function () {
        switch (this.ResponseType) {
            case "Tip":
                ShowDialogAutoClose('dlgFailed', this.Message, "d-" + this.TipType, 5);
                break;
            case "Redirect":
                Redirect(this.NextUrl,false);
                break;
            case "RedirectInNewWindow":
                Redirect(this.NextUrl, true);
                break;
            case "RunScript":
                var script = this.Script;
                if (script != null && script != "") {
                    eval(script);
                }
                break;
        }
    };

    var init = function () {
        try {
            var _serverResponse = eval("(" + result + ")");
            _this.Code = _serverResponse.Code;
            _this.Message = _serverResponse.Message;
            _this.ResponseType = _serverResponse.ResponseType;
            _this.TipType = _serverResponse.TipType;
            _this.NextUrl = _serverResponse.NextUrl;
            _this.Script = _serverResponse.Script;
            _this.Tag = _serverResponse.Tag;
        }
        catch (ex)
        {
            //donothing 
        }
    };
    init();
    return _this;
    
}



function communicateFailed(result) {
    alert("服务器通信失败，请稍后再试");
}