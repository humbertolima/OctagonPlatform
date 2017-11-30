$(function () {

    var listid = [];

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

        $.post(urldelete, { Ids: listid.toString() }, function (data) {

            for (var i = 0; i < data.length; i++) {
                $("#public-methods option[value='" + data[i] + "']").remove();
            }
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
            listid.push(parseInt(values, 10));
            DisplayTerminalsBygroup(parseInt(values, 10));
        },
        afterDeselect: function (values) {
            var index = listid.indexOf(parseInt(values));
            if (index > -1) {
                listid.splice(index, 1);
            }

        }

    });
    
    $('#select-all').click(function () {
        $('#public-methods').selectMultiple('select_all');
        return false;
    });
    $('#deselect-all').click(function () {
        $('#public-methods').selectMultiple('deselect_all');
        return false;
    });

    $('#select1').selectMultiple({

        afterSelect: function (values) {
            listid.push(parseInt(values, 10));
        },
        afterDeselect: function (values) {
            var index = listid.indexOf(parseInt(values));
            if (index > -1) {
                listid.splice(index, 1);
            }

        }

    });
    $('#select2').selectMultiple({

        afterSelect: function (values) {
            listid.push(parseInt(values, 10));
        },
        afterDeselect: function (values) {
            var index = listid.indexOf(parseInt(values));
            if (index > -1) {
                listid.splice(index, 1);
            }

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
    $('#groupauto').autocomplete({
        source: urlautogroup,
        select: function (event, ui) {

            $("#groupauto").val(ui.item.label); // display the selected text
            $("#groupid").val(ui.item.value); // save selected id to hidden input

            return false;
        }
    });
    //end

});
function DisplayTerminalsBygroup(groupselected,namegroup)
{
  
    $.post(urlselectgroup, { groupSelected: groupselected, partner: $("#partnerid").val() }, function (data) {
        data = JSON.parse(data);
       
        var unassoGroup = data[0];
        var assoGroup = data[1];
        $("#groupauto").val();
        for (var i = 0; i < unassoGroup.length; i++) {
            var name = unassoGroup[i].TerminalId +" / "+ unassoGroup[i].LocationName +" / "+ unassoGroup[i].Partner.BusinessName;
            $('#select1').selectMultiple('addOption', { value: unassoGroup[i].Id, text: name, index: 0 });          
        }
        for (var i = 0; i < assoGroup.length; i++) {
            var name = assoGroup[i].TerminalId + " / " + assoGroup[i].LocationName + " / " + assoGroup[i].Partner.BusinessName;
            $('#select1').selectMultiple('addOption', { value: assoGroup[i].Id, text: name, index: 0 });
        }
       
    }, 'json');
   
}