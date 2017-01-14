

function DeleteConfirm(successCallBack, cancelCallBack)
{
    bootbox.dialog({
        message: 'Are you sure?',
        title: "Delete Confirm",
        className: "modal-darkorange",
        buttons: {
            "Cancel": {
                callback: cancelCallBack
            },
            success: {
                label: "Ok",
                className: "btn-danger",
                callback: successCallBack
            }
        }
    });

    //bootbox.confirm("Are you sure?", function (result) {
    //    if (result) {
    //        alert("delete");
    //    }
    //});
}

function showAlert() {
    if ($('#alert') != undefined && $('#alert') != null && $('#alert').children().length > 0 )
    {
        $('#alert').slideDown(300);
        setTimeout(function () {
            $('#alert').slideUp(300);
        }, 5000);
    }
    
}

function showLoading() {
    //document.getElementById('loadingContainer').className = 'loading-container loading-active';

    $('.loading-container').removeClass('loading-inactive');
    $('.loading-container').addClass('loading-active');
}

function hideLoading() {
    //document.getElementById('loadingContainer').className = 'loading-container loading-inactive';

    $('.loading-container').removeClass('loading-active');
    $('.loading-container').addClass('loading-inactive');
}