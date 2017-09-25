
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
$(function () {
 
    $(".btn").click(function () {
        alert(this.id);
        var bankAccountId = this.id;
        var userId = $("#Id").val();
    $.ajax({
        url: "/Users/DeattachBankAccount",
        data: {
            'bankAccountID': bankAccountId,
            'ID': userId
        },
        datatype: "json",
        beforeSend: loadStart,
        complete: loadStop,
        success: function (data) {
            $.each(data, function (val, text) {

                ddlBankAccounts.append(
                    $("<option></option>").val(text.Value).html(text.Text)
                );
            });

        }
    });
});
});

function loadStart() {
    $("#loading").show();
}
function loadStop() {
    $("#loading").hide();
}