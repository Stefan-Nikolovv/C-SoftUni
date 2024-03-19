// switcher.js

function setStyleCookie(mode) {
    var styleMode = '@(Context.Request.Cookies["theme-style-mode"])';
    var styleCookieVal = mode == 1 ? 'dark' : 'light';
    var cookieKey = styleMode == '1' ? 'client_dark_mode_style_cookie' : 'client_light_mode_style_cookie';
    // reset cookies
    document.cookie = 'client_dark_mode_style_cookie=;expires=Thu, 01 Jan 1970 00:00:00 UTC;path=/;';
    document.cookie = 'client_light_mode_style_cookie=;expires=Thu, 01 Jan 1970 00:00:00 UTC;path=/;';
    document.cookie = cookieKey + '=' + styleCookieVal + ';expires=Thu, 01 Jan 2099 00:00:00 UTC;path=/;';
    document.body.classList.remove('active-dark-mode', 'active-light-mode');
    if (document.cookie.includes(cookieKey + '=dark')) {
        document.body.classList.add('active-dark-mode');
    } else {
        document.body.classList.add('active-light-mode');
    }
}

$(function () {
    $('.my_switcher .setColor.dark').on('click', function () {
        setStyleCookie(1);
    });
    $('.my_switcher .setColor.light').on('click', function () {
        setStyleCookie(0);
    });
});
