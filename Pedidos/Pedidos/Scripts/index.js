$(document).ready(function () {
    app.delimitateContent();

    window.app.MainMenu.bind('hide', function (e) {
        var form = e.sender.element.closest('section.content').find('.form-main'),            
            css = { 'width' : '100%' };

        form.removeClass('right').addClass('left').css(css);
    });

    window.app.MainMenu.bind('show', function (e) {
        var form = e.sender.element.closest('section.content').find('.form-main');
        form.removeClass('left').addClass('right');
        app.delimitateContent();
    });

    if (window.document.body.clientWidth <= 1150) {
        window.app.MainMenu.hide(false);
    }
    else {
        window.app.MainMenu.show(false);
    }

    $(window).resize(function () {
        app.delimitateContent();

        if (window.document.body.clientWidth <= 1150) {
            window.app.MainMenu.hide(false);
        }
        else {
            window.app.MainMenu.show(false);
        }
    });

    $('section#main > header > .menu-resizer').click(function (e) {
        e.preventDefault();
        var visible = window.app.MainMenu.visible();

        if (visible) {
            window.app.MainMenu.hide(true);
        }
        else {
            window.app.MainMenu.show(true);
        }
    });    
});