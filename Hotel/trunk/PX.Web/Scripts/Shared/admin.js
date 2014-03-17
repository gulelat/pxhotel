
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