$(function () {
    $('#partner').autocomplete({
        source: urlautopartner,
        select: function (event, ui) {

            $("#partner").val(ui.item.label); // display the selected text
            $("#partnerid").val(ui.item.value); // save selected id to hidden input

            return false;
        }
    });

    //clear id from hidden field if input is blank
    $('#partner').blur(function () {
        if ($('#partner').val() == '') {
            $('#partnerid').val('');
        }
    });
    $('#scheduled').autocomplete({
        source: urlautoschedule,
        select: function (event, ui) {

            $("#scheduled").val(ui.item.label); // display the selected text
            $("#scheduledid").val(ui.item.value); // save selected id to hidden input

            return false;
        }
    });

    //clear id from hidden field if input is blank
    $('#scheduled').blur(function () {
        if ($('#scheduled').val() == '') {
            $('#scheduledid').val('');
        }
    });
    
    $('#state').autocomplete({
        source: urlstate,
        select: function (event, ui) {

            $("#state").val(ui.item.label); // display the selected text
            $("#stateid").val(ui.item.value); // save selected id to hidden input

            return false;
        }
    });
    //clear id from hidden field if input is blank
    $('#state').blur(function () {
        if ($('#state').val() == '') {
            $('#stateid').val('');
        }
    });
    $('#city').autocomplete({
        source: urlcity,
        select: function (event, ui) {

            $("#city").val(ui.item.label); // display the selected text
            $("#cityid").val(ui.item.value); // save selected id to hidden input

            return false;
        }
    });
    //clear id from hidden field if input is blank
    $('#city').blur(function () {
        if ($('#city').val() == '') {
            $('#cityid').val('');
        }
    });
    $('#zipcode').autocomplete({
        source: urlzipcode,
        select: function (event, ui) {

            $("#zipcode").val(ui.item.label); // display the selected text

            return false;
        }
    });

    //Cash Load Report
    $('#terminal').autocomplete({
        source: urlautoterminal
    });
   
    //Auto group
    $('#group').autocomplete({
        source: urlautogroup,
        select: function (event, ui) {

            $("#group").val(ui.item.label); // display the selected text
            $("#groupid").val(ui.item.value); // save selected id to hidden input

            return false;
        }
    });
    //clear id from hidden field if input is blank
    $('#group').blur(function () {
        if ($('#group').val() == '') {
            $('#groupid').val('');
        }
    });

    $('#account').autocomplete({
        source: urlautoaccount,
        select: function (event, ui) {

            $("#account").val(ui.item.label); // display the selected text
            $("#accountid").val(ui.item.value); // save selected id to hidden input

            return false;
        }
    });
    //clear id from hidden field if input is blank
    $('#account').blur(function () {
        if ($('#account').val() == '') {
            $('#accountid').val('');
        }
    });
    $('#user').autocomplete({
        source: urluser,
        select: function (event, ui) {

            $("#user").val(ui.item.label); // display the selected text
            $("#userid").val(ui.item.value); // save selected id to hidden input
          
            return false;
        }
    });
    $('#user').blur(function () {
        if ($('#user').val() == "") {
            $('#userid').val('');
         
        }

    });

    $('#report').autocomplete({
        source: urlautoreport,
        select: function (event, ui) {

            $("#report").val(ui.item.label); // display the selected text
            $("#reportid").val(ui.item.value); // save selected id to hidden input

            return false;
        }
    });
    $('#report').blur(function () {
        if ($('#report').val() == "") {
            $('#reportid').val('');

        }

    });
});
