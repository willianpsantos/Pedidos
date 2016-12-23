$(function () {
    window.app.validateOnSubmit = true;

    $('input[id$=txtDataEmissao], input[id$=txtDataLocacao], input[id$=txtDataDevolucao], input[id$=txtDataSaida]').mask('99/99/9999');

    window.kendo.init();

    $('input[id$=txtPrecoUnitario], input[id$=txtQuantidade]').keyup(function (e) {
       
        var inputTotal = $('input[id$=txtItemTotal]');
        var precoUnitario = window.app.parseFloat($('input[id$=txtPrecoUnitario]').val());
        
        if (isNaN(precoUnitario)) {
            inputTotal.val('');
            return false;
        }

        var quantidade = window.app.parseFloat($('input[id$=txtQuantidade]').val());

        if (isNaN(quantidade)) {
            inputTotal.val('');
            return false;
        }

        var total = precoUnitario * quantidade;
        inputTotal.val(window.app.floatToString(total));
        return true;
    });

    $('input[id$=txtDesconto]').keyup(function (e) {

        var inputTotal = $('input[id$=txtTotal]');
        var subTotal = window.app.parseFloat($('input[id$=txtSubTotal]').val());

        inputTotal.val(window.app.floatToString(subTotal));

        if (isNaN(subTotal)) {
            return false;
        }

        var desconto = window.app.parseFloat($('input[id$=txtDesconto]').val());

        if (isNaN(desconto)) {
            return false;
        }

        var total = subTotal - desconto;
        inputTotal.val(window.app.floatToString(total));
        return true;
    });

    $('[data-role=datepicker]').kendoDatePicker({ culture: 'pt-BR' });

    var form = document.forms[0];

    form.onsubmit = function () {
        if (!window.app.validateOnSubmit)
            return true;

        return window.app.validate();
    }
});

