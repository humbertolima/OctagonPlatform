$(function () {


    // callankle();
    $("#ReportId").change(function () {
        var idreport = $(this).val();
        GetPartialFilter(idreport);
    });

    $('#user').autocomplete({
        source: url,
        select: function (event, ui) {

            $("#user").val(ui.item.label); // display the selected text
            $("#userid").val(ui.item.value); // save selected id to hidden input
            $.get(list, { userId: $("#userid").val() }, function (data) {

                $("#SubscriptionList").html(data);
            });
            return false;
        }
    });
    $('#user').blur(function () {
        if ($('#user').val() == "") {
            $('#user').attr("placeholder", "< ALL >");
            $('#userid').val('');

            $.get(list, { userId: 0 }, function (data) {
                // alert(data);
                $("#SubscriptionList").html(data); 
            });
        }

    });
    $('#FormSub').submit(function () {
        // var formData = $('#FormSub').serializeObject();
      
        $("#userId").val($("#userid").val());
        $("#message").html("<div class='loader'></div>");
        if ($("#ReportId").val() != "" && $("#ScheduledId").val() != "") {
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (data) {
                    //alert(data);
                    var isSuccessful = (data['success']);

                    if (isSuccessful) {

                        window.location.href = url2 + "&b=" + $("#userid").val();
                    }
                    else {

                        var errors = data['errors'];
                        $("#message").html(' <div class="alert alert-danger"><strong>' + errors + '</strong> </div>');
                    }



                },
                error: function (xhr, status, error) {
                    // Show the error
                    alert(xhr.responseText);

                }
            });
        } else {
            if ($("#ReportId").val() == "")
                $("#message").html(' <div class="alert alert-danger"><strong>Please Select Report</strong> </div>');
            else
                if ($("#ScheduledId").val() == "")
                    $("#message").html(' <div class="alert alert-danger"><strong>Please Select Schedule</strong> </div>');
        }

        return false;
    });
    $("#ScheduledId").change(function () {
        $.get(url_details_schedule, { id: $(this).val() }, function (data) {
            $("#schedule_details").html(data);
          
        });

    });

});
function SuccessEdit(data, itemid) {
   
   
    $("#subId").val(itemid);
    $("#PartialFilter").html("<div class='loader'></div>");
    $.get(filterreport2, {  idsub: itemid  }, function (data) {
        //alert(data);
        $("#PartialFilter").html(data);
        $("#Start_Date").attr("name", "StartDate");
    });
    
}
function GetPartialFilter(idreport) {
    
    $("#PartialFilter").html("<div class='loader'></div>");
    $.get(filterreport, { id: idreport }, function (data) {

        $("#PartialFilter").html(data);
        $("#Start_Date").attr("name", "StartDate");
    });
}