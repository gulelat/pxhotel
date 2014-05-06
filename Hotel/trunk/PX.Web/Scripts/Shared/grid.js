//Grid Functions
var gridSelector = gridSelector || "";
var lastSel = lastSel || 0;
var pagerSelector = pagerSelector || "";
var editFormPlusData = editFormPlusData || {};
var navButtonsSetup = navButtonsSetup || {};
$(function () {
    //Register default ondblClickRow
    //This is used for inline editing success post event
    $(gridSelector).jqGrid('setGridParam', {
        ondblClickRow: function (rowId) {
            if (typeof navButtonsSetup.enableEdit === 'undefined' ? true : navButtonsSetup.enableEdit) {
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
        }
    });
    //navButtons
    jQuery(gridSelector).jqGrid('navGrid', pagerSelector,
        {
            edit: typeof navButtonsSetup.enableEdit === 'undefined' ? true : navButtonsSetup.enableEdit,
            editicon: 'fa fa-pencil blue',
            add: typeof navButtonsSetup.enableCreate === 'undefined' ? true : navButtonsSetup.enableCreate,
            add: navButtonsSetup.enableCreate,
            addicon: 'fa fa-plus-square purple',
            del: typeof navButtonsSetup.enableDelete === 'undefined' ? true : navButtonsSetup.enableDelete,
            delicon: 'fa fa-trash-o red',
            search: typeof navButtonsSetup.enableSearch === 'undefined' ? true : navButtonsSetup.enableSearch,
            searchicon: 'fa fa-search orange',
            refresh: typeof navButtonsSetup.enableRefresh === 'undefined' ? true : navButtonsSetup.enableRefresh,
            refreshicon: 'fa fa-refresh green',
            view: typeof navButtonsSetup.enableView === 'undefined' ? true : navButtonsSetup.enableView,
            viewicon: 'fa fa-search-plus grey'
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
    buttons.eq(0).addClass('btn-primary').prepend('<i class="fa fa-check"></i>');
    buttons.eq(1).prepend('<i class="fa fa-clock-o s"></i>');

    buttons = form.next().find('.navButton a');
    buttons.find('.ui-icon').remove();
    buttons.eq(0).append('<i class="fa fa-chevron-left"></i>');
    buttons.eq(1).append('<i class="fa fa-chevron-right"></i>');
}

function styleDeleteForm(form) {
    var buttons = form.next().find('.EditButton .fm-button');
    buttons.addClass('btn btn-sm').find('[class*="-icon"]').remove();//ui-icon, s-icon
    buttons.eq(0).addClass('btn-danger').prepend('<i class="fa fa-trash"></i>');
    buttons.eq(1).prepend('<i class="fa fa-clock-o s"></i>');
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
    buttons.find('.EditButton a[id*="_reset"]').addClass('btn btn-sm btn-info').find('.ui-icon').attr('class', 'fa fa-retweet');
    buttons.find('.EditButton a[id*="_query"]').addClass('btn btn-sm btn-inverse').find('.ui-icon').attr('class', 'fa fa-comment-alt');
    buttons.find('.EditButton a[id*="_search"]').addClass('btn btn-sm btn-purple').find('.ui-icon').attr('class', 'fa fa-search');
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


