

    $("#search").keydown( function() {

        var searchText = $("#search").val();
        $.ajax({
            type: "Post",
            url: "Partners/Search?search=" + searchText,
            datatype: "html",
            success: function(data) {

                $("#listOfPartners").html(data);
            }
    });
    });
