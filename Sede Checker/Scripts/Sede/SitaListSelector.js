return SelectElementInList(arguments[0][0], arguments[0][1]);

function SelectElementInList(elementId, selectable)
{
	alert(selectable.indexOf("!"));
    if (selectable.indexOf("!") !== -1) {
        return SelectBySegment(elementId, selectable.replace("!", ""));
    } else if (selectable.indexOf("||") !== -1) {
        return SelectFromArray(elementId, selectable.split("||"));
    } else {
        return SelectByDefault(elementId, selectable);
    }
}

function SelectBySegment(elementId, selectable) {
    var elementsList = document.getElementById(elementId).options;
    for (var i in elementsList) {
        if (elementsList[i].innerHTML == undefined) {
            continue;
        }
        if (elementsList[i].innerHTML.toLowerCase().indexOf(selectable.toLowerCase()) + 1) {
            elementsList[i].selected = true;
            return true;
        }
    }
    return false;
}

function SelectFromArray(elementId, elementsArray) {
    var elementsList = document.getElementById(elementId).options;
    for (var i in elementsList) {
        if (elementsList[i].innerHTML == undefined) {
            continue;
        }
        for (var i in elementsArray) {
            if (elementsList[i].innerHTML.toLowerCase().indexOf(elementsArray[i].toLowerCase()) + 1) {
                elementsList[i].selected = true;
                return true;
            }
        }
    }
    return false;
}

function SelectByDefault(elementId, selectable) {
    var elementsList = document.getElementById(elementId).options;
    for (var i in elementsList) {
        if (elementsList[i].innerHTML == undefined) {
            continue;
        }
        if (elementsList[i].innerHTML.toLowerCase().indexOf(selectable.toLowerCase()) + 1) {
            elementsList[i].selected = true;
            return true;
        }
    }
    return true;
}
