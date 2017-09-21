
$("#BankAccounts").ready(function () {

    $.ajax({
        url: "/Populate/GetAllBankAccount",
        datatype: "json",
        beforeSend: loadStart,
        complete: loadStop,
        success: function (data) {
            var ddlBankAccounts = $("#BankAccounts");
            ddlBankAccounts.empty();
            $.each(data, function (val, text) {

                ddlBankAccounts.append(
                    $("<option></option>").val(text.Value).html(text.Text)
                );
            });

        }
    });
});

function loadStart() {
    $("#loading").show();
}
function loadStop() {
    $("#loading").hide();
}