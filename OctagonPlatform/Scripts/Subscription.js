$(function () {

    
    // callankle();
    $("#ReportId").change(function () {
        var idreport = $(this).val();
        GetPartialFilter(idreport);
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
        $('#message').fadeIn('slow', function () {
            $('#message').fadeOut(10000);
        });
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