
function ExportToExcel(filename, mytblId,idhead) {
  
    var html2 = $("#" + idhead).html() + $("#" + mytblId).html();
    var element = document.createElement('a');
    element.setAttribute('href', 'data:html;charset=utf-8,' + encodeURIComponent(html2));
    element.setAttribute('download', filename);

    element.style.display = 'none';
    document.body.appendChild(element);

    element.click();

    document.body.removeChild(element);
}
