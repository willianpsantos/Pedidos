(function ($) {

    var events = {
        HIDE: 'hide',
        SHOW: 'show'
    };

    var MainMenuWidget = window.kendo.ui.Widget.extend({
        options: {
            name: 'MainMenuWidget',            
            items: [],

            cssClass: {
                menu: 'menu',
                item: 'menu-item'
            }
        },

        render: function(){
            var that = this,
                items = that.options.items;

            that.element.html('');

            if (!items || !items.length)
                return;

            var html = '';

            $.each(items, function () {
                html += window.kendo.format(
                            '<div class="{0}"><a id="{1}" name="{2}" href="{3}">{4}</a></div>', 
                            that.options.cssClass.item,
                            this.name,
                            this.name,
                            this.link,
                            this.title
                        );
            });

            that.element.html(html);
        },

        hide: function(animated) {
            var that = this,
                element = that.element,
                animate = animated == undefined ? true : animated,
                classItems = '.' + that.options.cssClass.item;

            if (animate) {
                element.find(classItems).fadeOut('slow', function () {
                    element.animate(
                        { 'width': '-=' + that.width + 'px' },
                        400,
                        function () {
                            element.hide();                            
                        }
                    );
                });

                that.trigger('hide', { sender: that, animated: animated });
            }
            else {
                element.find(classItems).hide();
                element.hide().css({ 'width' : '0px' });
                that.trigger('hide', { sender: that, animated:animated });
            }
        },

        show: function(animated) {
            var that = this,
                element = that.element,
                animate = animated == undefined ? true : animated,
                classItems = '.' + that.options.cssClass.item;

            if (animate) {
                element.show();

                element.animate(
                    { 'width': '+=' + that.width + 'px' },
                    400,
                    function () {
                        element.find(classItems).fadeIn('slow', function () {
                            that.trigger('show', { sender: that, animated: animated });
                        }
                    );
                });
            }
            else {
                element.css({ 'width': that.width + 'px' }).show();
                element.find(classItems).show();
                that.trigger('show', { sender: that, animated: animated });
            }
        },

        visible: function() {
            var that = this;
            return that.element.is(':visible');
        },

        init: function (element, options) {
            var that = this;
            that.options = $.extend(that.options, options);
            window.kendo.ui.Widget.fn.init.call(that, element, that.options);            
            that.element = $(element);
            that.width = that.element.width();
            that.render();
        }
    });

    window.kendo.ui.plugin(MainMenuWidget);
})(jQuery);