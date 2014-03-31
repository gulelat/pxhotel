//Grid Functions
var gridSelector = gridSelector || "";
var lastSel = lastSel || 0;
var pagerSelector = pagerSelector || "";
var editFormPlusData = editFormPlusData || {};
$(function () {
    //Register default ondblClickRow
    //This is used for inline editing success post event
    $(gridSelector).jqGrid('setGridParam', {
        ondblClickRow: function (rowId) {
            //Only 1 row edit at a time
            if (rowId && rowId !== lastSel) {
                jQuery(gridSelector).restoreRow(lastSel);
                lastSel = rowId;
            }
            //Double click will open inline editing 
            jQuery(gridSelector).jqGrid("editRow", rowId, {
                keys: true,
                extraparam: editFormPlusData,
                //Success event catching
                successfunc: function (response) {
                    var res = jQuery.parseJSON(response.responseText);
                    ShowMessage(res);
                    return res.Success;
                }
            });
        }
    });
    //navButtons
    jQuery(gridSelector).jqGrid('navGrid', pagerSelector,
        {
            edit: true,
            editicon: 'icon-pencil blue',
            add: true,
            addicon: 'icon-plus-sign purple',
            del: true,
            delicon: 'icon-trash red',
            search: true,
            searchicon: 'icon-search orange',
            refresh: true,
            refreshicon: 'icon-refresh green',
            view: true,
            viewicon: 'icon-zoom-in grey'
        },
        {
            //edit record form
            closeAfterEdit: true,
            recreateForm: true,
            viewPagerButtons: false,
            beforeShowForm: function (e) {
                var form = $(e[0]);
                form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />');
                styleEditForm(form);
            },
            onclickSubmit: function (params, postData) {
                postData = $.extend({}, postData, editFormPlusData);
                return postData;
            },
            afterSubmit: function (response) {
                var res = jQuery.parseJSON(response.responseText);
                if (res.Success) {
                    return [true, res.Message];
                }
                else {
                    return [false, res.Message];
                }
            }
        },
        {
            //new record form
            closeAfterAdd: true,
            recreateForm: true,
            viewPagerButtons: false,
            beforeInitData: function () {
                jQuery(gridSelector).restoreRow(lastSel);
                $(gridSelector).resetSelection();
                if ($.isFunction(window["initCreateData"])) {
                    window["initCreateData"]();
                }
            },
            beforeShowForm: function (e) {
                var form = $(e[0]);
                form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />');
                styleEditForm(form);
            },
            onclickSubmit: function (params, postData) {
                postData = $.extend({}, postData, editFormPlusData);
                postData = $.extend({}, postData, { id: 0 });
                return postData;
            },
            afterSubmit: function (response) {
                var res = jQuery.parseJSON(response.responseText);
                if (res.Success) {
                    return [true, res.Message];
                }
                else {
                    return [false, res.Message];
                }
            }
        },
        {
            //delete record form
            recreateForm: true,
            beforeShowForm: function (e) {
                var form = $(e[0]);
                if (form.data('styled')) return false;

                form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />');
                styleDeleteForm(form);

                form.data('styled', true);
            },
            onclickSubmit: function (params, postData) {
                postData = $.extend({}, postData, editFormPlusData);
                return postData;
            },
            afterSubmit: function (response) {
                var res = jQuery.parseJSON(response.responseText);
                if (res.Success) {
                    return [true, res.Message];
                }
                else {
                    return [false, res.Message];
                }
            }
        },
        {
            //search form
            recreateForm: true,
            afterShowSearch: function (e) {
                var form = $(e[0]);
                form.closest('.ui-jqdialog').find('.ui-jqdialog-title').wrap('<div class="widget-header" />');
                styleSearchForm(form);
            },
            afterRedraw: function () {
                styleSearchFilters($(this));
            },
            multipleSearch: true,
            /**
            multipleGroup:true,
            showQuery: true
            */
        },
        {
            //view record form
            recreateForm: true,
            beforeShowForm: function (e) {
                var form = $(e[0]);
                form.closest('.ui-jqdialog').find('.ui-jqdialog-title').wrap('<div class="widget-header" />');
            }
        }
    );
});

function formatSelectCell(value) {
    return value ? value.replace(/^-+/, '') : "";
}

//switch element when editing inline
function aceSwitch(cellvalue, options, cell) {
    setTimeout(function () {
        $(cell).find('input[type=checkbox]')
            .wrap('<label class="inline" />')
            .addClass('ace ace-switch ace-switch-5')
            .after('<span class="lbl"></span>');
    }, 0);
}
//enable datepicker
function pickDate(cellvalue, options, cell) {
    setTimeout(function () {
        $(cell).find('input[type=text]')
            .datepicker({ format: 'yyyy-mm-dd', autoclose: true });
    }, 0);
}

function styleEditForm(form) {
    //enable datepicker on "sdate" field and switches for "stock" field
    form.find('input[name=sdate]').datepicker({ format: 'yyyy-mm-dd', autoclose: true })
        .end().find('input[name=stock]')
        .addClass('ace ace-switch ace-switch-5').wrap('<label class="inline" />').after('<span class="lbl"></span>');

    //update buttons classes
    var buttons = form.next().find('.EditButton .fm-button');
    buttons.addClass('btn btn-sm').find('[class*="-icon"]').remove();//ui-icon, s-icon
    buttons.eq(0).addClass('btn-primary').prepend('<i class="icon-ok"></i>');
    buttons.eq(1).prepend('<i class="icon-remove"></i>');

    buttons = form.next().find('.navButton a');
    buttons.find('.ui-icon').remove();
    buttons.eq(0).append('<i class="icon-chevron-left"></i>');
    buttons.eq(1).append('<i class="icon-chevron-right"></i>');
}

function styleDeleteForm(form) {
    var buttons = form.next().find('.EditButton .fm-button');
    buttons.addClass('btn btn-sm').find('[class*="-icon"]').remove();//ui-icon, s-icon
    buttons.eq(0).addClass('btn-danger').prepend('<i class="icon-trash"></i>');
    buttons.eq(1).prepend('<i class="icon-remove"></i>');
}

function styleSearchFilters(form) {
    form.find('.delete-rule').val('X');
    form.find('.add-rule').addClass('btn btn-xs btn-primary');
    form.find('.add-group').addClass('btn btn-xs btn-success');
    form.find('.delete-group').addClass('btn btn-xs btn-danger');
}

function styleSearchForm(form) {
    var dialog = form.closest('.ui-jqdialog');
    var buttons = dialog.find('.EditTable');
    buttons.find('.EditButton a[id*="_reset"]').addClass('btn btn-sm btn-info').find('.ui-icon').attr('class', 'icon-retweet');
    buttons.find('.EditButton a[id*="_query"]').addClass('btn btn-sm btn-inverse').find('.ui-icon').attr('class', 'icon-comment-alt');
    buttons.find('.EditButton a[id*="_search"]').addClass('btn btn-sm btn-purple').find('.ui-icon').attr('class', 'icon-search');
}

function beforeDeleteCallback(e) {
    var form = $(e[0]);
    if (form.data('styled')) return false;

    form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />');
    styleDeleteForm(form);

    form.data('styled', true);
}

function beforeEditCallback(e) {
    var form = $(e[0]);
    form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />');
    styleEditForm(form);
}


