return InvokeMemberClick(arguments[0][0]);

function InvokeMemberClick(element) {
    if ($ == undefined) {
        if (jQuery == undefined) {
            var script = document.createElement("script");
            script.src = "https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js";
        } else {
            $ = jQuery;
        }
    }
    $(element).click();
    return true;
}