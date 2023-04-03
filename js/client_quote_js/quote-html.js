$(document).ready(function () {
    $('input[type="checkbox"]').on('change', function () {
        $('input[name="' + this.name + '"]').not(this).prop('checked', false);
    });
});

function closeWindow(status, type, msg) {
    if (status == "ERROR") {
        if (msg == "")
            msg = "Some problem, try again later!";

        $.msgBox({ title: "Quote Details", content: msg, type: "error" });
    }
    else {
        $.msgBox({
            title: "Quote Details",
            content: msg, type: "info",
        });
    }
}

function validate_tu() {
    var sRequest = "";
    $(".special-request .list-border").each(function (i, oPax) {
        sPaxName = "";
        sPaxName += $(".meal input:checked", oPax).length > 0 ? "Meal: " + $(".meal input:checked", oPax).val() + "," : "";
        sPaxName += $(".seat input:checked", oPax).length > 0 ? "Seat: " + $(".seat input:checked", oPax).val() + "," : "";
        sPaxName += $(".wheelchair input:checked", oPax).length > 0 ? "Wheelchair: " + $(".wheelchair input:checked", oPax).val() + "," : "";
        sPaxName += $("input.freqflyer", oPax).val() != "" ? "Frequent Flyer Number: " + $("input.freqflyer", oPax).val() + "," : "";
        if (sPaxName.length > 0)
            sRequest += $(oPax).attr("data-ref") + "^" + sPaxName.substr(0, sPaxName.length - 1) + "^^";
    });

    $("[id$=txtRemarks]").val(sRequest);
    
    $.msgBox({
        type: "confirm", title: "Quote Details", content: "Kindly confirm to approve?",
        buttons: [{ value: "Yes" }, { value: "No" }], success: function (result) {
            if (result == "Yes") {
                //$('[id$=btnApprove]').click();
                submit_quote();
            }
        }
    });
    return false;
}