return SelectCitaDateByCalendar();

function SelectCitaDateByCalendar() {
    var CalendarPlace = document.getElementById('VistaMapa_Datatable').childNodes[3].childNodes;
    for (var i in CalendarPlace) {
        if (CalendarPlace[i].childNodes != undefined && CalendarPlace[i].childNodes.length > 0) {
            if (CalendarPlace[i].childNodes[3].innerText.replace(/\s+/g, '') == 'LIBRE') {
                CalendarPlace[i].childNodes[3].children[0].click();
                return true;
            }
        }
    }
}