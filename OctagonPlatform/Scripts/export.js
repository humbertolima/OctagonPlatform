
function Export(filename, mytblId, idhead) {

    var html, link, blob, url, css,cssexcel,headw;
    html = "<div id = 'table'>" + $("#" + mytblId).html() + "</div>";
    headw = "<div id = 'head'>" + $("#" + idhead).html() + "</div>";
    cssexcel = '#table table { border-collapse: collapse;}'+
         '#table td, #table th {text-align:left } ' +
        '#table tr td,#table tr th {border: solid #e9ecef 1pt;width: 93.5pt; }' +
        '#table tr th { background: #e9ecef}'

        ;
    css = (
        '<style>' +
        '@page WordSection1{size: 841.95pt 595.35pt;mso-page-orientation: landscape;}' +
        'div.WordSection1 {page: WordSection1;}' +
        cssexcel +
        '</style>'
    );   
   
    var head = '< html xmlns: o =\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\">    <head><meta http-equiv=\"Content-type\" content=\"text/html;charset=utf-8\" /> <style>' + cssexcel + '</style></head>   <body>'+headw+html+"</body ></html >";
    var ext = filename.split(".");

    if (ext[1] == "doc") {
        blob = new Blob(['\ufeff', css +headw+ html], {
            type: 'application/msword'
        });
    }
    if (ext[1] == "xls") {
        blob = new Blob(['\ufeff', head], {
            type: 'application/vnd.ms-excel;charset=utf-8,'
        });
    }
    url = URL.createObjectURL(blob);
    link = document.createElement('A');
    link.href = url;
    link.download = filename;  // default name without extension 
    document.body.appendChild(link);
    if (navigator.msSaveOrOpenBlob) navigator.msSaveOrOpenBlob(blob, filename); // IE10-11
    else link.click();  // other browsers
    document.body.removeChild(link);
};
function ExportPdf(filename, mytblId, idhead) {
   
        var html1 = "<div id = 'head'>" + $("#" + idhead).html() + "</div>" + "<div id = 'table'>" + $("#" + mytblId).html() + "</div>";
        $("input[name='html']").val(html1);
   
}