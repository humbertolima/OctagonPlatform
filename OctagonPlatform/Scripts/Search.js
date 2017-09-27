

$("#searchPartner").keydown(function () {

    var searchText = $("#searchPartner").val();
    $.ajax({
        type: "Post",
        url: "Partners/Search?search=" + searchText,
        datatype: "html",
        success: function (data) {

            $("#listOfPartners").html(data);
        }
    });
});

$("#searchPartnerContact").keydown(function () {

    var searchText = $("#searchPartnerContact").val();
    $.ajax({
        type: "Post",
        url: "PartnerContacts/Search?search=" + searchText,
        datatype: "html",
        success: function (data) {

            $("#listOfPartnersContact").html(data);
        }
    });
});
$("#searchBA").keydown(function () {

    var searchText = $("#searchBA").val();
    $.ajax({
        type: "Post",
        url: "BankAccount/Search?search=" + searchText,
        datatype: "html",
        success: function (data) {

            $("#listOfBA").html(data);
        }
    });
});

$("#searchUser").keydown(function () {

    var searchText = $("#searchUser").val();
    $.ajax({
        type: "Post",
        url: "Users/Search?search=" + searchText,
        datatype: "html",
        success: function (data) {

            $("#listOfUsers").html(data);
        }
    });
});

$("#searchTerminal").keydown(function () {

    var searchText = $("#searchTerminal").val();
    $.ajax({
        type: "Post",
        url: "Terminals/Search?search=" + searchText,
        datatype: "html",
        success: function (data) {

            $("#listOfTerminals").html(data);
        }
    });
});

$("#searchTerminalContact").keydown(function () {

    var searchText = $("#searchTerminalContact").val();
    $.ajax({
        type: "Post",
        url: "TerminalContacts/Search?search=" + searchText,
        datatype: "html",
        success: function (data) {

            $("#listOfTerminalContacts").html(data);
        }
    });
});


