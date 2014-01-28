//The module pattern
var fxMD = (function () {

    var oTable;

    var bindManageDevice = function () {
        debugger;
        $('#add_clientdevice').on("click", function (e) {
            debugger;

            var selValue = $('select[id=ClientID] option:selected').val();

            if (selValue == 0) {
                bootbox.alert("Please select the client site !", function () {
                });
            }
            else {

                var link = $(this);
                deviceModalPopUp(link, selValue);
            }

            // Prevent the normal behavior since we opened the dialog
            e.preventDefault();
        });

        $('select[id=ClientID]').on("change", function (e) {
            var selValue = $(this).val();

            if (selValue == 0) {
                $('#clientDeviceDetail').html('');
                $('#TimeZone_ID').val('');
                $('#Status_ID').val('');
            }
            else {
                getClientInfo(selValue);
            }

            // Prevent the normal behavior
            e.preventDefault();
        });


    };

    var bindDeviceGrid = function () {
        $('[data-link-edit="Edit"]').on("click", function (e) {

            var selValue = $('select[id=ClientID] option:selected').val();
            var link = $(this);
            deviceModalPopUp(link, selValue)

            // Prevent the normal behavior since we opened the dialog
            e.preventDefault();
        });

        $('[data-ajax-update="#DeviceGrid"]').on("click", function (e) {
            var selectedValue = $('select[id=ClientID] option:selected').val();
            var link = $(this);
            link.attr("href", link.attr('href') + '&ClientID=' + selectedValue);

            // Prevent the normal behavior since we opened the dialog
            //e.preventDefault();
        });
        $('[data-link-delete="Delete"]').on("click", function (e) {

            var link = $(this);
            var contactLnk = link.attr('data-id');

            var linkUrl = link.attr('href');
            bootbox.confirm("Do you really want to delete device information?", function (result) {
                if (result == true) {
                    var request = $.ajax({
                        type: "GET",
                        contentType: "application/json",
                        url: linkUrl,
                        dataType: "json",
                        data: { "ContactID": contactLnk }
                    });

                    request.done(function (data) {
                        if (data.success) {
                            bootbox.alert(data.errDesc);
                            oTable.fnReloadAjax();
                        }
                        else if (data.errors)
                            bootbox.alert(data.errDesc);

                    })
                }
            })

            e.preventDefault();
        });
    }

    var getClientInfo = function (clientID) {
        var manageClientGridLink = $('#add_clientdevice');

        var request = $.ajax({
            type: "GET",
            contentType: "application/json",
            url: manageClientGridLink.attr('data-clienturl'),
            dataType: "json",
            data: { "ClientID": clientID }
        });

        request.done(function (json) {
            json = json || {};
            if (json.success) {
                $('#TimeZone_ID').val(json.ClientTimeZone);
                $('#Status_ID').val(json.ClientStatus);
                getClientDevices(clientID);
            }
        });

        request.fail(function (jqXHR, textStatus) {
            handleAjaxError(jqXHR, textStatus);
        });
    }

    var getClientDevices = function (clientID) {
        var manageClientGridLink = $('#add_clientdevice');
        var finalUrl = manageClientGridLink.attr('data-gridUrl');

        var request = $.ajax({
            type: "GET",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            url: finalUrl,
            dataType: "html"
        });

        request.done(function (data) {
            $('#clientDeviceDetail').html(data);
            debugger;
            oTable = $('#DeviceGridTable').dataTable({
                "bServerSide": true,
                "sAjaxSource": "/ManageClientDevices/DeviceGridRecord",
                "bProcessing": true,
                "sPaginationType": "full_numbers",
                "bPaginate": true,
                "bJQueryUI": true,
                "bProcessing": false,
                "bAutoWidth": false,
                "oLanguage": {
                    "sSearch": "Search Device ID: "
                },
                "aoColumns": [
                    { "sName": "DeviceIDFromRMMTool", sWidth: '10%', "sClass": "wrapWord changFont" },
                    { "sName": "DeviceTypeName", sWidth: '10%', "sClass": "wrapWord changFont" },
                    { "sName": "DeviceDescription", sWidth: '10%', "sClass": "wrapWord changFont" },
                    { "sName": "AccessPolicyName", sWidth: '10%', "sClass": "wrapWord changFont" },
                    { "sName": "MaintenancePolicyName", sWidth: '10%', "sClass": "wrapWord changFont" },
                    { "sName": "PatchingPolicyName", sWidth: '10%', "sClass": "wrapWord changFont" },
                   // { "sName": "AntiVirusPolicyName", sWidth: '10%', "sClass": "wrapWord changFont" },                   
                    //{ "sName": "BackupPolicyName", sWidth: '10%', "sClass": "wrapWord changFont" },                    
                      { "sName": "RMMToolName", sWidth: '10%', "sClass": "wrapWord changFont" },
                       { "sName": "MiscInfo", sWidth: '10%', "sClass": "wrapWord changFont" },
                    { "sName": "DeviceID", "sWidth": '6%', fnRender: editLink, "bSortable": false, "bSearchable": false, "sClass": "wrapWord changFont" }
                ],
                "fnServerParams": function (aoData) {
                    aoData.push({ "name": "ClientID", "value": clientID });
                },
                "fnDrawCallback": function (oSettings) {
                    bindDeviceGrid();
                }
                 ,
                "fnInitComplete": function () {
                    $('#DeviceGridTable_wrapper').addClass('wrapperDiv');
                }
            });

            $('.dataTables_filter input')
            .unbind('keypress keyup')
            .bind('keypress keyup', function (e) {
                if ($(this).val().length < 3 && e.keyCode != 13) return;
                oTable.fnFilter($(this).val());
            });

        });

        request.fail(function (jqXHR, textStatus) {
            handleAjaxError(jqXHR, textStatus);
        });
    }

    var editLink = function make_delete_link(oObj) {
        debugger;
        var id = oObj.aData[8];
        var editDelete = "<img class='icon' src='/Images/edit.png' data-id='" + id + "' title='Edit Device Information' data-link-edit='Edit' href='/ManageClientDevices/Device/'>";
        editDelete = editDelete + "<img class='icon' src='/Images/delete.png' title='Delete Device'   href='/ManageClientDevices/DeleteDevices/' data-id='" + id + "'data-link-delete='Delete' />";
        return editDelete
    }

    var deviceModalPopUp = function (link, clientID) {
        var linkUrl = link.attr('href');
        var deviceID = 0;

        var deviceLnk = link.attr('data-id');
        if (deviceLnk == "undefined")
            deviceID = 0;
        else
            deviceID = deviceLnk;

        var modalTitle = "Add Device";
        if (deviceID > 0)
            modalTitle = "Update Device"

        if (typeof customerName != "undefined")
            modalTitle = modalTitle + ' - ' + customerName;

        var request = $.ajax({
            type: "GET",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            url: linkUrl,
            dataType: "html",
            data: { "DeviceID": deviceID, "ClientID": clientID }
        });

        request.done(function (data) {
            debugger;
            var contactDialog = $('<div class="dilogPading modal-popup">' + data + '</div>')
                .appendTo(document.body)
                .filter('div') // Filter for the div tag only, script tags could surface
                .dialog({ // Create the jQuery UI dialog
                    title: link.data('dialog-title') || modalTitle,
                    modal: true,
                    resizable: link.data('dialog-resize') || false,
                    draggable: false,
                    height: 'auto',
                    width: link.data('dialog-width') || 642,
                    close: function (event, ui) {
                        $(this).dialog('destroy').remove();
                    }
                }).find('form') // Attach logic on forms
                        .submit(deviceFormSubmitHandler)
                        .end();

            $fx.setUnobstrusiveControls("#dvDevice");
            bindCheckBoxClientCiteDevice();
            contactDialog.dialog('open');
        });

        request.fail(function (jqXHR, textStatus) {
            handleAjaxError(jqXHR, textStatus);
        });
    };
    var bindCheckBoxClientCiteDevice = function () {
        $('#ClientDeviceChkbox').change(function () {
            debugger;
            if ($("#ClientDeviceChkbox").is(':checked')) {
                $('#Password').get(0).type = 'text';
            }
            else {
                $('#Password').get(0).type = 'Password';
            }
        });
    }
    var deviceFormSubmitHandler = function (e) {
        var $form = $(this);
        // We check if jQuery.validator exists on the form
        if (!$form.valid || $form.valid()) {
            $.post($form.attr('action'), $form.serializeArray())
                .done(function (json) {
                    json = json || {};
                    debugger;
                    if (json.success) {
                        $(".modal-popup").dialog("close");
                        //reload the grid again.  
                        getClientDevices(json.ClientID);
                    } else if (json.errors) {
                        $fx.displayErrors($form, json.errors);
                    }
                })
                .error(function () {
                    var keyValList = new Object();
                    keyValList.Key = '';
                    keyValList.Value = 'An unknown error happened.';
                    var errors = new Object();
                    errors[0] = keyValList;
                    $fx.displayErrors($form, errors);
                });
        }

        // Prevent the normal behavior since we opened the dialog
        e.preventDefault();
    };

    var handleAjaxError = function (jqXHR, textStatus) {
        if (jqXHR.status == 401) {
            // perform a redirect to the login page since we're no longer authorized
            window.location = $('#loginURL').val();
        }
        else {
            alert("Error: " + jqXHR);
            alert("Request failed: " + textStatus);
        }
    };

    // public API
    return {
        bindManageDevice: bindManageDevice,
        bindDeviceGrid: bindDeviceGrid
    };
})();

$(document).ready(function () {
    fxMD.bindManageDevice();
    window.$fxMD = fxMD;
});