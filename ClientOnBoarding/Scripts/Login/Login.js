// The module pattern
var fxLogin = (function () {

    var externalLoginFormSubmitHandler = function (e) {
        var $form = $(this);

        // We check if jQuery.validator exists on the form
        if (!$form.valid || $form.valid()) {
            //$.post($form.attr('action'), $form.serializeArray(), { ReturnUrl: getReturnURL("ReturnUrl") })
            var data = $form.serializeArray();
            data.push({ name: 'ReturnUrl', value: getReturnURL("ReturnUrl") });
            $.post($form.attr('action'), data)
                .done(function (json) {
                    json = json || {};
                    // In case of success, we redirect to the provided URL or the same page.
                    if (json.success) {                        
                        location = json.redirect || location.href;
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

    var getReturnURL = function getUrlParamArray(param)
    {
        param = param.replace(/([\[\](){}*?+^$.\\|])/g, "\\$1");
        var value = [];
        var regex = new RegExp("[?&]" + param + "=([^&#]*)", "g");
        var url   = decodeURIComponent(window.location.href);
        var match = null;
        while (match = regex.exec(url)) {
            value.push(match[1]);
        }
        
        if (value.length > 0)
            return value[0];
        else
            return "";
    }

    // public API
    return {
        loginSubmitHandler: externalLoginFormSubmitHandler
    };

})();

// Using the module pattern for a jQuery feature
$(document).ready(function () {
    $('.midFrame').find('form').submit(fxLogin.loginSubmitHandler);
});