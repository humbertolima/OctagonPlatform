    $("#CountryId").change(function () {
        var countriesId = $(this).val();
        $.ajax({
            url: "/populate/GetallStates",
            data: { 'CountryID': countriesId },
            
                    datatype: "json",
                    beforeSend: loadStart,
                    complete: loadStop,
                    success: function (data) {
                        var ddlState = $("#StatesId");
                        var ddlCities = $("#CitiesId");

                        ddlState.empty();
                        ddlState.append($("<option></option>").val("").html("Select..."));
                        ddlCities.empty();

                       $.each(data, function (val, text) {

        ddlState.append(
            $("<option></option>").val(text.Value).html(text.Text)
        );
    });

                    }
        });
    });

    $("#StatesId").change(function () {
                var stateId = $(this).val();
                $.ajax({
                    url: "/Populate/GetallCities",
                    data: {'StateID': stateId },
                    datatype: "json",
                    beforeSend: loadStart,
                    complete: loadStop,
                    success: function (data) {

        var ddlCities = $("#CitiesId");
                        ddlCities.empty();

        if (!$.isEmptyObject(data)) {
        ddlCities.append($("<option></option>").val("").html("Select..."));
    }

        $.each(data, function (val, text) {

        ddlCities.append(
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