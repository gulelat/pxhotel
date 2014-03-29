
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
        s += '<option value="' + item.Value + '"'+ (item.Selected ? "selected" : "") + '>' + item.Text + '</option>';
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
    var centerClass = "gritter-center ";
    if (center == null || center)
        centerClass = "";
        
    $.gritter.add({
        time: 50000,
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


//it causes some flicker when reloading or navigating grid
//it may be possible to have some custom formatter to do this as the grid is being created to prevent this
//or go back to default browser checkbox styles for the grid
function styleCheckbox(table) {
    $(table).find('input:checkbox').addClass('ace')
        .wrap('<label />')
        .after('<span class="lbl align-top" />');

    $('.ui-jqgrid-labels th[id*="_cb"]:first-child')
    .find('input.cbox[type=checkbox]').addClass('ace')
    .wrap('<label />').after('<span class="lbl align-top" />');
}

//unlike navButtons icons, action icons in rows seem to be hard-coded
//you can change them like this in here if you want
function updateActionIcons(table) {
    /*
    var replacement = 
    {
        'ui-icon-pencil' : 'icon-pencil blue',
        'ui-icon-trash' : 'icon-trash red',
        'ui-icon-disk' : 'icon-ok green',
        'ui-icon-cancel' : 'icon-remove red'
    };
    $(table).find('.ui-pg-div span.ui-icon').each(function(){
        var icon = $(this);
        var $class = $.trim(icon.attr('class').replace('ui-icon', ''));
        if($class in replacement) icon.attr('class', 'ui-icon '+replacement[$class]);
    });
    */
}

//replace icons with FontAwesome icons like above
function updatePagerIcons(table) {
    var replacement =
        {
            'ui-icon-seek-first': 'icon-double-angle-left bigger-140',
            'ui-icon-seek-prev': 'icon-angle-left bigger-140',
            'ui-icon-seek-next': 'icon-angle-right bigger-140',
            'ui-icon-seek-end': 'icon-double-angle-right bigger-140'
        };
    $('.ui-pg-table:not(.navtable) > tbody > tr > .ui-pg-button > .ui-icon').each(function () {
        var icon = $(this);
        var $class = $.trim(icon.attr('class').replace('ui-icon', ''));

        if ($class in replacement) icon.attr('class', 'ui-icon ' + replacement[$class]);
    });
}

function enableTooltips(table) {
    $('.navtable .ui-pg-button').tooltip({ container: 'body' });
    $(table).find('.ui-pg-div').tooltip({ container: 'body' });
}