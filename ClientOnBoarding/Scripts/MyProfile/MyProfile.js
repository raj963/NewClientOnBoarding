// The module pattern
var fxMP = (function () {
    var bindCheckBoxUserPassword = function () {
        $('#userPasswordChkbox').change(function () {
            debugger;
            if ($("#userPasswordChkbox").is(':checked')) {
                $('#Password').get(0).type = 'text';
            }
            else {
                $('#Password').get(0).type = 'Password';
            }
        });
    }

    var bindCustomerInformationSubmitHandler = function (formTab) {
        //$('#customerDetail').find('form').submit(function (e) {
        $(formTab).find('form').submit(function (e) {
            var $form = $(this);
            // We check if jQuery.validator exists on the form
            if (!$form.valid || $form.valid()) {
                $.post($form.attr('action'), $form.serializeArray())
                    .done(function (json) {
                        json = json || {};
                        if (json.success) {                              
                            //Re-direct to search page
                            location = json.redirect
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
        });
    }

    // public API
    return {
        bindCustomerInformationSubmitHandler: bindCustomerInformationSubmitHandler,
        bindCheckBoxUserPassword: bindCheckBoxUserPassword
    };
})();

$(document).ready(function () {
    fxMP.bindCustomerInformationSubmitHandler('.midFrame');
    fxMP.bindCheckBoxUserPassword();
});

