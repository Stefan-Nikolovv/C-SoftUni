function ChangeColor() {

    function setStyleCookie(mode) {
        var styleMode = document.querySelector('meta[name="theme-style-mode"]').content;
        var styleCookieVal = mode == 1 ? 'dark' : 'light';
        var cookieKey = styleMode == 1 ? 'client_dark_mode_style_cookie' : 'client_light_mode_style_cookie';
        // reset cookies
        Cookies.remove('client_dark_mode_style_cookie')
        Cookies.remove('client_light_mode_style_cookie')
        Cookies.set(cookieKey, styleCookieVal, { expires: 7 });
        $('body').removeClass('active-dark-mode active-light-mode');
        if (Cookies.get(cookieKey) == 'dark') {
            $('body').addClass('active-dark-mode');
        } else {
            $('body').addClass('active-light-mode');
        }
    }

    $('.my_switcher .setColor.dark').on('click', function () {
        setStyleCookie(1);
    });
    $('.my_switcher .setColor.light').on('click', function () {
        setStyleCookie(0);
    });

};