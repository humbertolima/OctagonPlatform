$("#BankAccounts").click(function () {
    
    $.ajax({
        type: "Post",
        url: "/Users/GetAllBankAccount",
        datatype: "html",
        success: function (data) {
            var ddlBankAccount = $("#BankAccounts");
            console.log(data);
            $.each(data, function (val, text) {
                
                ddlBankAccount.append(
                    $("<option></option>").val(text.Value).html(text.Text)
                );
            });
        }
    });
});

$("#BankAccounts").click(function () {

    $.ajax({
        url: "/Users/GetAllBankAccount",
        datatype: "json",
        beforeSend: loadStart,
        complete: loadStop,
        success: function (data) {
            var ddlBankAccounts = $("#BankAccounts");
            
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