$(function () {
    window.app.validateOnSubmit = true;

    $('[id$=txtCPF]').mask('999.999.999-99');
    $('[id$=txtCNPJ]').mask('99.999.999/9999-99');
    $('[id$=txtTelefone]').mask('(99) 9999-9999');
    $('[id$=txtTelefoneComercial]').mask('(99) 9999-9999');
    $('[id$=txtCEP]').mask('99999-999');

    var form = document.forms[0];

    form.onsubmit = function () {
        if (!window.app.validateOnSubmit)
            return true;

        return window.app.validate();
    }
});