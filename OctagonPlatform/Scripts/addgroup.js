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

    $('#select-all').click(function () {
        $('#public-methods').selectMultiple('select_all');
        return false;
    });
    $('#deselect-all').click(function () {
        $('#public-methods').selectMultiple('deselect_all');
        return false;
    });


    //end

    //SelectMulti
    $('#callbacks').multiSelect({
        afterSelect: function (values) {
            alert("Select value: " + values);
        },
        afterDeselect: function (values) {
            alert("Deselect value: " + values);
        }
    });
    //end

});