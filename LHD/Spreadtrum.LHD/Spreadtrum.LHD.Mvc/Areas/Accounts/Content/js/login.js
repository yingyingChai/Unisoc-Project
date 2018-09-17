$(document).ready(function () {
    $('#frmLogin').bootstrapValidator({
        message: '',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        submitHandler: function(validator, form, submitButton) {
            PostForm("/Accounts/Login/TryLogin", "", "", nextStep, communicateFailed);
        },
    });
});

function nextStep(result)
{
    var response = new Response(result);

    if (response.Code == 0)
    {
        response.Next();
    }
    else
    {
        alert(response.Message);
    }
   
}