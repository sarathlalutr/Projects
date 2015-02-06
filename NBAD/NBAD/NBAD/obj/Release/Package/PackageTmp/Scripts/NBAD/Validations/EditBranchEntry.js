$(document).ready(function () {
});

function EditBranchEntry() {
    if ($("#txtBranchID").val() == "") {
        showOverAlert('Please enter Branch ID', 'error', 'bottom');
        $("#txtBranchID").focus();
        return false;
    }
    else if ($("#txtBranchName").val() == "") {
        showOverAlert('Please enter Branch Name', 'error', 'bottom');
        $("#txtBranchName").focus();
        return false;
    }

    return true;
}