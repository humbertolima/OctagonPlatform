function callankle() {
    document.location.href = "#ancla";
}
var listselected1 = [];
var listselected2 = [];
$(function () {
    callankle();
    var id = "";
    var groupselect = "";
    Clear();


    $("#CreateGroup").click(function () {

        $.post(urlcreate, $('#frmgroup').serialize(), function (data) {

            if (data.Id != undefined && data.Name != undefined) {
                $('#public-methods').selectMultiple('addOption', { value: data.Id, text: data.Name, index: 0 });
                $("#name").val("");
                $("#error").html("");
            }
            else
                $("#error").html(data);
        }, 'json');
    });
    $("#DeleteGroup").click(function () {

        $.post(urldelete, { Ids: id }, function (data) {

            $("#public-methods option[value='" + data + "']").remove();
            $("#select1 option[value ^='" + data + "_']").each(function (index, value) {
                $(this).remove();
            });
            $("#select2 option[value ^='" + data + "_']").each(function (index, value) {
                $(this).remove();
            });

            $('#select1').selectMultiple('refresh');
            $('#select2').selectMultiple('refresh');
            $('#public-methods').selectMultiple('refresh');
        }, 'json');
    });

    //Select multiple
    $('#public-methods').selectMultiple({
        selectableHeader: "<input type='text' class='form-control' autocomplete='off' placeholder='Search Group'>",
        afterInit: function (ms) {
            var that = this,
                $selectableSearch = that.$selectableUl.prev(),
                selectableSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selectable';

            that.qs1 = $selectableSearch.quicksearch(selectableSearchString)
                .on('keydown', function (e) {
                    if (e.which === 40) { // 40 es Down arrow ,cuando presione la tecla de ir abajo que coja el foco 
                        that.$selectableUl.focus();
                        return false;
                    }
                });
        },
        afterSelect: function (values) {
            id = parseInt(values, 10);

            $("#public-methods option").each(function (index, val) {
                var value = val.value;
                if (values != value) {
                    $('#public-methods').selectMultiple('deselect', value.toString());
                }
            });
            groupselect = parseInt(values, 10);
            DisplayTerminalsBygroup(parseInt(values, 10));

        },
        afterDeselect: function (values) {
            groupselect = "";
            RemoveDisplayTerminalsBygroup(values);

        }

    });
   
    $('#select1').selectMultiple({
        selectableHeader: "<input type='text' class='form-control' autocomplete='off' placeholder='Search'>",
        afterInit: function (ms) {

            var that = this,
                $selectableSearch = that.$selectableUl.prev(),
                selectableSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selectable';

            that.qs1 = $selectableSearch.quicksearch(selectableSearchString)
                .on('keydown', function (e) {
                    if (e.which === 40) { // 40 es Down arrow ,cuando presione la tecla de ir abajo que coja el foco 
                        that.$selectableUl.focus();
                        return false;
                    }
                });
        },
        afterSelect: function (values) {
            listselected1.push(values.toString());
        },
        afterDeselect: function (values) {
            var index = listselected1.indexOf(values.toString());
            if (index != -1)
                listselected1.splice(index, 1);

        }

    });
    $('#select2').selectMultiple({
        selectableHeader: "<input type='text' class='form-control' autocomplete='off' placeholder='Search'>",
        afterInit: function (ms) {
            var that = this,
                $selectableSearch = that.$selectableUl.prev(),
                selectableSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selectable';

            that.qs1 = $selectableSearch.quicksearch(selectableSearchString)
                .on('keydown', function (e) {
                    if (e.which === 40) { // 40 es Down arrow ,cuando presione la tecla de ir abajo que coja el foco 
                        that.$selectableUl.focus();
                        return false;
                    }
                });
        },
        afterSelect: function (values) {
            listselected2.push(values.toString());
        },
        afterDeselect: function (values) {
            var index = listselected2.indexOf(values.toString());
            if (index != -1)
                listselected2.splice(index, 1);

        }

    });

    //end

    //autocompleta
    $('#partner').autocomplete({
        source: urlautopartner,
        select: function (event, ui) {

            $("#partner").val(ui.item.label); // display the selected text
            $("#partnerid").val(ui.item.value); // save selected id to hidden input

            return false;
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
    $('#city').autocomplete({
        source: urlcity,
        select: function (event, ui) {

            $("#city").val(ui.item.label); // display the selected text
            $("#cityid").val(ui.item.value); // save selected id to hidden input

            return false;
        }
    });
    $('#zipcode').autocomplete({
        source: urlzipcode,
        select: function (event, ui) {

            $("#zipcode").val(ui.item.label); // display the selected text

            return false;
        }
    });
    //end
    $("#associated").click(function () {
        // alert("associated");
        var listassociated = listselected1.join(); // Convert array to string dividido por ,
       // alert(listassociated);
        if (listselected1.length > 0) {
            $.post(urlasign, { listtn: listassociated, groupSelected: groupselect, partner: $("#partnerid").val(), state: $("#stateid").val(), city: $("#cityid").val(), zipcode: $("#zipcode").val() }, function (data) {

               Display(data, groupselect);

            }, 'json');
        } else
            alert("Select Terminals");
        //alert(listassociated);
    });
    $("#unassociated").click(function () {
        // alert("unassociated");
        var listunassociated = listselected2.join();
       // alert(listunassociated);
        if (listselected2.length > 0) {
            $.post(urlunasign, { listtn: listunassociated, groupSelected: groupselect, partner: $("#partnerid").val(), state: $("#stateid").val(), city: $("#cityid").val(), zipcode: $("#zipcode").val() }, function (data) {
                              
                Display(data, groupselect);

            }, 'json');
        } else
            alert("Select Terminals");
    });
    $('#select-all').click(function () {
        $('#select1').selectMultiple('select_all');
        return false;
    });
    $('#deselect-all').click(function () {
        $('#select1').selectMultiple('deselect_all');
        return false;
    });
    $('#select-all1').click(function () {
        $('#select2').selectMultiple('select_all');
        return false;
    });
    $('#deselect-all1').click(function () {
        $('#select2').selectMultiple('deselect_all');
        return false;
    });

    //Filter
    $("#filter").click(function () {

        if (groupselect == "") alert("Select Group");
        else {
            RemoveDisplayTerminalsBygroup(groupselect);
            DisplayTerminalsBygroup(groupselect);
        }
    });
    $("#clear").click(function () {
        RemoveDisplayTerminalsBygroup(groupselect);
        Clear();
    });
});

function DisplayTerminalsBygroup(groupselected1) {
    if ($("#partner").val() == "") $("#partnerid").val("0");
    if ($("#state").val() == "") $("#stateid").val("0");
    if ($("#city").val() == "") $("#cityid").val("0");

    $.post(urlselectgroup, { groupSelected: groupselected1, partner: $("#partnerid").val(), state: $("#stateid").val(), city: $("#cityid").val(), zipcode: $("#zipcode").val() }, function (data) {
        Display(data);

    }, 'json');

}
function Display(data) {

    $("#select1 option").remove();
    $("#select2 option").remove();
    data = JSON.parse(data);
    var unassoGroup = data[0];
    var assoGroup = data[1];
    $("#groupauto").val();
    if (unassoGroup.length == 0 && assoGroup.length == 0)
        alert("No records available");
    for (var i = 0; i < unassoGroup.length; i++) {
        var name = unassoGroup[i].TerminalId + " / " + unassoGroup[i].LocationName + " / " + unassoGroup[i].Partner.BusinessName;
        // $('#select1').selectMultiple('addOption', { value: groupselected + "_" + unassoGroup[i].Id, text: name, index: 0 });  
        $('#select1').append('<option value="' + unassoGroup[i].Id + '" >' + name + '</option>');

    }
    for (var i = 0; i < assoGroup.length; i++) {
        var name = assoGroup[i].TerminalId + " / " + assoGroup[i].LocationName + " / " + assoGroup[i].Partner.BusinessName;
        // $('#select2').selectMultiple('addOption', { value: groupselected + "_" +assoGroup[i].Id, text: name, index: 0 });
        $('#select2').append('<option value="' + assoGroup[i].Id + '" >' + name + '</option>');
    }
     listselected1 = [];
     listselected2 = [];
    $('#select1').selectMultiple('refresh');
    $('#select2').selectMultiple('refresh');
}
function RemoveDisplayTerminalsBygroup(group) {

    $("option[value ^='" + group + "_']").each(function (index, value) {
        $(this).remove();
    });
    $('#select1').selectMultiple('refresh');
    $('#select2').selectMultiple('refresh');

}
function Clear() {
    $("#stateid").val("0");
    $("#partnerid").val("0");
    $("#cityid").val("0");
    $("#state").val("");
    $("#partner").val("");
    $("#city").val("");
    $("#zipcode").val("");
    groupselect = "";
}