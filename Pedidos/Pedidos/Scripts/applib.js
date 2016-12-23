var app = {};
app.rootUrl = window.location.protocol + "//" + window.location.host + (window.location.pathname ? window.location.pathname : '');

app.Enum = {

    getDescription: function ($enum, value) {
        for (var key in $enum)
            if (key == value)
                return $enum[key].description;

        return null;
    },

    getValue: function ($enum, value) {
        for (var key in $enum)
            if ($enum[key].code == value)
                return key;

        return null;
    },
        
    getCode: function ($enum, value) {
        for (var key in $enum)
            if (key == value)
                return $enum[key].code;

        return null;
    },

    toList: function ($enum) {
        var list = [];

        for (var key in $enum)
            list.push({
                text: $enum[key].description,
                value: key
            });

        return list;
    },

    cast: function (value) {
        if (!value)
            return undefined;

        if (typeof value == 'string')
            return value;

        if (typeof value == 'object' && value.hasOwnProperty('value'))
            return value['value'];

        return undefined;
    }
};

    
app.ajaxError = function (xhr, exception, deferred) {    
    var loading = $(".loading");            
    loading.hide();

    if (xhr.status == 400) {            
        codemaker.MessageBox.alert("Error[" + xhr.status + "]", $.parseJSON(xhr.responseText).message, { icon: codemaker.MessageBox.ERROR, height: 120 });
    } 
    else if (xhr.status == 403) {
        codemaker.MessageBox.alert("Error[" + xhr.status + "]", xhr.responseText, { icon: codemaker.MessageBox.ERROR, height: 120 });
    } 
    else if (xhr.status == 401) {

        codemaker.MessageBox.alert(
            "Error[" + xhr.status + "]",
            "Sua sessão expirou automaticamente ou houve falha de comunicação com o servidor. Será redirecionado para efetuar novamente seu login.",
            {
                icon: codemaker.MessageBox.INFO,
                height: 120,
                handler: function () {
                    location.href = app.rootUrl;
                }
            });

    } 
    else if (xhr.status == 404) {
        codemaker.MessageBox.alert("Error[" + xhr.status + "]", "Página não encontrada.", { icon: codemaker.MessageBox.INFO, height: 120 });
    } 
    else if (xhr.status == 500) {
        codemaker.MessageBox.alert("Error[" + xhr.status + "]", "Erro interno no servidor.", { icon: codemaker.MessageBox.ERROR, height: 120 });
    } 
    else if (xhr.status == 408) {
        codemaker.MessageBox.alert("Error[" + xhr.status + "]", "Tempo da requisição expirado.", { icon: codemaker.MessageBox.ERROR, height: 120 });
    } 
    else if (xhr.status == 204) {
        codemaker.MessageBox.alert("Error[" + xhr.status + "]", "Nenhum registro foi encontrado com o filtro utilizado.", { icon: codemaker.MessageBox.INFO, height: 120 });
    } 
};

app.showLoading = function() {
    var loading = $("#main > header > .loading");
    app.isBusy = true;        
    loading.show();
};

app.hideLoading = function() {
    var loading = $("#main > header > .loading");
    app.isBusy = false;        
    loading.hide();
};

app.meses = [
    { value: 1, text: 'Janeiro' },
    { value: 2, text: 'Fevereiro' },
    { value: 3, text: 'Março' },
    { value: 4, text: 'Abril' },
    { value: 5, text: 'Maio' },
    { value: 6, text: 'Junho' },
    { value: 7, text: 'Julho' },
    { value: 8, text: 'Agosto' },
    { value: 9, text: 'Setembro' },
    { value: 10, text: 'Outubro' },
    { value: 11, text: 'Novembro' },
    { value: 12, text: 'Dezembro' }
];
    
app.mes = function (id) {
    var mes;

    $.each(app.meses, function () {
        mes = this;

        if (mes.value == id)
            return false;

        return true;
    });

    return mes.text;
};    
    
app.estados = [
    { value: "AC", text: "Acre" },
    { value: "AL", text: "Alagoas" },
    { value: "AM", text: "Amazonas" },
    { value: "AP", text: "Amapá" },
    { value: "BA", text: "Bahia" },
    { value: "CE", text: "Ceará" },
    { value: "DF", text: "Distrito Federal" },
    { value: "ES", text: "Espírito Santo" },
    { value: "GO", text: "Goiás" },
    { value: "MA", text: "Maranhão" },
    { value: "MG", text: "Minas Gerais" },
    { value: "MS", text: "Mato Grosso do Sul" },
    { value: "MT", text: "Mato Grosso" },
    { value: "PA", text: "Pará" },
    { value: "PB", text: "Paraíba" },
    { value: "PR", text: "Paraná" },
    { value: "PE", text: "Pernambuco" },
    { value: "PI", text: "Piauí" },
    { value: "RJ", text: "Rio de Janeiro" },
    { value: "RN", text: "Rio Grande do Norte" },
    { value: "RO", text: "Rondônia" },
    { value: "RR", text: "Roraima" },
    { value: "RS", text: "Rio Grande do Sul" },
    { value: "SC", text: "Santa Catarina" },
    { value: "SE", text: "Sergipe" },
    { value: "SP", text: "São Paulo" },
    { value: "TO", text: "Tocantins" },
    { value: "OU", text: "Outros" }
];

app.estado = function(uf) {
    var estado;

    $.each(app.estados, function() {            
        estado = this;

        if (estado.value == uf)
            return false;

        return true;
    });

    return estado;
};

$.urlParam = function (name) {
    var results = new RegExp('[\\?&amp;]' + name + '=([^&amp;#]*)').exec(window.location.href);
    return results[1] || 0;
};

app.validate = function (callback) {
    if (!window.app.validateOnSubmit)
        return true;

    var addClassError = function (item) {
        var dataRole = item.attr('data-role');

        if (!dataRole) {
            item.addClass('k-error');
            return;
        }

        var handler = item.data().handler;

        if (!handler)
            return;

        if (handler.wrapper)
            handler.wrapper.addClass('k-error');
        else if (handler.element)
            handler.element.addClass('k-error');
    };

    var removeClassError = function (item) {
        var dataRole = item.attr('data-role');

        if (!dataRole) {
            item.removeClass('k-error');
            return;
        }

        var handler = item.data().handler;

        if (!handler)
            return;

        if (handler.wrapper)
            handler.wrapper.removeClass('k-error');
        else if (handler.element)
            handler.element.removeClass('k-error');
    };

    var rootElement = $('section.content');
    var requireds = rootElement.find('[data-required]');
    var validated = true;
    var fieldsWithError = [];

    $.each(requireds, function (index, value) {
        var item = $(this);
        var parent = item.parents('div.table-cell');
        removeClassError(item);
        parent.find('i.icon-error').parents('span').remove();
        var error = false;
        
        if (item.is('input')) {
            var value = item.val();

            if (!value)
                error = true;
        }
        else if (item.is('select')) {
            var value = item.find('option:selected').val();

            if (!value)
                error = true;
        }

        if (error) {
            addClassError(item);
            var html = '<span style="color:firebrick; font-size:1em !important;"> <i title="'+item.data('required')+'" class="fa fa-exclamation-triangle fa-lg icon-error"></i></span>';
            parent.append(html);
            validated = false;
            fieldsWithError.push(item);
        }
    });
    
    if (callback) {
        callback({
            validated: validated,
            fields: fieldsWithError
        });
    }
    
    return validated;
};

app.enable = function (enabled) {
    var rootElement = $('section.content'),
        items = rootElement.find('input[type="text"], button');

    if (enabled == undefined)
        enabled = true;

    $.each(items, function () {
        var item = $(this),
            dataRole = item.attr('data-role'),
            reallyEnabled = enabled,
            alwaysDisabled = item.attr('alwaysdisabled') == 'true';        
        
        if (alwaysDisabled)
            reallyEnabled = false;       

        if (!dataRole) {
            if (reallyEnabled) {
                item.removeAttr('disabled', 'disabled');
                item.removeClass('k-state-disabled');
            }
            else {
                item.attr('disabled', 'disabled');
                item.addClass('k-state-disabled');
            }
        }
        else {
            var data = item.data(),
                widget = data.handler;

            if (widget) {
                if (widget.enable) {
                    if (typeof widget.enable == 'function') {
                        widget.enable(reallyEnabled);
                    }
                    else {
                        widget.enable = reallyEnabled;
                    }
                }
                else if (widget.disable) {
                    if (widget.disable == 'function') {
                        widget.disable(reallyEnabled);
                    }
                    else {
                        widget.disable = reallyEnabled;
                    }
                }
            }
        }
    });
}

app.selectStep = function (step) {
    $('ul.steps > li').removeClass('selected');
    $('ul.steps > li#' + step).addClass('selected');
}

app.changeView = function (html) {
    var container = $('#main > .content');

    container.fadeOut('slow', function () {
        container.hide();
        container.html(html);
        container.fadeIn('slow');
        app.hideLoading();
        app.enable(true);
    });
};

app.navigate = function (view, data, method) {
    app.showLoading();
    app.enable(false);
    var type = (method == undefined ? 'POST' : method);
    var params = data && type == 'GET' ? '?' + $.param(data) : '';

    $.ajax({
        type: type,
        url: app.rootUrl + view + (type == 'GET' ? params : ''),
        data: data && type == 'POST' ? JSON.stringify(data) : undefined,
        contentType: 'application/json; charset=utf-8',
        dataType: 'html',
        success: function (response) {
            app.changeView(response);
        },
        error: app.ajaxError
    });
};

app.delimitateContent = function () {
    var main = $('section#main');
    var header = main.children('header');
    var footer = main.children('footer');
    var content = main.children('section.content');
    var menu = content.children('.menu');
    var form = content.children('.form-main');
    
    var mainHeight = main.height();    
    var footerHeight = footer.height();
    var headerHeight = header.height();
    var height = (mainHeight - headerHeight - footerHeight);

    var css = { 'height': height + 'px' };
    content.css(css);
    form.css(css);
    menu.css(css);

    var mainWidth = main.width();
    var menuWidth = menu.width();
    var formWidth = (mainWidth - menuWidth) - 10;
    form.css({ 'width': formWidth + 'px' });
};

app.setTitle = function (title, animated) {
    var h1 = $('section#main > header > h1');

    if (animated == true) {
        h1.fadeOut('slow', function () {
            h1.text(title);
            h1.fadeIn('slow');
        });
    }
    else {
        h1.text(title);
    }
};

app.disableAllButtons = function () {
    var btnContinuar = $('section#main > footer > button#btn-continuar'),
        btnVoltar = $('section#main > footer > button#btn-voltar'),
        btnInicio = $('section#main > footer > button#btn-inicio'),
        btnImprimir = $('section#main > footer > button#btn-imprimir-boleto'),
        btnFinalizar = $('section#main > footer > button#btn-finalizar');

    btnContinuar.attr('disabled', 'disabled').addClass('k-state-disabled');
    btnVoltar.attr('disabled', 'disabled').addClass('k-state-disabled');
    btnInicio.attr('disabled', 'disabled').addClass('k-state-disabled');
    btnImprimir.attr('disabled', 'disabled').addClass('k-state-disabled');
    btnFinalizar.attr('disabled', 'disabled').addClass('k-state-disabled');
};

app.enableAllButtons = function () {
    var btnContinuar = $('section#main > footer > button#btn-continuar'),
        btnVoltar = $('section#main > footer > button#btn-voltar'),
        btnInicio = $('section#main > footer > button#btn-inicio'),
        btnImprimir = $('section#main > footer > button#btn-imprimir-boleto'),
        btnFinalizar = $('section#main > footer > button#btn-finalizar');

    btnContinuar.removeAttr('disabled', 'disabled').removeClass('k-state-disabled');
    btnVoltar.removeAttr('disabled', 'disabled').removeClass('k-state-disabled');
    btnInicio.removeAttr('disabled', 'disabled').removeClass('k-state-disabled');
    btnImprimir.removeAttr('disabled', 'disabled').removeClass('k-state-disabled');
    btnFinalizar.removeAttr('disabled', 'disabled').removeClass('k-state-disabled');
};

app.parseFloat = function (value) {
    var aux = value.replace(',', '.');
    return parseFloat(aux);
};

app.floatToString = function (value) {
    var aux = value.toFixed(2).toLocaleString();
    aux = aux.replace('.', ',');
    return aux;
}