$(document).ready(function () {
});

function EditdetailsEntry() {
    if ($("#txtEmployeeId").val() == "") {
        showOverAlert('Please enter Employee ID', 'error', 'bottom');
        $("#txtEmployeeId").focus();
        return false;
    }
    else if ($("#txtEmployeeName").val() == "") {
        showOverAlert('Please enter Employee Name', 'error', 'bottom');
        $("#txtEmployeeName").focus();
        return false;
    }
    else if ($("#drpDesignation").val() == "-select-") {
        showOverAlert('Please select Designation', 'error', 'bottom');
        $("#drpDesignation").focus();
        return false;
    }
    else if ($("#drpDescription").val() == "-select-") {
        showOverAlert('Please select Description', 'error', 'bottom');
        $("#drpDescription").focus();
        return false;
    }
    else if ($("#drpBranch").val() == "-select-") {
        showOverAlert('Please select Branch', 'error', 'bottom');
        $("#drpBranch").focus();
        return false;
    }
    else if ($("#drpDepartment").val() == "-select-") {
            showOverAlert('Please select Department', 'error', 'bottom');
            $("#drpDepartment").focus();
            return false;
    }
    else if ($("#txtSwipeInTime").val() == "") {
            showOverAlert('Please select Swipe In Time', 'error', 'bottom');
            $("#txtSwipeInTime").focus();
            return false;
    }
    else if ($("#drpSwipeInLocation").val() == "-select-") {
            showOverAlert('Please select Swipe In Location', 'error', 'bottom');
            $("#drpSwipeInLocation").focus();
            return false;
    }
    else if ($("#txtSwipeOutTime").val() == "") {
            showOverAlert('Please select Swipe Out Time', 'error', 'bottom');
            $("#txtSwipeOutTime").focus();
            return false;
    }
    else if ($("#drpSwipeOutLocation").val() == "-select-") {
        showOverAlert('Please select Swipe Out Location', 'error', 'bottom');
        $("#drpSwipeOutLocation").focus();
        return false;
    }
    else if ($("#txtAprovedBy").val() == "") {
        showOverAlert('Please enter approver', 'error', 'bottom');
        $("#txtAprovedBy").focus();
        return false;
    }
    else if ($("#txtApprovedDate").val() == "") {
        showOverAlert('Please enter approved Date', 'error', 'bottom');
        $("#txtApprovedDate").focus();
        return false;
    }
}