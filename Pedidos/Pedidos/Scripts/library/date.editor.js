(function () {

    var kendo = window.kendo,
        ui = kendo.ui,        
        DatePicker = ui.DatePicker,

        DateEditor = DatePicker.extend({

            //value: function(value) {
            //    var that = this;

            //    if (that._value && !value) {
            //        that._value.setHours(8);                    
            //        return that._value;
            //    }

            //    if (value === undefined) {
            //        return that._value;
            //    }

            //    ui.DatePicker.fn.value.call(that, value);
            //    that._value && that._value.setHours(8);
            //},

            init: function (element, options) {
                var that = this;
                $(element).mask("99/99/9999");                
                DatePicker.fn.init.call(that, element, options);
                that.element.data("kendoDatePicker", that);

                if (that.options.enabled != undefined) {
                    that.enable(that.options.enabled);
                }
            },
            
            options: {                
                name: "DateEditor",
                culture: 'pt-BR',
                format: 'dd/MM/yyyy',
            }
        });

    ui.plugin(DateEditor);
})();