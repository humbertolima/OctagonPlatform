

    $("#searchPartner").keydown( function() {

        var searchText = $("#searchPartner").val();
        $.ajax({
            type: "Post",
            url: "Partners/Search?search=" + searchText,
            datatype: "html",
            success: function(data) {

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
