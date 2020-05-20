// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var isOk;
toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": true,
    "progressBar": true,
    "positionClass": "toast-top-left",
    "preventDuplicates": false,
    "showDuration": "100",
    "hideDuration": "100",
    "timeOut": "3000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

function openInPopUpEdit(id) {
    showInPopup('/Client/CreateClient/' + id);
}

function showPopUpDelete(id) {
    document.getElementById("btnDelete").setAttribute("onclick", "deleteClient('/Client/DeleteClient/" + id + "')");
    $('#delete-modal').modal('show');
}

function deleteClient(url) {
    $.ajax({
        type: 'POST',
        url: url,
        success: function (res) {
            if (res.isDeleted) {
                $('#view-list-clients').html(res.html);
                $('#delete-modal').modal('hide');
                toastr.warning("Client deleted successfuly !");
            }
            else {
                $('#delete-modal').modal('hide');
                toastr.info("Client not deleted");
            }
        },
        error: function (err) {
            $('#delete-modal').modal('hide');
            toastr.error("Error occured");
        }
    });
}

function showInPopup(url, title) {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    });
}

function JqueryAjaxPost(form) {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-list-clients').html(res.html);
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('.modal-content').modal('hide');
                    $('#form-modal').modal('hide');
                    toastr.success("Client submitted successfuly","Submit client",);
                }
                else {
                    toastr.warning("Form not valid");
                }
            },
            error: function (err) {
                console.log(err);
                alert("Error occured");
            }
        });
    }
    catch (e) {
        console.log(e);
    }

    // to prevent form submit event.
    return false;
}

function onReset() {
    document.getElementById("MyForm").reset();
}

function getLastPayedPeriod(idPaymentOnGoing) {
    $.ajax({
        type: 'GET',
        url: '/Client/GetLastPayedPeriodByIdPayment/' + idPaymentOnGoing,
        success: function (res) {
            $("#lastPayedPeriod").html(res.lastPayedPeriod);
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function isPaymentOK(idPaymentOnGoing) {
    $.ajax({
        type: 'GET',
        url: '/Client/isPaymentOK/' + idPaymentOnGoing,
        success: function (res) {
            if (res.isPaymentOk) {
                showPaymentAsOk();
            }
            else {
                showPaymentAsNok();
            }
        }
    });
}

function showPaymentAsOk() {
    $(".payment-div-ok").show()
    $(".payment-div-nok").hide();
    $('#btnPay').prop('disabled',true);
}

function showPaymentAsNok() {
    $(".payment-div-ok").hide()
    $(".payment-div-nok").show();
    $('#btnPay').prop('disabled', false);
}

function getPaymentStatus(IdPaymentOnGoing) {
    $.ajax({
        type: 'GET',
        url: '/Client/isPaymentOK/' + IdPaymentOnGoing,
        success: function (res) {
            console.log("From getPaymentStatus isOk = " + res.isPaymentOk);
            return '<div class="icon-status"><i class="fa fa-thumbs-up fa-lg text-success"></i></div>';
        },
        error: function (err) {
            console.log("From getPaymentStatus isOk = false");
            return '<div class="icon-status"><i class="fa fa-thumbs-down fa-lg text-danger"></i></div>';
        }
    });
}


function pay(id) {
    $.ajax({
        type: 'POST',
        url: '/Client/Pay/' + id,
        data: id,
        success: function (res) {
            toastr.success("Payment is done successfuly");
            showPaymentAsOk();
            $('#view-list-clients').html(res.html);
            getLastPayedPeriod(res.IdPayment);
            $('#IdPaymentOnGoing').val(res.IdPayment);

        },
        error: function (err) {
            toastr.error("Payment failed");
        }
    });
}