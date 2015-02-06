$(document).ready(function () {
});

function EditLocationEntry() {
    if ($("#txtLocation").val() == "") {
        showOverAlert('Please enter Location', 'error', 'bottom');
        $("#txtLocation").focus();
        return false;
    }

}