(function ($) {

    var LookupManager = window.kendo.Class.extend({

        options: {
            lookupType: 'server',
            name: 'LookupManager',
            modal: true,
            id: '',

            filter: {
                field: ''
            },

            select: {
                valueField: '',
                textField: ''
            },

            url: {
                service: '',
                lookupQueryString: 'lookup',
                page: ''
            },

            selectors: {
                select: '',
                openButton: '',
                clearButton: ''
            },

            grid: {
                columns: []
            }
        },

        _templates: {
            modalClient: {
                selector: 'div#modal',
                gridSelector: 'div#grid',
                html: '<div id="modal" name="{name}" style="display:none;">' +
                      '    <div id="grid"></div>' +
                      '</div>'
            },

            modalServer: {
                selector: 'div#modal',
                frameSelector: 'iframe#page',
                html: '<div id="modal" name="{name}" style="display:none;">' +
                      '    <iframe id="page" src="" style="width:100%; height:100%; paddding: 0; margin: 0; border:none;"></iframe>' +
                      '</div>'
            }
        },

        _filterData: function(text) {
            var that = this;

            var datasource = that.createDataSource();

            datasource.bind('dataBound', function (e) {
                var data = e.response.Data;
                that.select.empty();

                $.each(data, function () {
                    that.select.append('<option value="' + this[that.options.select.valueField] + '">' + this[that.options.select.textField] + '</option>');
                });
            });

            datasource.read({
                field: that.options.filter.field,
                operator: 'startswith',
                value: text
            });
        },

        _bindEvents: function() {
            var that = this;

            that.select = that.element.children(that.options.selectors.select).keypress(function (e) {
                var select = $(this);

                if (e.which == 13) {
                    that._filterData(select.text());
                }
            });

            that.element.children(that.options.selectors.openButton).click(function (e) {
                that.messageBox.center().open();
            });

            that.element.children(that.options.selectors.clearButton).click(function (e) {
                that.executeClear();
            });

            $(window).resize(function () {
                that.messageBox.wrapper.css({
                    width: ($('body').width() - 100) + 'px',
                    height: ($('body').height() - 50) + 'px'
                })
            });
        },

        createModal: function(){
            var that = this,
                width = $('body').width() - 100,
                height = $('body').height() - 50;

            that.messageBox = that.modalElement.kendoMessageBox({
                width: width,
                height: height,
                title: 'Pesquisa Avançada',

                buttons: {
                    items: [
                        {
                            name: 'ok',
                            text: 'OK',
                            iconCls: 'fa fa-check-circle font-green'
                        },

                        {
                            name: 'close',
                            text: 'Fechar',
                            iconCls: 'fa fa-times-circle font-red',
                            
                        }
                    ]
                },

                handler: function (e) {
                    switch (e.button.name) {
                        case 'ok':
                            that.executeFilter(that.data.field, that.data.operation, that.data.value);
                            break;
                        case 'close':
                            window.app.validateOnSubmit = true;
                            e.sender.close();                            
                            break;
                    }
                },

                open: function (e) {
                    window.app.validateOnSubmit = false;

                    switch (that.options.lookupType) {
                        case 'client':
                            that.grid.dataSource.read();
                            break;
                        case 'server':
                            var lastFilter = that.element.data('last-filter'),
                                url = that.options.url.page + '?' + that.options.url.lookupQueryString + '=true';

                            if (lastFilter) {
                                url += '&' + lastFilter;
                            }

                            that.iframe.attr('src', url);
                            break;
                    }
                                        }
            }).data('kendoMessageBox');

            that.messageBox.lookupManager = that;
        },

        createDataSource: function () {
            var that = this;

            return new window.kendo.data.DataSource({
                serverFiltering: true,
                serverSorting: true,
                serverPaging: true,
                pageSize: 50,
                page:0,

                schema: {
                    total: 'Total',
                    data: 'Data'
                },

                transport: {
                    read: function (options) {
                        $.ajax({
                            type: 'GET',
                            url: that.options.url,
                            dataType: 'json',
                            data: options.data,
                            success: function (response) {
                                options.success(response)
                            }
                        })
                    }
                }
            });
        },

        createGrid: function(){
            var that = this;

            that.grid = that.modalElement.find(that._templates.modal.gridSelector).kendoGrid({
                autoBind: false,
                pageable: {
                    pageSize: 25,
                    previousNext: true,
                    info: true,
                    messages: {
                        of: 'de {0} Páginas'
                    }
                },
                dataSource: that.createDataSource(),
                columns: that.options.grid.columns
            }).data('kendoGrid');
        },

        setData: function(data){
            var that = this;
            that.data = data;
        },

        executeFilter: function (field, operation, value) {
           
            var that = this;
            var params = 'field=' + field + '&operation=' + operation + '&value=' + value;
            window.__doPostBack(that.options.id + '$LookupControl', params);
            that.messageBox.close();
        },

        executeClear: function () {
            window.app.validateOnSubmit = false;
            var that = this;
            var params = 'clear';
            window.__doPostBack(that.options.id + '$LookupControl', params);
        },

        init: function (element, options) {
            var that = this;
            that.options = $.extend({}, that.options, options);
            that.element = $(element);
            
            that.uniqueName = window.kendo.guid();
            that.select = that.element.children(that.options.selectors.select);
            var html = '';

            switch (that.options.lookupType) {
                case 'client':
                    html = that._templates
                                .modalClient
                                .html
                                .replace(/\{name\}/g, that.uniqueName);

                    that.modalElement = $(html).appendTo(document.body);
                    that.createGrid();
                    break;
                case 'server':
                    html = that._templates
                                .modalServer
                                .html
                                .replace(/\{name\}/g, that.uniqueName);

                    that.modalElement = $(html).appendTo(document.body);
                    that.iframe = that.modalElement.find(that._templates.modalServer.frameSelector);
                    break;
            }            
                                
            that.createModal();
            that._bindEvents();
        }
    });

    window.kendo.ui.plugin(LookupManager);

})(jQuery)