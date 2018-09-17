var app = angular.module('myApp', []);
function startMessageListener() {
    var userID = $("#hidCurrentUserID").val();
    var wsUri = "ws://lhd.spreadtrum.com:9999/Notifications?uid=" + userID;
    try
    {
        websocket = new WebSocket(wsUri);
        websocket.onmessage = function (evt) {
            onMessage(evt)
        };
    }
    catch (ex)
    {
        //do nothing 
    }
}
function onMessage(evt) {
    var message = JSON.parse(evt.data);
    var notificationCount = message.WaitForConfirm + message.WaitDispose + message.WaitForOtherBinDispose;

    try {
        $('#Notification').html(notificationCount);
        $('#Notification').css('display', notificationCount == 0 ? "none" : "block");

        $('#WaitForConfirm').html(message.WaitForConfirm);
        $('#WaitForConfirm').css('display', message.WaitForConfirm == 0 ? "none" : "block");

        $('#WaitForDispose').html(message.WaitDispose);
        $('#WaitForDispose').css('display', message.WaitDispose == 0 ? "none" : "block");

        $('#NewComments').html(message.NewComments);
        $('#NewComments').css('display', message.NewComments == 0 ? "none" : "block");

        $('#WaitForOtherBinDispose').html(message.WaitForOtherBinDispose);
        $('#WaitForOtherBinDispose').css('display', message.WaitForOtherBinDispose == 0 ? "none" : "block");
    }
    catch (ex)
    {
        //do nothing 
    }

}

$(document).ready(function () {
    startMessageListener();
    
});
