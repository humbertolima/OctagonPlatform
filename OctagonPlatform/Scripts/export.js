
function Export(filename, idhead,idtable,orientation) {

    var html, link, blob, url, css, cssexcel, headw = '';
    var ahtml = $('<div></div>').append($("#" + idtable).clone()).html();
    
    html = "<div id = 'table'>" + ahtml + "</div>";
    if (idhead != '' )
        headw = "<div id = 'head'>" + $("#" + idhead).html() + "</div>";   

    cssexcel = '#table table { border-collapse: collapse;}'+
         '#table td, #table th {text-align:left } ' +
        '#table tr td,#table tr th {border: solid black 1pt;border:none;border-bottom:solid windowtext 1.0pt; padding: .75pt .75pt .75pt .75pt; }' +
        '#table tr th { background: #f9f9f9;font-size:10pt; height: 18.6pt}' +
        '#table tr td {font-size:10pt;}'

        ;
    var size = "8.5in 11.0in";
    if (orientation == "Landscape")
        size = "11.0in 8.5in";
    css = '@page WordSection1{size:'+size+';margin: .5in .5in .5in .5in;}' +
        'div.WordSection1 {page: WordSection1;}' +  cssexcel  ;   
   
    var htmlexcel = '< html xmlns: o =\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\">    <head><meta http-equiv=\"Content-type\" content=\"text/html;charset=utf-8\" /> <style>' + cssexcel + '</style></head>   <body>' + headw + html + "</body ></html >";
   
    var htmlword = '<html>  <head>  <meta http-equiv=Content- Type content="text/html; charset=windows-1252"> <meta name=Generator content="Microsoft Word 15 (filtered)">  <style>' + css + '</style></head>   <body><div class=WordSection1>' + headw + html + "</div ></body ></html >";
    var ext = filename.split(".");

    if (ext[1] == "doc") {
        blob = new Blob(['\ufeff', htmlword], {
            type: 'application/msword'
        });
    }
    if (ext[1] == "xls") {
        blob = new Blob(['\ufeff', htmlexcel], {
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
function ExportPdf(idhead,idtable) {
    
    var css = '<style>.mitable table { border-collapse: collapse;}' +
        '.mitable td,.mitable  th {text-align:left } ' +
        '.mitable tr td,.mitable tr th {border-bottom:solid black 0.5pt; padding: .75pt .75pt .75pt .75pt; }' +
        '.mitable tr td {font-size:10pt;background: white}'+
        '.mitable tr th { background: #f9f9f9;font-size:10pt; height: 18.6pt}</style>';
    var ahtml = $('<div></div>').append($("#" + idtable).clone()).html();
    var head = $("#" + idhead).html().replace('height="82">','height="82"/>');
    var html1 = css + "<div id ='head'>" + head + "</div>" + "<div id ='table' class='mitable'>" + ahtml + "</div>";
   
    $("input[name='html']").val(html1);
   
}
function callankle() {
    document.location.href = "#ancla";
}
function callankle2() {
    document.location.href = "#ancla2";
}