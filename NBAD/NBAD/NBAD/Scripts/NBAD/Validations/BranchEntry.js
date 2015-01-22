$(document).ready(function () {
});

function BranchEntry() {
    if ($("#ContentPlaceHolder1_txtBranchID").val() == "") {
        showAlert('Please enter Branch ID', 'error', 'bottom');
        $("#ContentPlaceHolder1_txtBranchID").focus();
        return false;
    }
    else if ($("#ContentPlaceHolder1_txtBranchName").val() == "") {
        showAlert('Please enter Branch Name', 'error', 'bottom');
        $("#ContentPlaceHolder1_txtBranchName").focus();
        return false;
    }

}