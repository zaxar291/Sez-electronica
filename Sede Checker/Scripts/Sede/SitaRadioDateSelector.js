return SelectCitaDateByRadioButtons(arguments[0][0], arguments[0][1], arguments[0][2]);

function SelectCitaDateByRadioButtons(baseElementClass, dateMin, dateMax) {

    if ($ == undefined) {
        if (jQuery == undefined) {
            var script = document.createElement("script");
            script.src = "https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js";
        } else {
            $ = jQuery;
        }
    }

    var baseDateMin;
    var baseDateMax;

    if (baseDateMin != "01.01.0001") {
        baseDateMin = new Date(dateMin.replace(/(\d+).(\d+).(\d+)/, '$3/$2/$1'));
    } else {
        baseDateMin = null;
    }

    if (baseDateMax != "01.01.0001") {
        baseDateMax = new Date(dateMax.replace(/(\d+).(\d+).(\d+)/, '$3/$2/$1'));
    } else {
        baseDateMax = null;
    }

    var citasList = $(baseElementClass);

    for (var i = 0; i <= citasList.length; i++) {
        var SE = $(citasList[i]).get()[0];
        if (SE != undefined) {
            if (baseDateMax != null && baseDateMin != null) {
                var citaDate = new Date(SE.childNodes[7].innerHTML.replace(/(\d+)\/(\d+)\/(\d+)/, '$3/$2/$1'));
                if (baseDateMax >= citaDate && baseDateMin <= citaDate) {
                    $(SE).get()[0].childNodes[17].click();
                    return true;
                }
            } else if (baseDateMin != null && baseDateMax == null) {
                var citaDate = new Date(SE.childNodes[7].innerHTML.replace(/(\d+)\/(\d+)\/(\d+)/, '$3/$2/$1'));
                if (baseDateMin <= citaDate) {
                    $(SE).get()[0].childNodes[17].click();
                    return true;
                }
            } else if (baseDateMin == null && baseDateMax != null) {
                var citaDate = new Date(SE.childNodes[7].innerHTML.replace(/(\d+)\/(\d+)\/(\d+)/, '$3/$2/$1'));
                if (baseDateMax >= citaDate) {
                    $(SE).get()[0].childNodes[17].click();
                    return true;
                }
            }
        }
    }
    return false;
}