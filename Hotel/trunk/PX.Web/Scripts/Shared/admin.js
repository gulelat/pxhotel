
//Build dropdown from json object
function buildDropdown(response, hasDefaultSelect, name) {
    if (name == null) {
        name = "One";
    }
    if (hasDefaultSelect == null) {
        hasDefaultSelect = true;
    }
    var data = typeof response === "string" ?
        $.parseJSON(response) : response;
    var s = "<select>";
    if (hasDefaultSelect)
        s += '<option value="">--Select ' + name + '--</option>';
    $.each(data, function (i, item) {
        s += '<option value="' + item.Value + '">' + item.Text + '</option>';
    });
    return s + "</select>";
}

//Load spinner
var opts = {
    lines: 13, // The number of lines to draw
    length: 20, // The length of each line
    width: 10, // The line thickness
    radius: 30, // The radius of the inner circle
    corners: 1, // Corner roundness (0..1)  
};

$(function() {
    var target = document.getElementById('spinner-preview');
    var spinner = new Spinner(opts).spin(target);
});

function showLoading() {
    $("#spinner-preview").show();
}
function hideLoading() {
    $("#spinner-preview").hide();
}

//Show message by reponse data
function ShowMessage(response, center) {
    if (response.Success) {
        ShowSuccessMessage(response.Message, center);
    }
    else {
        ShowErrorMessage(response.Message, center);
    }
}

//Show success message
function ShowSuccessMessage(message, center) {
    var centerClass = "";
    if (center == null || center)
        centerClass = "gritter-center ";
        
    $.gritter.add({
        time: 2000,
        title: 'Message',
        text: message,
        class_name: centerClass + 'gritter-info'
    });
}

//Show error message
function ShowErrorMessage(message, center) {
    var centerClass = "";
    if (center == null || center)
        centerClass = "gritter-center ";
    $.gritter.add({
        time: 2000,
        title: 'Error',
        text: message,
        class_name: centerClass + 'gritter-error'
    });
}

//Show warning message
function ShowWarningMessage(message, center) {
    var centerClass = "";
    if (center == null || center)
        centerClass = "gritter-center ";
    $.gritter.add({
        time: 2000,
        title: 'Warning',
        text: message,
        class_name: centerClass + 'gritter-warning'
    });
}