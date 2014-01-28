
var fxSearch = (function () {

    var oTable;

    var bindOTable = function () {
        oTable = $('#SearchGridTable').dataTable({
            "bServerSide": true,
            "sAjaxSource": "/Search/SearchResultData",
            "bProcessing": true,
            "sPaginationType": "full_numbers",
            "bPaginate": true,
            "bJQueryUI": true,
            "bProcessing": false,
            "bFilter": false,
            "bAutoWidth": false,
            "aoColumns": [
                { "sName": "CustomerName", sWidth: '9%', "sClass": "wrapWord changFont" },
                { "sName": "ClientSiteName", sWidth: '9%', "sClass": "wrapWord changFont" },
                { "sName": "ClientSiteStatus", sWidth: '9%', "sClass": "wrapWord changFont" },
                { "sName": "DeviceName", sWidth: '9%', "sClass": "wrapWord changFont", fnRender: devIdLink },
                { "sName": "DeviceDesc", sWidth: '9%', "sClass": "wrapWord changFont" },
                { "sName": "AccessPolicy", sWidth: '9%', "sClass": "wrapWord changFont", fnRender: apLink },
                { "sName": "MaintenancePolicy", sWidth: '9%', "sClass": "wrapWord changFont", fnRender: mpLink },
                { "sName": "PatchingPolicy", sWidth: '9%', "sClass": "wrapWord changFont", fnRender: ppLink },
                //{ "sName": "AntiVirusPolicy", sWidth: '9%', "sClass": "wrapWord changFont", fnRender: avpLink },
                //{ "sName": "BackUpPolicy", sWidth: '9%', "sClass": "wrapWord changFont", fnRender: bpLink },
                { "sName": "RMMTool", sWidth: '8%', "sClass": "wrapWord changFont", fnRender: rmmToolLink },
            ],
            "fnServerParams": function (aoData) {
                aoData.push({ "name": "CustomerName", "value": $('#Customer').val() },
                            { "name": "ClientName", "value": $('#Client').val() },
                            { "name": "DeviceDesc", "value": $('#DeviceId').val() });
            },
            "fnDrawCallback": function (oSettings) {
                bindReadOnlyLinks();
            },
            "fnInitComplete": function () {
                $('#SearchGridTable_wrapper').addClass('wrapperDiv');
            }
        });
    };

    var bindReadOnlyLinks = function () {
        $('[data-link-edit="Edit-ReadOnly"]').on("click", function (e) {
            var link = $(this);
            readOnlyModalPopUp(link, e.pageX, e.pageY)

            // Prevent the normal behavior since we opened the dialog
            e.preventDefault();
        });
    };

    var apLink = function (oObj) {
        var id = oObj.aData[6];
        var name = oObj.aData[5];
        var accessPolicyLink = "<a "
                             + "data-link-edit='Edit-ReadOnly'"
                             + "data-dialog-title='Access Policy Detail' "
                             + "data-dialog-height='auto' "
                             + "data-dialog-width='550' "
                             + "href='/Search/AccessPolicy?AccessPolicyID=" + $.trim(id) + "' "
                             + ">" + name + "</a>";
        return accessPolicyLink
    };

    var devIdLink = function (oObj) {
        var id = oObj.aData[13];
        var name = oObj.aData[3];
        var deviceIdLink = "<a "
                             + "data-link-edit='Edit-ReadOnly'"
                             + "data-dialog-title='Manage Client Divices' "
                             + "data-dialog-height='auto' "
                             + "data-dialog-width='550' "
                             + "href='/Search/ManageDevice?DeviceID=" + $.trim(id) + "' "
                             + ">" + name + "</a>";
        return deviceIdLink
    };

    var mpLink = function (oObj) {
        var id = oObj.aData[8];
        var name = oObj.aData[7];
        var accessPolicyLink = "<a "
                             + "data-link-edit='Edit-ReadOnly' "
                             + "data-dialog-title='Maintenance Policy Detail' "
                             + "data-dialog-height='auto' "
                             + "data-dialog-width='645' "
                             + "href='/Search/MaintenancePolicy?MaintenancePolicyID=" + $.trim(id) + "' "
                             + ">" + name + "</a>";
        return accessPolicyLink
    };

    var ppLink = function (oObj) {
        var id = oObj.aData[10];
        var name = oObj.aData[9];
        var accessPolicyLink = "<a "
                             + "data-link-edit='Edit-ReadOnly' "
                             + "data-dialog-title='Patching Policy Detail' "
                             + "data-dialog-height='auto' "
                             + "data-dialog-width='550' "
                             + "href='/Search/PatchingPolicy?PatchingPolicyID=" + $.trim(id) + "' "
                             + ">" + name + "</a>";
        return accessPolicyLink
    };

    //var avpLink = function (oObj) {
    //    var id = oObj.aData[12];
    //    var name = oObj.aData[11];
    //    var accessPolicyLink = "<a "
    //                         + "data-link-edit='Edit-ReadOnly' "
    //                         + "data-dialog-title='Antivirus Policy Detail' "
    //                         + "data-dialog-height='auto' "
    //                         + "data-dialog-width='550' "
    //                         + "href='/Search/AntiVirusPolicy?AntiVirusID=" + $.trim(id) + "' "
    //                         + ">" + name + "</a>";
    //    return accessPolicyLink
    //};

    //var bpLink = function (oObj) {
    //    var id = oObj.aData[14];
    //    var name = oObj.aData[13];
    //    var accessPolicyLink = "<a "
    //                         + "data-link-edit='Edit-ReadOnly' "
    //                         + "data-dialog-title='Backup Policy Detail' "
    //                         + "data-dialog-height='auto' "
    //                         + "data-dialog-width='550' "
    //                         + "href='/Search/BackUpPolicy?BackUpPolicyID=" + $.trim(id) + "' "
    //                         + ">" + name + "</a>";
    //    return accessPolicyLink
    //};

    var rmmToolLink = function (oObj) {
        var id = oObj.aData[12];
        var name = oObj.aData[11];
        var accessPolicyLink = "<a "
                             + "data-link-edit='Edit-ReadOnly' "
                             + "data-dialog-title='Tool Information' "
                             + "data-dialog-height='auto' "
                             + "data-dialog-width='550' "
                             + "href='/Search/ToolInformation?ToolID=" + $.trim(id) + "' "
                             + ">" + name + "</a>";
        return accessPolicyLink
    };

    var bindSearchControl = function () {
        $('#search').click(function () {
            var request = $.ajax({
                type: "GET",
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                url: '/Search/SearchResult',
                dataType: "html"
            });

            request.done(function (data) {
                $('#SearchDetail').html(data);
                bindOTable();
            });

            request.fail(function (jqXHR, textStatus) {
                $fx.handleAjaxError(jqXHR, textStatus);
            });

            event.preventDefault();
        });
    };

    var readOnlyModalPopUp = function (link, x, y) {
        var linkUrl = link.attr('href');

        var request = $.ajax({
            type: "GET",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            url: linkUrl,
            dataType: "html"
        });

        request.done(function (data) {
            var contactDialog = $('<div class="dilogPading modal-popup">' + data + '</div>')
                .appendTo(document.body)
                .filter('div') // Filter for the div tag only, script tags could surface
                .dialog({ // Create the jQuery UI dialog
                    title: link.data('dialog-title'),
                    position: [x, y],
                    modal: false,
                    resizable: false,
                    draggable: true,
                    height: link.data('dialog-height'),
                    width: link.data('dialog-width'),
                    close: function (event, ui) {
                        $(this).dialog('destroy').remove();
                    }
                });

            contactDialog.dialog('open');
        });

        request.fail(function (jqXHR, textStatus) {
            $fx.handleAjaxError(jqXHR, textStatus);
        });
    };

    // public API
    return {
        bindSearchControl: bindSearchControl,
        bindOTable: bindOTable,
        bindReadOnlyLinks: bindReadOnlyLinks,
    };
})();

$(document).ready(function () {
    fxSearch.bindSearchControl();
});